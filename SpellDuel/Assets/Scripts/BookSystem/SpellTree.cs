using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpellTree : MonoBehaviour
{
    public int leftPoints;
    public HashSet<int> unlockedSkills = new HashSet<int>();
    public HashSet<int> activeSpells = new HashSet<int>();
    public SO_Ficha playerStats;
    public TextMeshProUGUI leftPointsTxt;
    public GameObject seal;
    public GameObject body;
    public string[] definitions = new string[Globs.totalSpells];

    private void Awake()
    {
        seal = Resources.Load("Seal") as GameObject;
    }

    private void Start()
    {
        leftPoints = Globs.lvl;
        leftPointsTxt.text = "Available points: " + leftPoints;
        definitions[0] = "";
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            foreach (int spell in activeSpells)
            {
                print(spell);
            }
        }
    }

    public enum Spells
    {
        Fire, Firing, Fired,
        Ice, Icing, Iced,
        Earth, Earthing, Earthed,
        Thunder, Thundering, Thundered,
        Water, Watering, Watered,
        Ruin, Ruining, Ruined,
        
        Shield, Shielding, Shielded,
        Fireproof,DragonSkin,DragonMouth,
        Thermal, YetiSkin,YetiMouth,
        Insulating,UnagiSkin,UnagiMouth,
        
        Paralysis, Paralyzing, Paralyze,
        Haste, Hastening, Hastened,
        Doom, Dooming, Doomed,
        Whisper, Whispering, Whispered,
        Vanish, Vanishing, Vanished
    }
    
    public enum State
    {
        Locked,Unlocked,Active
    }
    
}
