using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WellChanger : SecondWorldExChangingTrigger
{
    public override void TriggerWorldChange()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.red;
    }

    public override void TriggerWorldChangeBack()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.white;
    }
}