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
        player.MoveForward(Input.GetAxisRaw("Horizontal"));

        if (Input.GetButtonDown("Jump"))
            InvokeTransition(typeof(PlayerJump));
        if (Input.GetButtonDown("Dash"))
            InvokeTransition(typeof(PlayerDash));
        if (Input.GetMouseButtonDown(0))
            InvokeTransition(typeof(PlayerAttack));
        //if (Input.GetMouseButtonDown(1))
        //    InvokeTransition(typeof(PlayerColorSelect));
    }
}
