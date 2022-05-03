using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SWhisper : AlterSpell
{
    private Conjure _conjure;

    public SWhisper()
    {
        lifespan = 10f;
        _conjure = MonoBehaviour.FindObjectOfType<Conjure>();
        tim = new Timers(1);
        _conjure.whisper = true;
        isActive = true;
    }
    
    public override void Effect()
    {
        tim.alarm[0] = tim.Timer(lifespan,tim.alarm[0], EndEffect);
    }

    public override void EndEffect()
    {
        _conjure.whisper = false;
        Debug.Log("Desaparecido");
        _conjure.effectsManager.RemoveEffect(this);
    }
}
