using UnityEngine;

[NodeTint("#5b00c1")]
public class StoryMarksNode : DialogueBaseNode
{
    [Tooltip("Массив отметок, которые нужно добавить игроку")] [TextArea]
    public string[] MarksToAdd;

    [Tooltip("Массив отметок, которые нужно удалить")] [TextArea]
    public string[] MarksToRemove;
}