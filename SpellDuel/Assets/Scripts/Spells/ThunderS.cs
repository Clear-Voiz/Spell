using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

public class ThunderS : Spell
{
    private VisualEffect _bolt;
    public float range;
    private const string LENGTH = "Length";

    private void Awake()
    {
        cooldown = 1f;
        cost = 2f;
        lifespan = 0.2f;
        Element = Elements.Thunder;
        PM = 1.2f; //Power Multiplier
        _bolt = GetComponentInChildren<VisualEffect>();
        lifespan = _bolt.GetFloat("LifeSpan");
        
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        ImpactVFX = null;
        

        if (!IsOwner) return;
        Strike();

    }


    private void Strike()
    {
        Physics.Raycast(_conjure.ori.position, _conjure.ori.forward, out RaycastHit hit);
        if (hit.collider != null)
        {
            //Debug.Log("Clashed actually");
            
            Clash(hit.collider);
            
            
            range = hit.distance;
            
            if (hit.collider.TryGetComponent(out Spell spell))
            {
                if (spell is ShieldS)
                {
                    if (!IsOwner) return;
                    BonusPoints(LocalConnection);
                    spell.Despawner();
                }
            }
            
        }
        else
        {
            range = 30f;
        }
        
        float length = range / _bolt.GetFloat(LENGTH);

        transform.localScale = new Vector3(1, 1f, length);
        //_bolt.SetFloat("Length",range);
        //_bolt.SetFloat("Pivoter",-4.4f);

       
        StartCoroutine(Cine_Shake.Instance.shakeCamera(3f, lifespan-0.1f));
        StartCoroutine(Cleaner());
    }
    
    private IEnumerator Cleaner()
    {
        yield return new WaitForSeconds(lifespan);
        Despawner();
    }
}
