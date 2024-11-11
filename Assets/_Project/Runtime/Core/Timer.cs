using System;
using System.Collections;
using UnityEngine;

public class Timer
{
    public event Action TimeEnded;
    private readonly ICoroutinePerformer _performer;
    private Coroutine _coroutine;

    public Timer(ICoroutinePerformer performer)
    {
        _performer = performer;
    }

    public void Reset()
    {
        _performer.StopPerform(_coroutine);
    }

    public void Start(float duration)
    {
        _coroutine = _performer.StartPerform(Delay(duration));
    }

    private IEnumerator Delay(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        TimeEnded?.Invoke();
    }
}