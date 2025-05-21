using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelDisintegration : MonoBehaviour
{
    
    [SerializeField] public GameObject _obj;
    
    public void  Disable()
    {
        _obj.SetActive(false);
    }
}
