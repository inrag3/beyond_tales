using DialogueSystem;
using UnityEngine;

public class TestDialogueLauncher : MonoBehaviour
{
    [SerializeField] private DialogueGraph _dialogue;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Debug.Log("dialogue started");
            DialogueManager.instance.StartDialogue(_dialogue);
        }
    }
}