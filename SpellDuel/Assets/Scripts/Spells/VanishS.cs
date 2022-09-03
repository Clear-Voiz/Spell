using System.Threading.Tasks;
using FishNet.Object;
using UnityEngine;

public class VanishS : Spell
{
    /*
    private Material curMat;
    private Material mat;
    private SkinnedMeshRenderer meshRend;
    private Material inv;
    private float time;

    private Texture texture;

    public override void OnStartServer()
    {
        base.OnStartServer();
        
        Debug.Log(_conjure.playerMesh);
        
        if (_conjure.playerMesh.TryGetComponent(out meshRend))
        {
            //meshRend = GameObject.Find("Player1_mesh").GetComponent<MeshRenderer>();
            mat = meshRend.material;
            Debug.Log("meshrend: "+meshRend);
            //texture = mat.GetTexture(0);
            inv = Resources.Load("DissolveMat") as Material;
            meshRend.material = inv;
            //inv.SetTexture(0,texture);
            Debug.Log(meshRend);
            //var skin = meshRend.material.mainTexture;
        }

        tim = new Timers(3);
    }
    

    private void Update()
    {
        if(!IsOwner) return;
        Effect();
    }
    
    [ServerRpc]
    public void Effect()
    {
        tim.alarm[0] = tim.Chronometer(1f,tim.alarm[0], Disappear);
        
        if (tim.alarm[0] < 1f)
        {
            await Task.Yield();
            tim.alarm[1] = tim.Timer(3f,tim.alarm[1]);
        }

        if (tim.alarm[1] == 3f)
        {
            tim.alarm[2] = tim.Chronometer(1f,tim.alarm[2], Reappear);
        }
    }
    
    
    [ObserversRpc]
    public void Reappear()
    {
        var temp = 1f - tim.alarm[2];
        meshRend.material.SetFloat("Time_", temp);
        while (tim.alarm[2] < 1f)
        {
            await Task.Yield();
        }
        meshRend.material = mat;
        if(!IsOwner) return;
        Despawner();
    }
    
    [ObserversRpc]
    private void Disappear()
    {
        //Debug.Log("vanished");
        float disValue = tim.alarm[0];
        meshRend.material.SetFloat("Time_",disValue);
        while (tim.alarm[0] < 1f)
        {
            
        }
        meshRend.material.SetFloat("Time_",1f);
    }
    */

}
