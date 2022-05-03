using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSlow : AlterSpell
{
    private StatModifier slow;
    private Conjure _conjure;

    public SSlow(Conjure conjure)
    {
        tim = new Timers(1);
        lifespan = 5f;
        if (!Globs.spd.statModifiers.Contains(slow))
        {
            slow = new StatModifier(-0.1f, modiType.Percent);
            Globs.spd.AddModifier(slow);
        }

        _conjure = conjure;
    }
    public override void Effect()
    {
        tim.alarm[0] = tim.Timer(lifespan,tim.alarm[0],EndEffect);
    }

    public override void EndEffect()
    {
        Globs.spd.RemoveModifier(slow);
        Debug.Log(Globs.spd.statModifiers.Count);
        _conjure.effectsManager.RemoveEffect(this);
        
    }
}
