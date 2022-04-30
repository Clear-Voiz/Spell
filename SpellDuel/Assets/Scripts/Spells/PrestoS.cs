using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrestoS : Spell
{
    private StatModifier presto;
    private Timers tim;
    private void Start()
    {
        tim = new Timers(1);
        tim.alarm[0] = 5f;
        presto = new StatModifier(1f, modiType.Percent);
        if (!Globs.spd.statModifiers.Contains(presto))
        {
            Globs.spd.AddModifier(presto);
            Debug.Log("has been added");
        }
    }

    private void Update()
    {
        tim.alarm[0] = tim.Timer(tim.alarm[0], EndEffect);
    }

    private void EndEffect()
    {
        Globs.spd.RemoveModifier(presto);
        Destroy(this);
    }
}
