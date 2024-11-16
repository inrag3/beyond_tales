using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

[CreateAssetMenu]
public class DialogueGraph : NodeGraph { 
	public Node GetFirstNode()
	{
		foreach (var node in nodes)
		{
			if (node is InitializeActorsNode)
			{
				return node;
			}
		}
		Debug.LogError("В диалоге обязательно должна быть инициализирующая нода");
		return null;
	}
}