using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AlterSpell
{
    protected Timers tim;
    protected float lifespan;
    public bool isActive;
    public bool IsBuff {get; protected set;}

    public abstract void Effect();
    public abstract void EndEffect();
}
