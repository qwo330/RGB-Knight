using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : Actor
{
    [SerializeField] private new SpriteRenderer renderer;
    [SerializeField] private Animator animator;
    [SerializeField] private new Rigidbody2D rigidbody;
    [SerializeField] private WalkableDetector walkableDetector;
    [SerializeField] private UnityEvent damageEvent;

    public bool Grounded => walkableDetector.IsDetected;
    public bool Invincible { get; set; }

    private void Update()
    {
        animator.SetFloat("ForwardSpeed", Mathf.Abs(rigidbody.velocity.x));
        animator.SetFloat("VerticalSpeed", rigidbody.velocity.y);
        animator.SetBool("Grounded", walkableDetector.IsDetected);
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

    public void Stop()
    {
        rigidbody.velocity = Vector2.zero;
    }


    public void Jump(float speed)
    {
        var velocity = rigidbody.velocity;
        velocity.y = speed;
        rigidbody.velocity = velocity;
    }


    public override void Attacked()
    {
        if (Invincible)
            return;

        --_HP;

        if (_HP <= 0)
        {
            Debug.Log("Dead...");
            return;
        }

        StartCoroutine(ReactToDamage());
    }

    private IEnumerator ReactToDamage()
    {
        Invincible = true;

        var startTime = Time.time;
        var originColor = renderer.color;

        yield return new WaitWhile(() =>
        {
            var elapsedTime = Time.time - startTime;
            var color = renderer.color;
            color.a = Mathf.Sin(Time.time);
            renderer.color = color;
            return Time.time - startTime < 0.1f;
        });

        renderer.color = originColor;
        Invincible = false;
    }
}