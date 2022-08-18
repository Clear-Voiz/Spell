using UnityEngine;

public class SDoom : AlterSpell
{
    private Elements Element;
    private float PM;
    private GameObject LP;
    private Conjure _conjure;

    public SDoom(Conjure _conjure, float pm)
    {
        this._conjure = _conjure;
        tim = new Timers(1);
        Element = Elements.NonElemental;
        PM = pm;
        _conjure.effectsManager.AddEffect(this);
        //OnStart();
    }
    public override void Effect()
    {
        tim.alarm[0] = tim.Timer(3f,tim.alarm[0], EndEffect);
    }

    public override void EndEffect()
    {
        var stats = _conjure.stats;
        var tmpdmg = (Globs.mgk.Value - stats.mgkDef.Value) * PM * stats.RES[Element];
        int damage = Mathf.RoundToInt(tmpdmg);
        
        if (damage < 0)
        {
            damage = 1;
        }
        
        if (stats.RES[Element] > 1f)
        {
            //MonoBehaviour.Instantiate(LP, _conjure.transform.position, Quaternion.identity);
        }
        
        if (stats.HP > damage)
        {
            stats.HP -= damage;
            Debug.Log(
                stats.HP + 
                " mgk:" + Globs.mgk.Value + 
                ", mgkDef" + stats.mgkDef.Value + 
                ", PM" + PM + 
                ", RES" + stats.RES[Element]
            );
        }
        else
        {
            stats.HP = 0f;
            Debug.Log(stats.HP);
            //Destroy(other.gameObject);
        }

        OnEnd();
    }
}
