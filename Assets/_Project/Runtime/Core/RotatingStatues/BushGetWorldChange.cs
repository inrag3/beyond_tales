using System;
using _Project.Runtime.Core.Grenades;
using _Project.Runtime.Core.Interactables;
using _Project.Runtime.Core.Interactables.Processors;
using UnityEngine;
using Zenject;

public class BushGetWorldChange: Interactable
{
    [SerializeField] private int _potionNumber;
    [SerializeField] private GameObject _potionIngredients;
    private bool _lastUpdate;
    private GrenadeThrower _grenadeThrower;

    [Inject]
    void Construct(GrenadeThrower grenadeThrower)
    {
        _grenadeThrower = grenadeThrower;
    }

    private void Start()
    {
        IsAccessible = true;
        _lastUpdate = !_grenadeThrower.NeedIngredient(_potionNumber);
    }

    private void Update()
    {
        if (_lastUpdate !=  _grenadeThrower.NeedIngredient(_potionNumber))
        {
            _lastUpdate = !_lastUpdate;
            _potionIngredients.SetActive(_lastUpdate);
        }
    }

    public override void Interact(IInteractableVisitor visitor)
    {
        visitor.Accept(this,_potionNumber);
    }
}