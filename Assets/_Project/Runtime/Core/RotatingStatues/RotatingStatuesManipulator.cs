using System;
using System.Collections.Generic;
using _Project.Runtime.Core.Grenades;
using _Project.Runtime.Infrastructure.Factories;
using _Project.Runtime.QuestSystem;
using UnityEngine;
using Zenject;

public class RotatingStatuesManipulator : SecondWorldExChangingTrigger
{
    private IHerbalistProvider _herbalistProvider;
    [SerializeField] private Transform _target;
    [SerializeField] private GameObject _brokenModel;
    [SerializeField] private GameObject _clearModel;
    [SerializeField] private List<GameObject> _redEyes;
    [SerializeField] private AddStoryMarksQuestAction _callback;
    [SerializeField] private float maxTurnSpeed = 720f;  // °/с
    [SerializeField] private float corridorAngle = 20f;   // «Коридор», градусы
    [SerializeField] private float finalTolerance = 0.5f;
    private bool _isBroken = true;
    private bool _isFixed = false;
    private State _state = State.TrackPlayer;
    private bool Fixed
    {
        get
        {
            return _isFixed;
        }
        set
        {
            foreach (var item in _redEyes)
                item.SetActive(value);
            _isFixed = value;
        }
    }



    [Inject]
    void Construct(IHerbalistProvider herbalistProvider)
    {
        _herbalistProvider = herbalistProvider;
        foreach (var item in _redEyes)
            item.SetActive(false);
        _clearModel.SetActive(false);
    }
    

    protected override void TriggerWorldChange()
    {
        if (Fixed)
        {
            return;
        }
        _brokenModel.SetActive(false);
        _clearModel.SetActive(true);
        _clearModel.transform.rotation = _brokenModel.transform.rotation;
        _isBroken = false;
    }

    protected override void TriggerWorldChangeBack()
    {
        if (Fixed)
        {
            return;
        }
        _brokenModel.SetActive(true);
        _clearModel.SetActive(false);
        _brokenModel.transform.rotation = _clearModel.transform.rotation;
        _isBroken = true;
    }
    
    void Update ()
    {
        if(_isBroken || Fixed)
            return;
        
        switch (_state)
        {
            case State.TrackPlayer:
                YawTowards(_herbalistProvider.Herbalist.Transform);

                // Попали в коридор? — переключаемся на «LockingOnTarget»
                if (FlatAngleTo(_target) <= corridorAngle)
                    _state = State.LockingOnTarget;
                break;

            case State.LockingOnTarget:
                YawTowards(_target);

                // Полностью повернулись на lockTarget — переходим в «Locked»
                if (FlatAngleTo(_target) <= finalTolerance)
                {
                    _state = State.Locked;
                    Fixed = true;
                    _callback.Activate();
                }
                break;

            case State.Locked:
                // Ничего не делаем
                break;
        }
    }
    
    /// <summary>Поворачивает по Y к целевому Transform с ограниченной скоростью.</summary>
    private void YawTowards(Transform target)
    {
        // Горизонтальная проекция направления (XZ‑плоскость)
        Vector3 dir = target.position - transform.position;
        dir.y = 0f;                       // «сплющиваем» в плоскость
        if (dir.sqrMagnitude < 0.0001f) return;

        Quaternion goal = Quaternion.LookRotation(dir, Vector3.up);

        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            goal,
            maxTurnSpeed * Time.deltaTime);
    }

    /// <summary>Угол (в градусах) между горизонтальным forward и целью.</summary>
    private float FlatAngleTo(Transform target)
    {
        Vector3 fwd = transform.forward;
        fwd.y = 0f;

        Vector3 dir = target.position - transform.position;
        dir.y = 0f;

        return Vector3.Angle(fwd, dir);
    }
    private enum State { TrackPlayer, LockingOnTarget, Locked }

}
