using UnityEngine;

public class EnemySecondWorldChanger : SecondWorldExChangingTrigger
{
    private bool _isChanged = false;
    public override void TriggerWorldChange()
    {
        if (!_isChanged)
        {
            //gameObject.GetComponent<Renderer>().material.color = Color.white;
            Debug.Log("Имеджинируем партиклы превращения в другое животное");
            gameObject.transform.localScale *= 0.5f;
            _isChanged = true;
        }
    }

    public override void TriggerWorldChangeBack()
    {
    }
}