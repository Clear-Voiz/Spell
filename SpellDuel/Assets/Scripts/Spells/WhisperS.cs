using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhisperS : Spell,IEffectable
{
    private Timers tim;
    private void Awake()
    {
        Element = Elements.NonElemental;
        lifespan = 10f;
        _conjure = FindObjectOfType<Conjure>();
    }

    private void Start()
    {
        tim = new Timers(1);
        _conjure.whisper = true;
        tim.alarm[0] = lifespan;
    }

    private void Update()
    {
        tim.alarm[0] = tim.Timer(tim.alarm[0], Effect);
    }

    public void Effect()
    {
        _conjure.whisper = false;
        Debug.Log("Desaparecido");
        Destroy(this);
    }
}
