using DialogueSystem.Nodes;

[CreateNodeMenu("Checks/Check")]
public class CheckNode : DialogueBaseNode
{
    [Output] public DialogueBaseNode pass;
    [Output] public DialogueBaseNode fail;

    public Node GetNextNodeByCheck(bool check)
    {
        var port = GetOutputPort(check ? "pass" : "fail");

        return port.IsConnected ? port.Connection.node : null;
    }
}