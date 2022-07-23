using System;
using TMPro;
using UnityEngine;

public class HUDs : MonoBehaviour
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

    public void SetStats()
    {
        for (int i = 0; i < GameManager.Instance.players.Count; i++)
        {
            Debug.Log(GameManager.Instance.players.Count);
            if (GameManager.Instance.players[i].controlledPawn.IsOwner)
            {
                ownerStats = GameManager.Instance.players[i].controlledPawn.GetComponent<Stats>();
                Debug.Log("ownerStats conn" +ownerStats.OwnerId);
            }
            else
            {
                contendantStats = GameManager.Instance.players[i].controlledPawn.GetComponent<Stats>();
                Debug.Log("ContendantStats conn" + ownerStats.OwnerId);
            }
        }
    }
   
}
