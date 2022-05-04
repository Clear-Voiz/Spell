using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsManager : MonoBehaviour
{
    public List<AlterSpell> ActiveEffects;
    void Start()
    {
        ActiveEffects = new List<AlterSpell>();
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

    // Update is called once per frame
    void Update()
    {
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
    }
}
