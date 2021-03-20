using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EColor
{
    Red,
    Green,
    Blue,
}

public class Actor : MonoBehaviour
{
    public int _HP { get; set; }
    public int _MaxHP = 5;
    public EColor _EColor { get; set; }
    public float _MoveSpeed = 1.0f;

    void Update()
    {
        Move();
    }

    public virtual void Move()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector2.left * Time.deltaTime * _MoveSpeed);
        }
        else if(Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector2.right * Time.deltaTime * _MoveSpeed);
        }
        else if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector2.up * Time.deltaTime * _MoveSpeed);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector2.down * Time.deltaTime * _MoveSpeed);
        }
    }

    public virtual void Attcked()
    {

    }

    public virtual void Dead()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Util.Log("user trigger enter");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Util.Log("user collider enter");
        
    }
}