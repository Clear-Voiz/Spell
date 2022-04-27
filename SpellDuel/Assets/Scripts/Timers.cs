using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timers
{
    private int length;
    public float[] alarm;
    public int index;
    
    public Timers(int Length)
    {
        length = Length;
        alarm = new float[length];
        index = 0;
    }

    public float Timer(float duration, Action act)
    {
        if (duration > 0f)
        {
            duration -= Time.deltaTime;

            if (duration <= 0f)
            {
                act();
                duration = 0f;
            }
        }

        return duration;
    }
    
    public float Chronometer(float duration, Action constAct)
    {
        if (duration > 0f)
        {
            duration -= Time.deltaTime;
            constAct();
            if (duration <=0f)
            {
                duration = 0f;
            }
        }
        
        return duration;
    }

    public float Timer(float duration)
    {
        if (duration > 0f)
        {
            duration -= Time.deltaTime;
            if (duration <=0f)
            {
                duration = 0f;
            }
        }
        
        return duration;
    }
}
