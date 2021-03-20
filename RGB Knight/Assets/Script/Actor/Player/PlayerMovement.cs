using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : StateController
{
    private Player player;

    private void Awake()
    {
        player = GetComponentInParent<Player>();
    }

    private void Update()
    {
        var forward = Input.GetAxisRaw("Horizontal");
        var up = Input.GetAxisRaw("Vertical");

        player.MoveForward(forward);

        if (Input.GetButtonDown("Jump"))
        {
            if (up < 0)
                DownPlatform();
            else
                InvokeTransition(typeof(PlayerJump));
        }

        if (Input.GetButtonDown("Dash"))
            InvokeTransition(typeof(PlayerDash));
        if (Input.GetMouseButtonDown(0))
            InvokeTransition(typeof(PlayerAttack));
        //if (Input.GetMouseButtonDown(1))
        //    InvokeTransition(typeof(PlayerColorSelect));
    }

    private void DownPlatform()
    {
        if (!player.Grounded)
            return;

        var hits = Physics2D.RaycastAll(transform.position, Vector2.down);
        foreach (var hit in hits)
        {
            var platform = hit.transform.GetComponent<PlatformEffector2D>();
            if (platform != null)
            {
                platform.rotationalOffset = 180;
                break;
            }
        }
    }
}