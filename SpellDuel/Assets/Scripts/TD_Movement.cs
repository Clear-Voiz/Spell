using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class TD_Movement : MonoBehaviour
{
    public Transform player;
    public Rigidbody rb;
    [SerializeField] private AnimationCurve curve;
    
    // Update is called once per frame
    void Update()
    {
        var horizontal = Input.GetAxisRaw("Horizontal");
        var vertical = Input.GetAxisRaw("Vertical");
        var movimiento = new Vector3(horizontal, 0f, vertical);

        if (movimiento.magnitude >= 0.1f)
        {
            var angle = Mathf.Atan2(movimiento.x, movimiento.z) * Mathf.Rad2Deg; //radianes a grados
            player.position += (movimiento * Globs.spd.Value * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            Globs.Xp += 1;
            print(Globs.Xp);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Evade(player.position,player.position+(Vector3.right*Globs.spd.Value),0.4f);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Evade(player.position,player.position+(Vector3.right*-Globs.spd.Value),0.4f);
        }
    }

    private async void Evade(Vector3 initialPos, Vector3 finalPos, float duration)
    {
        float elapsedTime = 0f;
        float completeness = 0f;
        while (elapsedTime / duration < 1)
        {
            completeness = elapsedTime / duration;
            player.position = Vector3.Lerp(initialPos, finalPos, curve.Evaluate(completeness));
            elapsedTime += Time.deltaTime;
            await Task.Yield();
        }
    }
}
