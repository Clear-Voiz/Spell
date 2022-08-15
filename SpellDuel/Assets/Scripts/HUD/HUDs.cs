using System;
using FishNet.Object;
using TMPro;
using UnityEngine;

public class HUDs : NetworkBehaviour
{
    public TextMeshProUGUI toDisplay;
    public static event Action<HUDs> OnExisting;
    public Stats ownerStats;
    public Stats contendantStats;
    private Timers tim;
    private float waitTime;
    public GameObject finalPanel;
    [SerializeField]private TextMeshProUGUI result;
    

    private void Awake()
    {
        toDisplay = GetComponent<TextMeshProUGUI>();
    }
    private void Start()
    {
        tim = new Timers(2);
        waitTime = 1f;
        tim.alarm[1] = waitTime;
        SetHUDStats();
        
        //Displayer.text = "<color=green>HP</color>: " + stats[0].hp +"\n<color=purple>MP</color>: "+stats[0].mt;
    }

    private void OnEnable()
    {
        OnExisting?.Invoke(this);
        Stats.OnEnd += EndFightCinematic;
    }

    private void OnDisable()
    {
        Stats.OnEnd -= EndFightCinematic;

    }
    
    

//optimitzar aquest update
    private void Update()
    {
        if (ownerStats != null && contendantStats!=null)
        {
            toDisplay.text = "HP: " + ownerStats.hp +"\nMP: " + ownerStats.mt + "\nOwner: " + ownerStats.OwnerId
                + "\nRHP: " + contendantStats.hp + "\nRMT = " + contendantStats.mt;
            
        }
        /*else
        {
            print("not connected to the stats");
        }*/
        //tim.alarm[0] = tim.Timer(1f, tim.alarm[0], MagicRecovery);
        tim.alarm[1] = tim.Timer(waitTime, tim.alarm[1], ActivateFinalPanel);
    }

    [ObserversRpc]
    public void SetHUDStats()
    {
        
        for (int i = 0; i < GameManager.Instance.players.Count; i++)
        {
            if (GameManager.Instance.players[i].IsOwner)
            {
                ownerStats = GameManager.Instance.players[i].controlledPawn.GetComponent<Stats>();
            }
            else
            {
                contendantStats = GameManager.Instance.players[i].controlledPawn.GetComponent<Stats>();
            }
        }
    }
    
    /*private void Awake()
    {
        /*if (GameObject.Find("HUD_Display").GetComponent<TextMeshProUGUI>() != null)
        {
            Displayer = GameObject.Find("HUD_Display").GetComponent<TextMeshProUGUI>();
        }#1#

    }*/

    

    public float Mt
    {
        get
        {
            if (ownerStats != null) return ownerStats.mt;
            else
                return 100;
        }
        set
        {
            if (ownerStats != null)
            {
                ownerStats.mt = value;
                //toDisplay.text = "<color=green>HP</color>: " + ownerStats.hp +"\n<color=purple>MP</color>: "+ownerStats.mt;
            }
            
        }
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
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    /*public void Rematch()   reactivate once you solve all the camera problems
    {
        finalPanel.SetActive(false);
        secondaryCam.SetActive(false);
        primaryCam.SetActive(true);
        foreach (var col in stats)
        {
            col.ResetPlayerStats();
        }
    }*/

    public void EndFightCinematic(Stats stats)
    {
        tim.alarm[1] = 0f;
    }

    private void ActivateFinalPanel()
    {
        UIManager.Instance.Show<FinalView>();
        if (ownerStats.HP > 0f)
        {
            result.text = "Victory";
        }
        else
        {
            result.text = "<color=blue>Defeat</color>";
        }
    }
   
}
