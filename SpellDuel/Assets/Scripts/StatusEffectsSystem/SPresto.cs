using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class SPresto : AlterSpell
{
    private StatModifier presto;
    private Conjure _conjure;
    public SPresto(Conjure conjure)
    {
        tim = new Timers(1);
        lifespan = 5f;
        _conjure = conjure;
        presto = new StatModifier(1f, modiType.Percent);
        if (!Globs.spd.statModifiers.Contains(presto))
        {
            Globs.spd.AddModifier(presto);
            Debug.Log("has been added");
        }

        isActive = true;
    }
    public override void Effect()
    {
        tim.alarm[0] = tim.Timer(lifespan, tim.alarm[0], EndEffect);
    }
    public override void EndEffect()
    {
        Globs.spd.RemoveModifier(presto);
        _conjure.effectsManager.RemoveEffect(this);
        
    }
}
