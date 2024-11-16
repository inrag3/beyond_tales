using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class DialogueBaseNode : Node
{
	[Input(backingValue = ShowBackingValue.Never)] public DialogueBaseNode input;
	[Output(backingValue = ShowBackingValue.Never,connectionType = ConnectionType.Override)] 
	public DialogueBaseNode output;
    
	public override object GetValue(NodePort port) {
		return null;
	}

	public virtual Node GetNextNode()
	{
		var port = GetOutputPort("output");
		if (port.IsConnected)
		{
			return port.Connection.node;
		}

		return null;
	}
}