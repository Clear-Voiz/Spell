using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SWater : AlterSpell
{
    private Conjure _conjure;
    public SWater()
    {
        IsBuff = true;
        tim = new Timers(2);
        _conjure = MonoBehaviour.FindObjectOfType<Conjure>();
    }
    
    public override void Effect()
    {
        var EM = _conjure.effectsManager.ActiveEffects;
        if (EM.Count > 0)
        {
            var removedEffects = 0f;
            for (int i = _conjure.effectsManager.ActiveEffects.Count-1; i >= 0; i--)
            {
                if (EM[i].IsBuff==false)
                {
                    EM[i].EndEffect();
                    removedEffects += 1f;
                }
            }
            Debug.Log(removedEffects);
        }
        
        EndEffect();
    }

    public override void EndEffect()
    {
        _conjure.effectsManager.RemoveEffect(this);
    }
}
