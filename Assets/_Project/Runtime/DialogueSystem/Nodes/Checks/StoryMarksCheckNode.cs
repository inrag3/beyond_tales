using UnityEngine;

[CreateNodeMenu("Checks/StoryMarksCheck")]
[NodeTint("#FF3766")]
public class StoryMarksCheckNode : CheckNode
{
    [Tooltip("Отметки необходимые для прохождения проверки")]
    public string[] RequiredMarks;

    [Tooltip("Отметки при наличии, которых проверка автоматически проваливается")]
    public string[] ForbiddenMarks;
}