using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpell : OffensiveSpell
{ 
        public FireSpell(Transform _transform, Transform _dir)
        { 
                VFX = Resources.Load("PS_FireBall") as GameObject;
                ImpactVFX = null;
                lifespan = 3f;
                speed = 8f;
                PM = 0.6f; //Power Multiplier
                cost = 2f;
                Element = Elements.Fire;
                ActiveCol = "#ff0000ff";
                InactiveCol = "#800000ff";
                controllable = true;
                transform = _transform;
                dir = _dir;
        }
}
