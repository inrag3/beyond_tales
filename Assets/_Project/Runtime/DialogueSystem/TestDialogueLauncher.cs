using System.Collections;
using System.Collections.Generic;
using DialogueSystem;
using UnityEngine;
using Zenject;

public class TestDialogueLauncher : MonoBehaviour
{
    [SerializeField] private DialogueGraph _dialogue;

    [Inject]
    private DialogueManager _dialogueManager;
    
    public void Construct(DialogueManager dialogueManager)
    {
        _dialogueManager = dialogueManager;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            _dialogueManager.StartDialogue(_dialogue);
        }
    }
}
