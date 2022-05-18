using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Costs",fileName = "Costs_")]
public class SpellCosts : ScriptableObject
{
    public Dictionary<string, int> costs;

    private void Awake()
    {
        if (costs != null)
            return;
        costs = new Dictionary<string, int>();
        costs.Add("Fire",5);
        costs.Add("Ice",10);
        costs.Add("Thunder",35);
        costs.Add("Doom",8);
        costs.Add("Whisper",20);
        costs.Add("Earth",10);
        costs.Add("Water",15);
        costs.Add("Darkness",25);
        costs.Add("Impulse",5);
        costs.Add("Presto",5);
        costs.Add("Check",25);
        costs.Add("Shield",10);
        costs.Add("Vanish",15);
        costs.Add("Paralysis",12);
    }
}
