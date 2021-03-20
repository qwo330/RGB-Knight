using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : StateController
{
    [SerializeField] private float jumpSpeed;

    private Player player;
    private new Rigidbody2D rigidbody;

    private bool canJump;

    private void Awake()
    {
        player = GetComponentInParent<Player>();
        rigidbody = player.GetComponent<Rigidbody2D>();
        canJump = true;
    }

    private void OnEnable()
    {
        player.Jump(jumpSpeed);
    }

    private void Update()
    {
        player.MoveForward(Input.GetAxis("Horizontal"));

        if (canJump && Input.GetButtonDown("Jump"))
        {
            InvokeTransition(typeof(PlayerJump));
            canJump = false;
        }
        else if (rigidbody.velocity.y <= 0.0f && player.Grounded)
        {
            InvokeTransition(typeof(PlayerMovement));
            canJump = true;
        }
    }
}