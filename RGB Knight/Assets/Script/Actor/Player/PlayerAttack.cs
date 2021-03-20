using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : StateController
{
    private Player player;
    private Animator animator;

    private void Awake()
    {
        player = GetComponentInParent<Player>();
        animator = player.GetComponent<Animator>();
    }

    private void OnEnable()
    {
        animator.ResetTrigger("Attack");
        animator.SetTrigger("Attack");
    }

    public void OnHit(GameObject target)
    {
        var enemy = target.GetComponentInParent<Monster>();
        enemy.Attacked();
    }
}