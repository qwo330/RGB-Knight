using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : StateController
{
    private Player player;

    private void Awake()
    {
        player = GetComponentInParent<Player>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
        }
    }
}
