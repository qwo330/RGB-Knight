using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : StateController
{
    [SerializeField] private float timeBetweenAttacks;

    private Player player;
    private Animator animator;

    private float timeSinceLastAttack;

    private readonly int AttackTag = Animator.StringToHash("Attack");

    private void Awake()
    {
        player = GetComponentInParent<Player>();
        animator = player.GetComponent<Animator>();
    }

    private void OnEnable()
    {
        player.Stop();

        var elapsedTime = Time.time - timeSinceLastAttack;
        if (elapsedTime >= timeBetweenAttacks)
        {
            animator.ResetTrigger("Attack");
            animator.SetTrigger("Attack");
            timeSinceLastAttack = Time.time;
        }
    }

    private void Update()
    {
        var motion = animator.GetCurrentAnimatorStateInfo(0);
        if (motion.tagHash != AttackTag)
            InvokeTransition(typeof(PlayerMovement));
    }

    public void OnHit(GameObject target)
    {
        var enemy = target.GetComponentInParent<Monster>();
        enemy.Attacked();
    }
}