using System;
using System.Collections.Generic;
using _Project.Runtime.Core.Interactables;
using _Project.Runtime.Core.PauseHandler;
using DialogueSystem;
using UnityEngine;
using Zenject;

public class Dialoguer : IInitializable, IDisposable
{
    private readonly List<DialogueTrigger> _triggers;
    private readonly List<DialogueInteractionTrigger> _interactionTriggers;
    private readonly DialogueManager _dialogueManager;
    private readonly IPauseHandler _pauseHandler;

    public Dialoguer(
        List<DialogueTrigger> triggers,List<DialogueInteractionTrigger> interactionTriggers,
        DialogueManager dialogueManager,
        IPauseHandler pauseHandler)
    {
        _pauseHandler = pauseHandler;
        _dialogueManager = dialogueManager;
        _triggers = triggers;
        _interactionTriggers = interactionTriggers;
    }

    public void Initialize()
    {
        _dialogueManager.Ended += OnDialogueEnded;
        
        foreach (var trigger in _triggers)
        {
            trigger.Entered += OnEntered;
        }

        foreach (var interactionTrigger in _interactionTriggers)
        {
            interactionTrigger.Interacted += OnEntered;
        }
    }

    public void Dispose()
    {
        foreach (var trigger in _triggers)
        {
            trigger.Entered -= OnEntered;
        }
        
        foreach (var interactionTrigger in _interactionTriggers)
        {
            interactionTrigger.Interacted -= OnEntered;
        }

        _dialogueManager.Ended -= OnDialogueEnded;
    }

    private void OnEntered(DialogueGraph graph)
    {
        _dialogueManager.StartDialogue(graph);
        _pauseHandler.Pause();
    }

    private void OnDialogueEnded()
    {
        _pauseHandler.Resume();
    }
}