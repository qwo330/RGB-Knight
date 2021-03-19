using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Actor
{
    public override void Move()
    {
        //base.Move();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("mon trigger enter");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("mon collider enter");

    }
}
