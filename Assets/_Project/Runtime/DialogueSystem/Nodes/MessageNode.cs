using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

[NodeTint("#00C12B")]
public class MessageNode : DialogueBaseNode
{
	[Tooltip("Имя актера, который произносит текущее сообщение")]
	public string ActorName;

	[TextArea(5,5)]
	public string message;
	
	
}