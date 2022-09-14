using System;
using System.Threading.Tasks;
using FishNet.Object;
using UnityEngine;

public class TD_Movement : NetworkBehaviour
{
    public Transform player;
    [SerializeField] private AnimationCurve curve;
    private Timers tim;
    private bool canDash;
    private float cooldown;
    private Stats stats;
    private Rigidbody rb;

    private void Awake()
    {
        stats = GetComponent<Stats>();
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        tim = new Timers(1);
        canDash = true;
        cooldown = 1f;
    }

    private void FixedUpdate()
    {
        if (!IsOwner) return;
        
        var horizontal = Input.GetAxisRaw("Horizontal");
        var vertical = Input.GetAxisRaw("Vertical");
        var movement = new Vector3(horizontal, 0f, vertical);

        if (movement.magnitude >= 0.1f)
        {
            //var angle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg; //radianes a grados
            movement = Vector3.ClampMagnitude(movement, 1f);
            movement = transform.InverseTransformDirection(movement* stats.cSpd * Time.deltaTime);
            rb.MovePosition(rb.position + movement); 
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            Globs.Xp += 1;
            print(Globs.Xp);
        }

        if (Input.GetKeyDown(KeyCode.E) && canDash)
        {
            Evade(rb.position,rb.position+(Vector3.right*stats.cSpd),0.4f);
            canDash = false;
        }
        if (Input.GetKeyDown(KeyCode.Q) && canDash)
        {
            Evade(rb.position,rb.position+(Vector3.right*-stats.cSpd),0.4f);
            canDash = false;
        }

        if (!canDash)
        {
            tim.alarm[0] = tim.Timer(cooldown, tim.alarm[0]);
            if ((int)tim.alarm[0] == (int)cooldown)
            {
                canDash = true;
                tim.alarm[0] = 0f;
            }
        }
    }
    
    /*private void Update()
    {
        throw new NotImplementedException();
    }*/

    private async void Evade(Vector3 initialPos, Vector3 finalPos, float duration)
    {
        if (!IsOwner) return;
        float elapsedTime = 0f;
        float completeness = 0f;
        while (elapsedTime / duration < 1f)
        {
            completeness = elapsedTime / duration;
            //rb.position = Vector3.Lerp(initialPos, finalPos, curve.Evaluate(completeness));
            rb.MovePosition(Vector3.Lerp(initialPos, finalPos, curve.Evaluate(completeness)));
            elapsedTime += Time.deltaTime;
            await Task.Yield();
        }
    }
    
}
