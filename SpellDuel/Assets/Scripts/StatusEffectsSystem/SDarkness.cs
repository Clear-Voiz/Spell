using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SDarkness : AlterSpell
{
    private Light[] illumination;
    private float[] maxValues;
    private float elapsedTime;
    private float elapsedTime2;
    private float duration;
    private Conjure _conjure;

    public SDarkness(Conjure _conjure)
    {
        tim = new Timers(3);
        this._conjure = _conjure;
        lifespan = 10f;
        illumination = MonoBehaviour.FindObjectsOfType<Light>();
        maxValues = new float[illumination.Length];
        duration = 1f;
        for (int i = 0; i < illumination.Length; i++)
        {
            maxValues[i] = illumination[i].intensity;
        }

        IsBuff = true;
    }
    public override void Effect()
    {
        if (tim.alarm[0] < 1f)
        {
            tim.alarm[0] = tim.Chronometer(1f,tim.alarm[0], ShutDown);
        }

        if (tim.alarm[0] == 1f)
        {
            tim.alarm[1] = tim.Timer(3f,tim.alarm[1]);
        }

        if (tim.alarm[1] == 3f)
        {
            tim.alarm[2] = tim.Chronometer(1f,tim.alarm[2], TurnOn);
        }
    }
    
    private void ShutDown()
    {
        var completeness = elapsedTime / duration;
        for (int i = 0; i < illumination.Length; i++)
        {
            var quant = Mathf.Lerp(maxValues[i], 0f, completeness);
            illumination[i].intensity = quant;
        }

        var intensity = Mathf.Lerp(1f, 0f, completeness);
        RenderSettings.ambientIntensity = intensity;

        elapsedTime += Time.deltaTime;
        if (completeness >=1f)
        {
            elapsedTime = 0f;
        }
    }

    private void TurnOn()
    {
        var completeness = elapsedTime2 / duration;
        for (int i = 0; i < illumination.Length; i++)
        {
            var quant = Mathf.Lerp(0f,maxValues[i], completeness);
            illumination[i].intensity = quant;
        }
        var intensity = Mathf.Lerp(0f, 1f, completeness);
        RenderSettings.ambientIntensity = intensity;
        elapsedTime2 += Time.deltaTime;
        if (completeness >=1f)
        {
            Debug.Log("EnterEndState");
            EndEffect();
        }
    }

    public override void EndEffect()
    {
       _conjure.effectsManager.RemoveEffect(this);
    }
}
