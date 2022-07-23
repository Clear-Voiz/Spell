using System.Threading.Tasks;
using FishNet.Object;
using UnityEngine;

public class TD_Movement : NetworkBehaviour
{
    public Transform player;
    public Rigidbody rb;
    [SerializeField] private AnimationCurve curve;
    private Timers tim;
    private bool canDash;
    private float cooldown;

    private void Start()
    {
        tim = new Timers(1);
        canDash = true;
        cooldown = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner) return;
        var horizontal = Input.GetAxisRaw("Horizontal");
        var vertical = Input.GetAxisRaw("Vertical");
        var movement = new Vector3(horizontal, 0f, vertical);

        if (movement.magnitude >= 0.1f)
        {
            var angle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg; //radianes a grados
            player.localPosition += (Globs.spd.Value * Time.deltaTime * movement);
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            Globs.Xp += 1;
            print(Globs.Xp);
        }

        if (Input.GetKeyDown(KeyCode.E) && canDash)
        {
            Evade(player.position,player.position+(Vector3.right*Globs.spd.Value),0.4f);
            canDash = false;
        }
        if (Input.GetKeyDown(KeyCode.Q) && canDash)
        {
            Evade(player.position,player.position+(Vector3.right*-Globs.spd.Value),0.4f);
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

    private async void Evade(Vector3 initialPos, Vector3 finalPos, float duration)
    {
        if (!IsOwner) return;
        float elapsedTime = 0f;
        float completeness = 0f;
        while (elapsedTime / duration < 1f)
        {
            completeness = elapsedTime / duration;
            player.position = Vector3.Lerp(initialPos, finalPos, curve.Evaluate(completeness));
            elapsedTime += Time.deltaTime;
            await Task.Yield();
        }
    }
}
