using System.Collections;
using System.Collections.Generic;
using DialogueSystem;
using UnityEngine;

public class TestDialogueLauncher : MonoBehaviour
{
    [SerializeField] private DialogueGraph _dialogue;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            DialogueManager.instance.StartDialogue(_dialogue);
        }
    }
}
