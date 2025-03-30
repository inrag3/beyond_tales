using System;
using System.Collections.Generic;
using _Project.Runtime.Core.Interactables;
using _Project.Runtime.Core.PauseHandler;
using _Project.Runtime.QuestSystem;
using DialogueSystem;
using UnityEngine;
using Zenject;

public class Dialoguer : IInitializable, IDisposable
{
    private readonly List<DialogueTrigger> _triggers;
    private readonly List<DialogueInteractionTrigger> _interactionTriggers;
    private readonly List<StartDialogueQuestAction> _startDialogueQuestActions;
    private readonly DialogueManager _dialogueManager;
    private readonly IPauseHandler _pauseHandler;
    private readonly PauseHandlersRegister _pauseHandlersRegister;

    public Dialoguer(
        List<DialogueTrigger> triggers,List<DialogueInteractionTrigger> interactionTriggers,
        List<StartDialogueQuestAction> questActions,
        DialogueManager dialogueManager,
        IPauseHandler pauseHandler, PauseHandlersRegister pauseHandlersRegister)
    {
        _pauseHandler = pauseHandler;
        _dialogueManager = dialogueManager;
        _triggers = triggers;
        _interactionTriggers = interactionTriggers;
        _startDialogueQuestActions = questActions;
        _pauseHandlersRegister = pauseHandlersRegister;
    }

    public void Initialize()
    {
        _dialogueManager.Ended += OnDialogueEnded;
        
        foreach (var trigger in _triggers)
        {
            trigger.Entered += OnStartDialogueGraph;
        }

        foreach (var interactionTrigger in _interactionTriggers)
        {
            interactionTrigger.Interacted += OnStartDialogueGraph;
        }

        foreach (var questAction in _startDialogueQuestActions)
        {
            questAction.StartAction += OnStartDialogueGraph;
        }
    }

    public void Dispose()
    {
        foreach (var trigger in _triggers)
        {
            trigger.Entered -= OnStartDialogueGraph;
        }
        
        foreach (var interactionTrigger in _interactionTriggers)
        {
            interactionTrigger.Interacted -= OnStartDialogueGraph;
        }
        
        foreach (var questAction in _startDialogueQuestActions)
        {
            questAction.StartAction -= OnStartDialogueGraph;
        }

        _dialogueManager.Ended -= OnDialogueEnded;
    }

    private void OnStartDialogueGraph(DialogueGraph graph)
    {
        _dialogueManager.StartDialogue(graph);


        _pauseHandlersRegister.PauseAll();
        // _pauseHandler.Pause();
    }
    

    private void OnDialogueEnded()
    {
        _pauseHandlersRegister.ResumeAll();
        //_pauseHandler.Resume();
    }
}