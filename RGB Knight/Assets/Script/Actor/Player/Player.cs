using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Actor
{
    [SerializeField] private new SpriteRenderer renderer;
    [SerializeField] private Animator animator;
    [SerializeField] private new Rigidbody2D rigidbody;

    private bool grounded;

    public bool Grounded => grounded;

    private void Update()
    {
        animator.SetFloat("ForwardSpeed", Mathf.Abs(rigidbody.velocity.x));
        animator.SetFloat("VerticalSpeed", rigidbody.velocity.y);
    }

    public void MoveForward(float direction)
    {
        MoveForward(direction, _MoveSpeed);
    }

    public void MoveForward(float direction, float speed)
    {
        var velocity = rigidbody.velocity;
        velocity.x = direction * speed;
        rigidbody.velocity = velocity;

        if (direction < 0)
            renderer.flipX = true;
        else if (direction > 0)
            renderer.flipX = false;
    }

    public void Jump(float speed)
    {
        var velocity = rigidbody.velocity;
        velocity.y = speed;
        rigidbody.velocity = velocity;
        
        animator.SetTrigger("Jump");
    }

    public void OnPlatformContacted(bool value)
    {
        grounded = value;
        animator.SetBool("Grounded", value);
    }
}