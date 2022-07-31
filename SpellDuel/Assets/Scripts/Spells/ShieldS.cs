using UnityEngine;

public class ShieldS : Spell
{
    public override void OnStartClient()
    {
        base.OnStartClient();
        lifespan = 5f;
        StartCoroutine(DestroyAfter(lifespan));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!IsOwner) return;
        if (other.CompareTag("Spell"))
        {
            if (other.TryGetComponent(out Spell spell))
            {
                if (spell.Element == Elements.NonElemental)
                {
                    spell.speed = 0f;
                    //Destroy(other.gameObject);
                }
                else
                {
                    BonusPoints(Owner);
                    Despawner();
                }
            }
            else
            {
                Debug.Log("false");
            }
        }
    }
}
