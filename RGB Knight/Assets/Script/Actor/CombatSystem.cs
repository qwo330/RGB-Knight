using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatSystem : MonoBehaviour
{
    private enum State
    {
        None,
        Attack,
        Parry,
    }

    private Actor actor;
    private State state = State.None;

    private void Awake()
    {
        actor = GetComponent<Actor>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var other = collision.GetComponentInParent<Actor>();
        
        if (other == null || other._EColor != actor._EColor)
            return;

        switch (state)
        {
            case State.Attack:
                other.Attacked();
                state = State.None;
                break;
            case State.Parry:
                other.Parried();
                state = State.None;
                break;
        }
    }

    private IEnumerator ApplyDirectingWhenParried()
    {
        Time.timeScale = 0.1f;
        yield return new WaitForSeconds(0.02f);
        Time.timeScale = 1.0f;
    }


    private void OnAttackStart()
    {
        state = State.Attack;
    }

    private void OnAttackFinish()
    {
        state = State.None;
    }

    private void OnParryingStart()
    {
        if (state == State.Attack)
            state = State.Parry;
    }

    private void OnParryingFinish()
    {
        if (state == State.Parry)
            state = State.Attack;
    }
}