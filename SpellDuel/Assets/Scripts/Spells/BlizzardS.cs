using FishNet.Object;
using FishNet.Object.Synchronizing;
using UnityEngine;
using Random = UnityEngine.Random;

public class BlizzardS : Spell
{
    private float interval;
    private IceShardS iceShard;
    private Timers tim;
    private int repetitions = 10;

    private void Awake()
    {
        cooldown = 10f;
        interval = 0.2f;
        //iceShard = _conjure.SH.ice;

    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        
        tim = new Timers(1);
    }

   

    private void Update()
    {
        if(!IsOwner) return;
        Vector3 place = new Vector3(Random.Range(_conjure.ori.position.x - 2f, _conjure.ori.position.x + 2f),
            Random.Range(_conjure.ori.position.y + 1f, _conjure.ori.position.y + 3f), _conjure.ori.position.z +(_conjure.ori.forward.z*1.5f));
        tim.alarm[0] = tim.Cicle(interval, tim.alarm[0], Freezer,place, ref repetitions);
    }

    [ServerRpc]
    private void Freezer(Vector3 vec)
    {
        IceShardS shard = Instantiate(_conjure.SH.ice, vec, Quaternion.identity);
        shard._conjure = _conjure;
        Spawn(shard.gameObject,Owner);
        Debug.Log(repetitions);
        if (repetitions < 1)
        {
            Despawner();
        }
    }
}