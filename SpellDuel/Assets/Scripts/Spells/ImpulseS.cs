using System.Threading.Tasks;
using FishNet.Object;
using UnityEngine;
public class ImpulseS: Spell
{
    private bool trigger;
    //private Vector3 dir;
    private Rigidbody rb;
    private Collider col;
    private float impulseForce;
    private Timers tim;
    [SerializeField] private AnimationCurve curve;
    
    private void Awake()
    {
        PM = 1.2f; //Power Multiplier
        Element = Elements.NonElemental;
        cooldown = 3f;
        impulseForce = 13f;
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        if(!IsOwner) return;
        TakeOver();
    }

    private void Update()
    {
        if (!IsOwner) return;
        if (rb == null) return;
        
        if (trigger)
        {
            //_subject.transform.localRotation = _conjure.ori.rotation;
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Yeeted and deleted");
                _conjure.stateManager.SwitchBattleState(_conjure.stateManager.staffPush);
                trigger = false;
                Push();
                    
                //rb.AddForce(_subject.transform.forward*12f,ForceMode.Impulse);
            }
        }
    }

    
    [ServerRpc]
    private async void Push()
    {
        await Task.Delay(500);
        Vector3 dir = (_conjure.aimAt.pointer.position - rb.position);
        dir = dir.normalized * _conjure.stats.str*impulseForce;
        rb.isKinematic = false;
        rb.AddForce(dir,ForceMode.Impulse);
        rb.useGravity = true;
        Despawn();
    }

    [ServerRpc]
    private void TakeOver()
    {
        Debug.Log("Raycast thrown");
        Physics.Raycast(_conjure.ori.position, _conjure.ori.forward, out RaycastHit hit);
        
        if (!hit.collider.CompareTag("Controllable")) Despawn();
        if (hit.collider.TryGetComponent(out rb))
        {
            Debug.Log("Gotcha now!");
            //rb.isKinematic = false;
            PM = rb.mass; //POTENTIAL CAUSE OF NOT RECEIVING DAMAGE
            //rb.useGravity = false;
            _conjure.stateManager.SwitchBattleState(_conjure.stateManager.objectLift);
            trigger = true;
            Lift(rb.position,rb.position+(Vector3.up*2f),0.4f);

        }
    }
    
    private async void Lift(Vector3 initialPos, Vector3 finalPos, float duration)
    {
        if (!IsOwner) return;
        float elapsedTime = 0f;
        float completeness = 0f;
        rb.isKinematic = false;
        rb.useGravity = false;
        
        while (completeness < 1f && trigger)
        {
            completeness = elapsedTime / duration;
            //rb.position = Vector3.Lerp(initialPos, finalPos, curve.Evaluate(completeness));
            rb.MovePosition(Vector3.Lerp(initialPos, finalPos, curve.Evaluate(completeness)));
            elapsedTime += Time.deltaTime;
            await Task.Yield();
            Debug.Log(completeness);
        }

        if(trigger) rb.isKinematic = true;
    }
    
}
