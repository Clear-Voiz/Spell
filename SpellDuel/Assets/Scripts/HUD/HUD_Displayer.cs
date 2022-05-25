﻿using System;
using FishNet.Object;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HUD_Displayer : MonoBehaviour
{
    private TextMeshProUGUI Displayer;
    private Timers tim;
    public GameObject finalPanel;
    public GameObject secondaryCam;
    public GameObject primaryCam;
    private Transform looser;
    private float waitTime;
    private Stats[] stats;
    [SerializeField]private TextMeshProUGUI result;

    private void Awake()
    {
        /*if (GameObject.Find("HUD_Display").GetComponent<TextMeshProUGUI>() != null)
        {
            Displayer = GameObject.Find("HUD_Display").GetComponent<TextMeshProUGUI>();
        }*/

    }

    private void OnEnable()
    {
        Stats.OnDefeat += EndFightCinematic;
        Stats.spreadStats += GetPlayerStats;
    }

    private void OnDisable()
    {
        Stats.OnDefeat -= EndFightCinematic;
        Stats.spreadStats -= GetPlayerStats;
    }

    public float Mt
    {
        get
        {
            if (stats[0] != null) return stats[0].mt;
            else
                return 100;
        }
        set
        {
            if (stats[0] != null)
            {
                stats[0].mt = value;
                Displayer.text = "<color=green>HP</color>: " + stats[0].hp +"\n<color=purple>MP</color>: "+stats[0].mt;
            }
            
        }
    }


    private void Start()
    {
        tim = new Timers(2);
        waitTime = 1f;
        tim.alarm[1] = waitTime;
        
        Displayer.text = "<color=green>HP</color>: " + stats[0].hp +"\n<color=purple>MP</color>: "+stats[0].mt;
    }
    
    private void Update()
    {
        tim.alarm[0] = tim.Timer(1f, tim.alarm[0], MagicRecovery);
        tim.alarm[1] = tim.Timer(waitTime, tim.alarm[1], ActivateFinalPanel);
    }

    private void MagicRecovery()
    {
        if (Mt > 0f)
        {
            Mt -= 1f;
        }

        tim.alarm[0] = 0f;
    }

    public void Continue()
    {
        SceneManager.LoadScene(0);
    }

    public void Rematch()
    {
        finalPanel.SetActive(false);
        secondaryCam.SetActive(false);
        primaryCam.SetActive(true);
        foreach (var col in stats)
        {
            col.ResetPlayerStats();
        }
    }

    public void EndFightCinematic(Transform trans)
    {
        looser = trans;
        tim.alarm[1] = 0f;
    }

    private void ActivateFinalPanel()
    {
        finalPanel.SetActive(true);
        if (looser.CompareTag("Enemy"))
        {
            result.text = "Victory";
        }
        else
        {
            result.text = "<color=blue>Defeat</color>";
        }
    }

    
    private void GetPlayerStats(Stats _stats)
    {
        if (_stats.gameObject.TryGetComponent(out NetworkObject nob))
        {
            if (nob.IsOwner)
                stats[0] = _stats;
            else
                stats[1] = _stats;
        }
    }
}
