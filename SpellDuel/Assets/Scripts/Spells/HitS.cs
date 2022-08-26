using UnityEngine;

public class HitS : Spell,IShootable
{
    private void Awake()
    {
        cooldown = 0.3f;
        speed = 18f;//tis 18f
        lifespan = 3f;
        PM = 0.5f;
        Element = Elements.NonElemental;
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        
        if (!IsOwner) return;
        StartCoroutine(DestroyAfter(lifespan));
        Debug.Log(transform.rotation);
        Debug.Log(_conjure.aimAt.rot);
    }

    private void Update()
    {
        if (!IsOwner) return;
        Shoot();
        //Control();
    }

    private void OnTriggerEnter(Collider other)
    {
        Clash(other);
    }
    
    public void Shoot()
    {
        transform.Translate(speed * Time.deltaTime * Vector3.forward);
    }
}
