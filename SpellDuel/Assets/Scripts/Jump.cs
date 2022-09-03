using FishNet.Object;
using UnityEngine;

public class Jump : NetworkBehaviour
{
    public Rigidbody Rig;
    public bool isGrounded;
    private void Update()
    {
        if (!IsOwner) return;
        if (isGrounded)
        {if (Input.GetKeyDown(KeyCode.Space)) //|| Input.GetKeyDown(KeyCode.W)
        {
            Rig.AddForce(0f,5f,0f,ForceMode.Impulse);
        }}
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Ground")) isGrounded = true;
        Debug.Log(isGrounded);
    }

    private void OnCollisionExit(Collision other)
    {
        if(other.gameObject.CompareTag("Ground")) isGrounded = false;
        Debug.Log(isGrounded);
    }
}
