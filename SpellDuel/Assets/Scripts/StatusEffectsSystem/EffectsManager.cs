using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EffectsManager : MonoBehaviour
{
    public List<AlterSpell> ActiveEffects;
    public TextMeshProUGUI informer;
    private Timers tim;
    private float duration;

    private void Awake()
    {
        tim = new Timers(1);
    }

    void Start()
    {
        ActiveEffects = new List<AlterSpell>();
        duration = 1f;
        tim.alarm[0] = duration;
    }

    private void OnEnable()
    {
        AlterSpell.onEnd += RemoveEffect;
        AlterSpell.onStart += AddEffect;
    }

    private void OnDisable()
    {
        AlterSpell.onEnd -= RemoveEffect;
        AlterSpell.onStart -= AddEffect;
    }

    
    void Update()
    {
        if (tim.alarm[0] < duration) 
            tim.alarm[0] = tim.Timer(duration, tim.alarm[0], Vanish);
        
        if (ActiveEffects.Count <= 0) return;
        
        for (int i = ActiveEffects.Count-1; i >=0 ; i--) //never change to foreach or might cause errors
        {
            ActiveEffects[i].Effect();
            if (ActiveEffects.Count <= 0)
            {
                return;
            }
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            print(ActiveEffects.Count);
        }
    }

    public void RemoveEffect(AlterSpell effect)
    {
        ActiveEffects.Remove(effect);
    }
    
    public void AddEffect(AlterSpell effect)
    {
        ActiveEffects.Add(effect);
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
