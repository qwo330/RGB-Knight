using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonHand : MonoBehaviour
{
    public int _Speed;

    public void Action(Vector3 pos)
    {
        transform.position = pos;

    }


}
