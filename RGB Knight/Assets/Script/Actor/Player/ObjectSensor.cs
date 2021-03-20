using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectSensor : MonoBehaviour
{
    [System.Serializable]
    private class DetectionEvent : UnityEvent<bool> { }

    [SerializeField] private LayerMask targets;
    [SerializeField] private float range;

    [Space(5)]

    [SerializeField] private DetectionEvent onDetect;
    
    private bool detected;

    private void FixedUpdate()
    {
        onDetect?.Invoke(Physics2D.OverlapCircle(transform.position, range, targets));
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
#endif
}