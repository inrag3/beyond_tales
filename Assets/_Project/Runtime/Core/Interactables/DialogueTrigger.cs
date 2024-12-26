using System;
using UnityEngine;

namespace _Project.Runtime.Core.Interactables
{
    [RequireComponent(typeof(SphereCollider))]
    public class DialogueTrigger : MonoBehaviour
    {
        [SerializeField] private DialogueGraph _graph;
        public event Action<DialogueGraph> Entered;
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Herbalist.Herbalist _))
            {
                Entered?.Invoke(_graph);
            }
        }
    }
}