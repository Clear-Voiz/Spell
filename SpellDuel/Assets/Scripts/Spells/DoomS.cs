using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

public class DoomS : Spell,IEffectable,IShootable
{
    private Timers tim;
    private bool countStart;
    private Collider col;
    //private MeshRenderer rend;

    private void Awake()
    {
        tim = new Timers(1);
        //rend = GetComponent<MeshRenderer>();
        PM = 1.2f;
        Element = Elements.NonElemental;
    }

    private void Start()
    {
        speed = 20f;
        tim.alarm[0] = 3f;
    }

    private void Update()
    {
        
        if (countStart)
        {
            tim.alarm[0] = tim.Timer(tim.alarm[0], Effect);
        }
        Shoot();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            col = other;
            countStart = true;
            //rend.enabled = false;
            speed = 0f;
        }
    }


    public void Effect()
    {
        Damager(col);
        Destroy(gameObject);
    }

    public void Shoot()
    {
        transform.Translate(Vector3.forward*speed * Time.deltaTime);
    }
}
