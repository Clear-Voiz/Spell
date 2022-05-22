using System;
using FishNet.Object;
using UnityEngine;

public sealed class PawnMovement : NetworkBehaviour
{
    [SerializeField] private float speed;

    [SerializeField] private float jumpSpeed;

    [SerializeField] private float gravityScale;

    private CharacterController _characterController;

    private PawnInput _input;

    private Vector3 _velocity;

    public override void OnStartNetwork()
    {
        base.OnStartNetwork();
        _characterController = GetComponent<CharacterController>();
        _input = GetComponent<PawnInput>();
    }

    private void Update()
    {
        if (!IsOwner) return;

        Vector3 desiredVelocity =
            Vector3.ClampMagnitude(
                speed * ((_input.vertical * transform.forward) + (_input.horizontal * transform.right)), speed);
        _velocity.x = desiredVelocity.x;
        _velocity.z = desiredVelocity.z;

        if (_characterController.isGrounded)
        {
            _velocity.y = 0f;
            if (_input.jump)
            {
                _velocity.y = jumpSpeed;
            }
        }
        else
        {
            _velocity.y += Time.deltaTime * Physics.gravity.y * gravityScale;
        }

        _characterController.Move(Time.deltaTime * _velocity);
    }
}
