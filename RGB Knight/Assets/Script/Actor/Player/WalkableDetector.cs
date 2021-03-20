using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkableDetector : MonoBehaviour
{
    [SerializeField] private Collider2D boundary;
    [SerializeField] private LayerMask targets;

    public bool IsDetected { get; private set; }

    private void Update()
    {
        IsDetected = Physics2D.IsTouchingLayers(boundary, targets);
    }
}
