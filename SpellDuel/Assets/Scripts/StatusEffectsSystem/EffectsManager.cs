using System;
using System.Collections.Generic;
using FishNet.Object;
using TMPro;
using UnityEngine;

public class EffectsManager : NetworkBehaviour
{
    public List<AlterSpell> ActiveDebuffs;
    public List<AlterSpell> ActiveBuffs;
    public TextMeshProUGUI informer;
    private Timers tim;
    private float duration;

    private void Awake()
    {
        tim = new Timers(1);
    }

    void Start()
    {
        ActiveDebuffs = new List<AlterSpell>();
        ActiveBuffs = new List<AlterSpell>();
        duration = 1f;
        tim.alarm[0] = duration;
    }

    private void OnEnable()
    {
        AlterSpell.onEnd += RemoveEffect;
    }

    private void OnDisable()
    {
        AlterSpell.onEnd -= RemoveEffect;
    }

    
    void Update()
    {
        if (tim.alarm[0] < duration) 
            tim.alarm[0] = tim.Timer(duration, tim.alarm[0], Vanish);
        
        //DEBUFFS
        /*if (ActiveDebuffs.Count > 0)
        {
            for (int i = ActiveDebuffs.Count - 1; i >= 0; i--) //never change to foreach or might cause errors
            {
                ActiveDebuffs[i].Effect();
                if (ActiveDebuffs.Count < 1) break;
            }
        }*/

        if (Input.GetKeyDown(KeyCode.Z))
        {
            print(ActiveDebuffs.Count);
        }
        
        //BUFFS
        /*if (ActiveBuffs.Count > 0)
        {
            for (int i = ActiveBuffs.Count - 1; i > 0; i--) //never change to foreach or might cause errors
            {
                Debug.Log(i);
                ActiveDebuffs[i].Effect();
                if (ActiveDebuffs.Count < 1) break;
            }
        }*/
    }
    
    public void RemoveEffect(AlterSpell effect)
    {
        if (effect.IsBuff)
        {
            ActiveBuffs.Remove(effect);
        }
        else
        {
            ActiveDebuffs.Remove(effect);
        }
    }
    
    public void AddDebuff(AlterSpell effect)
    {
        ActiveDebuffs.Add(effect);
        if (effect.effectTitle != String.Empty)
        {
            informer.text = effect.effectTitle;
            tim.alarm[0] = 0f;
        }
    }
    
    public void AddBuff(AlterSpell effect)
    {
        ActiveBuffs.Add(effect);
        if (effect.effectTitle != String.Empty)
        {
            informer.text = effect.effectTitle;
            tim.alarm[0] = 0f;
        }
    }

    private void Vanish()
    {
        informer.text = String.Empty;
    }
}
