using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SIce : AlterSpell
{
    private int amount;
    private float interval;
    private GameObject iceShard;
    private Conjure _conjure;
    
    public SIce()
    {
        interval = 0.2f;
        amount = 10;
        _conjure = MonoBehaviour.FindObjectOfType<Conjure>();
        iceShard = _conjure.SH.ice;
        tim = new Timers(1);
        OnStart();
    }
    
    public override void Effect()
    {
        tim.alarm[0] = tim.Cicle(interval, tim.alarm[0], Freezer);
    }

    public override void EndEffect()
    {
        OnEnd();
    }
    

    private void Freezer()
    { 
        Vector3 place = new Vector3(Random.Range(_conjure.ori.position.x - 2f, _conjure.ori.position.x + 2f),
            Random.Range(_conjure.ori.position.y + 1f, _conjure.ori.position.y + 3f), _conjure.ori.position.z*1.5f);
            
        MonoBehaviour.Instantiate(iceShard, place, Quaternion.identity);
        tim.alarm[0] = 0f;
        amount -= 1;
        if (amount <= 0)
        {
            EndEffect();
        }
    }
}
