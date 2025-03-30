using System;
using System.Collections;
using _Project.Runtime.Core.Interactables;
using UnityEngine;

namespace _Project.Runtime.QuestSystem
{
    public class SpawnerCounterAction : BaseQuestAction
    {
        [SerializeField] private int targetCounter;

        [SerializeField] private DialogueInteractionTrigger _dialogue;
        [SerializeField] private int secondWait;

        [NonSerialized]
        private int _currentCounter = 0;
        private IEnumerator _coroutine;

        public void ReRunCoroutine()
        {
            DelayCoroutine();
            _coroutine = StartCoroutine(secondWait);
            StartCoroutine(_coroutine);
        }

        public void DelayCoroutine()
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }
        }

        public override void Activate()
        {
            _currentCounter++;
            if (_currentCounter == targetCounter)
            {
                _currentCounter = 0;
                ReRunCoroutine();
            }
        }

        private IEnumerator StartCoroutine(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            _dialogue.Interact();
        }
    }
}