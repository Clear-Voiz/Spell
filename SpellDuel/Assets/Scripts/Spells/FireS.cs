using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireS : OffensiveSpell
{
        public static string[] definition = {"Fire</color></b>: casts a flame controllable with your wand.","Firing</color></b>: Increased size and damage.","Fired</color></b>: Explodes when colliding or manually by left-pressing your wand."};
        
        public FireS(Transform _transform, Transform _dir)
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
