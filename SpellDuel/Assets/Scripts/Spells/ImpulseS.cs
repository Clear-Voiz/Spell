using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpulseS: Spell
{
    private GameObject _subject;
    private Timers tim;
    private bool trigger;
    private Vector3 dir;
    private Rigidbody rb;
    private void Awake()
    {
        PM = 1.2f; //Power Multiplier
        Element = Elements.NonElemental;
        _conjure = FindObjectOfType<Conjure>();
        tim = new Timers(2);
    }

    private void Start()
    {
        tim.alarm[0] = 2f;
        RaycastHit hit;
        Physics.Raycast(_conjure.ori.position, _conjure.ori.forward, out hit);
        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Controllable"))
            {
                _subject = hit.collider.gameObject;
                rb = _subject.AddComponent<Rigidbody>();
                rb.useGravity = false;

            }
        }
        trigger = true;
        
    }

    private void Update()
    {
        if (_subject != null)
        {
            if (trigger)
            {
                //_subject.transform.localRotation = _conjure.ori.rotation;
                if (Input.GetMouseButtonDown(0))
                {
                    trigger = false;
                    rb.AddForce(_conjure.ori.forward*12f,ForceMode.Impulse);
                    //rb.AddForce(_subject.transform.forward*12f,ForceMode.Impulse);
                }
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
