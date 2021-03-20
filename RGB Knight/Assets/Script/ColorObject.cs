using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorObject : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Util.Log("obj coll enter");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Util.Log("obj triger enter");
        
    }
}

/*
 시멘트, 아스팔트 바닥
철근(가시) 발판
무너진 구조물
파괴가능 오브젝트

세이브 포인트
컬러 아이템
움직이는 발판
     */
