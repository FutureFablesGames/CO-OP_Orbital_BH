using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityBody : MonoBehaviour
{
    [Header("Components")]
    public GravityAttractor attractor;

    [Header("Variables")]
    public Vector3 Up = Vector3.zero;
    
    public float gravityMultiplier = 1.0f;  // Consider this a weight multiplier.  Heavier boys can't jump as high.

    [Header("Grounding")]
    public LayerMask WhatIsGround;
    [Range(0, 5)] public float f_GroundedDistance;
    public bool Grounded = false;

    private void Start()
    {
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        GetComponent<Rigidbody>().useGravity = false;       
        attractor = GameObject.FindGameObjectWithTag("Ground").GetComponent<GravityAttractor>();
    }

    private void Update()
    {
        // Ground Check Bool
        Grounded = CheckIfGrounded(f_GroundedDistance);

        // Up Direction of Gravity
        Up = attractor.Attract(transform, gravityMultiplier);        
    }

    public bool CheckIfGrounded(float dist)
    {
        return Physics.Raycast(transform.position, (attractor.transform.position - transform.position).normalized, dist, WhatIsGround);
    }
}
