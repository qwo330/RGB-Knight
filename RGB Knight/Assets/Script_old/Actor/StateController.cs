using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StateController : MonoBehaviour
{
    public bool Active => gameObject.activeInHierarchy;
    public UnityAction<System.Type> InvokeTransition { get; set; }

    public void Activate()
    {
        gameObject.SetActive(true);
    }

    public void Inactivate()
    {
        gameObject.SetActive(false);
    }
}