using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUD_Displayer : MonoBehaviour
{
    private TextMeshProUGUI Displayer;

    private void Awake()
    {
        
    }

    private void Start()
    {
        if (GameObject.Find("HUD").GetComponent<TextMeshProUGUI>() == null)
        {
           return;
        }
        else
        {
            Displayer = GameObject.Find("HUD").GetComponent<TextMeshProUGUI>();
        }

        Displayer.text = "<color=green>HP</color>: " + Globs.hp.Value +"\n<color=purple>MP</color>: "+Globs.mp.Value;
    }
}
