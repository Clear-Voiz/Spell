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
    

    private void Awake()
    {
        toDisplay = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        OnExisting?.Invoke(this);
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
    }

    [ObserversRpc]
    public void SetHUDStats()
    {
        for (int i = 0; i < GameManager.Instance.players.Count; i++)
        {
            if (GameManager.Instance.players[i].controlledPawn.IsOwner)
            {
                ownerStats = GameManager.Instance.players[i].controlledPawn.GetComponent<Stats>();
            }
            else
            {
                contendantStats = GameManager.Instance.players[i].controlledPawn.GetComponent<Stats>();
            }
        }
    }
   
}
