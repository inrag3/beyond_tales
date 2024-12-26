using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    private Rigidbody _rb;

    private bool _targetHit;
    
    public event Action<Grenade> Hit;
    public bool IsPreventDestroy {get; set; }

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // make sure only to stick to the first target you hit
        if (_targetHit)
            return;
        _targetHit = true;


        // make sure projectile sticks to surface
        _rb.isKinematic = true;

        // make sure projectile moves with target
        //transform.SetParent(collision.transform);
        Hit?.Invoke(this);
        
        if(!IsPreventDestroy)
            Destroy(gameObject);
    }
}