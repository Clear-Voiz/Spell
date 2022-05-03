using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SParalysis : AlterSpell
{
    private TD_Movement movComp;
    private Conjure _conjure;
        
    public SParalysis(Collider other, ref Conjure _conjure)
    {
        tim = new Timers(1);
        
        
        if (other.TryGetComponent(out movComp))
        {
            movComp.enabled = false;
        }   
    }
    public override void Effect()
    {
        tim.alarm[0] = tim.Timer(3f, tim.alarm[0], EndEffect);
    }

    public override void EndEffect()
    {
        movComp.enabled = true;
        _conjure.effectsManager.RemoveEffect(this);
        
    }
}
