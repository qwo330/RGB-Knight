using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : StateController
{
    [SerializeField] private float speed;
    [SerializeField] private float duration;
    [SerializeField] private float evasionTime;

    private Player player;
    private new Rigidbody2D rigidbody;

    private Vector3 destination;
    private Vector3 currentVelocity;
    
    private float timer;


    private void Awake()
    {
        player = GetComponentInParent<Player>();
        rigidbody = player.GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        var direction = Input.GetAxisRaw("Horizontal");
        destination = player.transform.position + Vector3.right * direction * speed * duration;

        player.Invincible = true;
        timer = 0.0f;
    }

    private void FixedUpdate()
    {
        var nextPosition = Vector3.SmoothDamp(rigidbody.position, destination, ref currentVelocity, duration);
        rigidbody.MovePosition(nextPosition);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= evasionTime)
            player.Invincible = false;
        if (timer >= duration)
            InvokeTransition(typeof(PlayerMovement));
    }
}