using System.Collections;
using System.Collections.Generic;
using _Project.Runtime.Core.Herbalist;
using DialogueSystem;
using UnityEngine;
using Zenject;

public class TestDialogueLauncher : MonoBehaviour
{
    [SerializeField] private DialogueGraph _dialogue;
    private IInputService _inputService;
    
    
    [Inject]
    private void Construct(IInputService inputService)
    {
        _inputService = inputService;
    }
    void Update()
    {
        if (_inputService.IsDialogButtonPressed)
        {
            DialogueManager.instance?.StartDialogue(_dialogue);
        }
    }
}
