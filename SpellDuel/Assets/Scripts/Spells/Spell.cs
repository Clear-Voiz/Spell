using System;
using System.Collections;
using FishNet.Connection;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using UnityEngine;

[Flags]
public enum Elements{NonElemental = 0,Fire = 1,Ice = 2,Thunder = 4,Earth = 8,Water = 16,Wind = 32,Light = 64, Dark= 128}

public abstract class Spell: NetworkBehaviour
{
    //var definitions
    protected GameObject ImpactVFX;
    protected float lifespan;
    public float speed;
    protected float PM; //Power Multiplier
    protected float cost;
    public Elements Element;
    protected string ActiveCol;
    protected string InactiveCol;
    protected GameObject LP;
    
    [SyncVar]
    public Conjure _conjure;

    
    protected void Damager(Collider other)
    {
        if (!IsOwner) return;
        if (other.CompareTag("Player"))
        {
            NetworkObject nob;
            if (other.TryGetComponent(out nob))
            {
                if (nob.IsOwner == false)
                {
                    speed = 0f;
                    var stats = other.gameObject.GetComponent<Stats>();
                    var tmpdmg = (Globs.mgk.Value - stats.mgkDef.Value) * PM * stats.RES[Element];
                    int damage = Mathf.RoundToInt(tmpdmg);
                    if (damage < 0)
                    {
                        damage = 1;
                    }
                    
                    Inflict(stats,damage, LocalConnection);
                }
            }
        }

        else
        {
            if (!other.CompareTag("Spell"))
            {
                speed = 0f;
                //Fade Away (not necessary, the spell should play its animation effect and THEN fade away. Meanwhile it just stays in place until the end of its lifespawn)
            }
        }
    }

    [ServerRpc]
    public void Despawner()
    {
        Despawn();
    }
    
    protected IEnumerator DestroyAfter(float t)
    {
        yield return new WaitForSeconds(t);
        Despawn();
    }

    [ServerRpc]
    protected void Inflict(Stats stats, int damage, NetworkConnection conn)
    {
        if (stats.RES[Element] > 1f)
        {
            BonusPoints(conn);
        }
        
        if (stats.HP > damage)
        {
            stats.HP -= damage;
            Debug.Log(
                stats.hp + 
                " mgk:" + Globs.mgk.Value + 
                ", mgkDef" + stats.mgkDef.Value + 
                ", PM" + PM + 
                ", RES" + stats.RES[Element]
            );
        }
        else
        {
            stats.HP = 0f;
            stats.SettleGame(conn);
            //Destroy(other.gameObject);
        }
    }

    [ObserversRpc]
    protected void BonusPoints(NetworkConnection conn)
    {
       
            LP = _conjure.SH.LP;
            GameObject lp = Instantiate(LP, transform.position, Quaternion.identity);
            if (lp.TryGetComponent(out LocalPoints slp))
            {
                slp.conn = conn;
            }
    }

}
