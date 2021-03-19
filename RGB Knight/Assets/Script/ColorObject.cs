using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorObject : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("obj coll enter");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("obj triger enter");
        
    }
}
