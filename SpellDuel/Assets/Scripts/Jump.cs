using FishNet.Object;
using UnityEngine;

public class Jump : NetworkBehaviour
{
    public Rigidbody Rig;
    public bool isGrounded;
    [SerializeField]private float impulseForce;

    private void Awake()
    {
        Rig = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!IsOwner) return;
        if (!isGrounded) return;
        if (Input.GetKeyDown(KeyCode.Space) && (Rig !=null)) //|| Input.GetKeyDown(KeyCode.W)
        {
            Rig.AddForce(0f,impulseForce,0f,ForceMode.Impulse);
        }
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground")) isGrounded = true;
        Debug.Log(isGrounded);
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Ground")) isGrounded = false;
        Debug.Log(isGrounded);
    }
    
}
