using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellTree : MonoBehaviour
{
    public int leftPoints;
    public HashSet<int> unlockedSkills;
    public enum Spells
    {
        Fire, Firing, Fired,
        Ice, Icing, Iced,
        Earth, Earthing, Earthed,
        Thunder, Thundering, Thundered,
        Water, Watering, Watered,
        Ruin, Ruining, Runed,
        
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
