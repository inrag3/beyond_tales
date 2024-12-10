using System;
using System.Collections.Generic;
using DialogueSystem;
using Unity.VisualScripting;

public class Dialoguer : IInitializable, IDisposable
{
    private readonly List<DialogueTrigger> _triggers;
    private readonly DialogueManager _dialogueManager;
    private readonly IPauseHandler _pauseHandler;

    public Dialoguer(
        List<DialogueTrigger> triggers,
        DialogueManager dialogueManager,
        IPauseHandler pauseHandler)
    {
        _pauseHandler = pauseHandler;
        _dialogueManager = dialogueManager;
        _triggers = triggers;
    }

    public void Initialize()
    {
        _dialogueManager.Ended += OnDialogueEnded;
        
        foreach (var trigger in _triggers)
        {
            trigger.Entered += OnEntered;
        }
    }

    public void Dispose()
    {
        foreach (var trigger in _triggers)
        {
            trigger.Entered -= OnEntered;
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
        _pauseHandler.Unpause();
    }
}