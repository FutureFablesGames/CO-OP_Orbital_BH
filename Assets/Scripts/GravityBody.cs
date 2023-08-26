using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityBody : MonoBehaviour
{

    // ================================================
    // COMPONENTS / COMPONENTS / COMPONENTS / COMPONEN
    // ================================================

    private Rigidbody rb;

    // ================================================
    // VARIABLES / VARIABLES / VARIABLES / VARIABLES /
    // ================================================

    public LayerMask GroundLayer;
    public float WeightMultiplier = 1.0f;

    public Vector3 Up {
        get { return transform.up; }
    }

    public bool IsGrounded {
        get { return Physics.Raycast(transform.position, -Up, 1.5f, GroundLayer); }
    }

    // ================================================
    // MONOBEHAVIOUR / MONOBEHAVIOUR/ MONOBEHAVIOUR /    
    // ================================================

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        //rb.useGravity = false;       
    }

    private void Update()
    {
        
      
    }

    // ================================================
    // FUNCTIONS / FUNCTIONS / FUNCTIONS / FUNCTIONS / 
    // ================================================
    // 
}
