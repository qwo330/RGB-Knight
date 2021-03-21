using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : StateController
{
    [SerializeField] private float jumpSpeed;

    private Player player;
    private Animator animator;
    private new Rigidbody2D rigidbody;

    private bool canJump;

    private void Awake()
    {
        player = GetComponentInParent<Player>();
        animator = player.GetComponent<Animator>();
        rigidbody = player.GetComponent<Rigidbody2D>();
        canJump = true;
    }

    private void OnEnable()
    {
        player.Jump(jumpSpeed);
        if (canJump)
        {
            animator.SetTrigger("Jump");
            animator.SetBool("Jumping", true);
        }
    }

    private void Update()
    {
        player.MoveForward(Input.GetAxis("Horizontal"));

        if (canJump && Input.GetButtonDown("Jump"))
        {
            canJump = false;
            InvokeTransition(typeof(PlayerJump));
        }
        else if (rigidbody.velocity.y <= 0.0f && player.Grounded)
        {
            canJump = true;
            animator.SetBool("Jumping", false);
            InvokeTransition(typeof(PlayerMovement));
        }
    }
}