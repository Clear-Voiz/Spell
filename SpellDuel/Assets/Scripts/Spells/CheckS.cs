using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckS : Spell
{
    private Rigidbody _rig;
    private Timers tim;
    private void Awake()
    {
        _rig = GetComponent<Rigidbody>();
        tim = new Timers(2);
        Element = Elements.NonElemental;
        _conjure = FindObjectOfType<Conjure>();
        PM = 0.5f;
        LP = _conjure.SH.LP;

    }

    private void Update()
    {
        tim.alarm[0] = tim.Timer(0.7f, tim.alarm[0], Fall);
        tim.alarm[1] = tim.Timer(4f, tim.alarm[1], Finish);
    }

    private void Fall()
    {
        _rig.useGravity = true;
        Debug.Log("using gravity");
    }

    private void Finish()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            _rig.useGravity = false;
            _rig.velocity = Vector3.zero;
        }
        
        if (other.CompareTag("Enemy"))
        {
            var stats = other.GetComponent<Stats>();
            if (stats.hp <= (stats.maxHp / 10))
            {
                Destroy(other.gameObject);
            }
            else
            {
                Damager(other);
            }
        }
    }
}
