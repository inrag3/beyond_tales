using System;
using System.Collections.Generic;
using DialogueSystem.Nodes;
using UnityEngine;

[NodeTint("#FFBF00")]
public class ChoiceNode : DialogueBaseNode
{
    [Output(dynamicPortList = true, connectionType = ConnectionType.Override)]
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

[Serializable]
public class Answer
{
    [TextArea] public string text;
}