using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefensiveSpell : Spell
{
    public override void Move()
    {
        
    }

    public override void Impact(Collision other)
    {
        if (other.collider.CompareTag("Spell"))
        {
            
            var shoot = other.collider.GetComponent<Shoot>();
            if (shoot.spell.Element != Elements.NonElemental)
            {
                MonoBehaviour.Destroy(other.gameObject);
                if (shoot.spell.Element != Elements.Thunder)
                {
                    MonoBehaviour.Destroy(transform.gameObject);
                   
                }
            }
            else
            {
                MonoBehaviour.Destroy(transform.gameObject);
            }
        }
    }
}
