using FishNet.Object;
using UnityEngine;

public class Jump : NetworkBehaviour
{
    public Rigidbody Rig;
    private void Update()
    {
        if (!IsOwner) return;
        if (Input.GetKeyDown(KeyCode.Space)) //|| Input.GetKeyDown(KeyCode.W)
        {
            Rig.AddForce(0f,5f,0f,ForceMode.Impulse);
        }
    }
}
