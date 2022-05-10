using System;
using TMPro;
using UnityEngine;

public class HUD_Displayer : MonoBehaviour
{
    private TextMeshProUGUI Displayer;
    private Timers tim;

    private void Awake()
    {
        if (GameObject.Find("HUD_Display").GetComponent<TextMeshProUGUI>() != null)
        {
            Displayer = GameObject.Find("HUD_Display").GetComponent<TextMeshProUGUI>();
        }
    }

    public float Mt
    {
        get => Globs.mt;
        set
        {
            Globs.mt = value; 
            Debug.Log(Displayer);
            Displayer.text = "<color=green>HP</color>: " + Globs.hp.Value +"\n<color=purple>MP</color>: "+Globs.mt;
        }
    }


    private void Start()
    {
        tim = new Timers(1);
        
        Displayer.text = "<color=green>HP</color>: " + Globs.hp.Value +"\n<color=purple>MP</color>: "+Globs.mt;
    }
    
    private void Update()
    {
        tim.alarm[0] = tim.Timer(1f, tim.alarm[0], MagicRecovery);
    }

    private void MagicRecovery()
    {
        if (Mt > 0f)
        {
            Mt -= 1f;
        }

        tim.alarm[0] = 0f;
    }
}
