using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : ScriptableObject
{
    public event Action OnDissolve;


    public void Decompose()
    {
        
        OnDissolve?.Invoke();
    }
}
