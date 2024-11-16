using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
[NodeTint("#FFBF00")]
public class ChoiceNode : DialogueBaseNode
{

    [Output(dynamicPortList = true,connectionType = ConnectionType.Override)] 
    public List<Answer> answers = new List<Answer>();

    public Node GetNodeByAnswer(int answerNum)
    {
        var port = GetPort("answers " + answerNum);
        if (port == null)
        {
            Debug.LogError($"Ошибка ответа под номером {answerNum} не существует");
            return null;
        }

        if (port.IsConnected)
        {
            return port.Connection.node;
        }

        return null;
    }
}

[System.Serializable] public class Answer {
    [TextArea]
    public string text;
}
