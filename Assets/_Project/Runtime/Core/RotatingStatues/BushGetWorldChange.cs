using _Project.Runtime.Core.Interactables;
using _Project.Runtime.Core.Interactables.Processors;
using UnityEngine;

public class BushGetWorldChange: Interactable
{
    [SerializeField] private int _potionNumber;
    private void Start()
    {
        IsAccessible = true;
    }

    public override void Interact(IInteractableVisitor visitor)
    {
        visitor.Accept(this,_potionNumber);
    }
}