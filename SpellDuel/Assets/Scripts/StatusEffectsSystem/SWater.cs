using UnityEngine;

public class SWater : AlterSpell
{
    private void Awake()
    {
        IsBuff = true;
        tim = new Timers(2);
        lifespan = 1f;
    }

    public override void OnStartServer()
    {
        base.OnStartServer();
        if (!IsServer) return;
        Effect();
        
    }


    public override void Effect()
    {
        var EM = _conjure.effectsManager.ActiveDebuffs;
        if (EM.Count > 0)
        {
            var removedEffects = 0f;
            for (int i = _conjure.effectsManager.ActiveDebuffs.Count-1; i >= 0; i--)
            {
                if (EM[i].IsBuff==false)
                {
                    EM[i].EndEffect();
                    removedEffects += 1f;
                }
            }
            Debug.Log(removedEffects);
        }
        
        
        //EndEffect();
    }

    public override void EndEffect()
    {
        OnEnd();
    }
}
