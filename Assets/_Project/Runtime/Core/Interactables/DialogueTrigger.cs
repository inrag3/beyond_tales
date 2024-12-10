using System;
using _Project.Runtime.Core.Herbalist;
using UnityEngine;


[RequireComponent(typeof(SphereCollider))]
public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private DialogueGraph _graph;

    public event Action<DialogueGraph> Entered;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Herbalist _))
        {
            Entered?.Invoke(_graph);
        }
    }
}