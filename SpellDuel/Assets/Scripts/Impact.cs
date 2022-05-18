using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Impact : MonoBehaviour
{
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Enemy"))
        {
            var stats = other.gameObject.GetComponent<Stats>();
            if (stats.hp > (Globs.mgk.Value - stats.mgkDef.Value))
            {
                //stats.hp -= (Globs.mgk.Value - stats.magicDef)*PM*RES;
                Debug.Log(stats.hp);
            }
            else
            {
                stats.hp = 0f;
                Debug.Log(stats.hp);
                Destroy(other.gameObject);
            }

            Destroy(gameObject);
        }
    }
}
