using DialogueSystem.Nodes;

/// <summary>
/// Нода которая запоминает описанный в ней ответ, чтобы потом добавить его
/// в следующий выбор игрока
/// при выборе данного варианта ответа диалог пойдет к следующей ноде от этой
/// </summary>
[NodeTint("#999923")]
public class CashAnswerNode : DialogueBaseNode
{
    [Output(backingValue = ShowBackingValue.Never, connectionType = ConnectionType.Override)]
    public DialogueBaseNode answerNode;

    public Answer answer;

    public Node GetAnswerNode()
    {
        var port = GetOutputPort("answerNode");
        if (port.IsConnected)
        {
            return port.Connection.node;
        }

        return null;
    }
}