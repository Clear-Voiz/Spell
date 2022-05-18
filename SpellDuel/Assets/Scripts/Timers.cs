using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timers
{
    public float[] alarm;

    public Timers(int length)
    {
        alarm = new float[length];
        for(int i = 0;i<length;i++)
        {
            alarm[i] = 0f;
        }
    }

    public float Timer(float duration, float currentTime, Action act)
    {
        if (currentTime < duration)
        {
            currentTime += Time.deltaTime;

            if (currentTime >= duration)
            {
                currentTime = duration;
                act();
            }
        }

        return currentTime;
    }
    
    public float Cicle(float duration, float currentTime, Action act)
    {
        if (currentTime < duration)
        {
            currentTime += Time.deltaTime;

            if (currentTime >= duration)
            {
                currentTime = 0f;
                act();
            }
        }

        return currentTime;
    }
    
    /*public float Timer<T>(float duration, Action<T> act)
    {
        if (duration > 0f)
        {
            duration -= Time.deltaTime;

            if (duration <= 0f)
            {
                act(T);
                duration = 0f;
            }
        }

        return duration;
    }*/

    
    public float Chronometer(float duration, float currentTime, Action constAct)
    {
        if (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            if (currentTime >= duration)
            {
                currentTime = duration;
            }
            constAct();
        }
        
        return currentTime;
    }

    public float Timer(float duration, float currentTime)
    {
        if (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            if (currentTime >= duration)
            {
                currentTime = duration;
            }
        }
        
        return currentTime;
    }
}
