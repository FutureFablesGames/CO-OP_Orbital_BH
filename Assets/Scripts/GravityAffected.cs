using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityAffected : MonoBehaviour
{
    public LayerMask WhatIsGround;

    [Range(0, 5)] public float GroundedDistance;
    [HideInInspector] public Transform Planet;
    [HideInInspector] public float AttractionStrength, Distance;
    private bool Grounded = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (!Planet)
            return;

        Grounded = CheckIfGrounded(GroundedDistance);
        if (CheckIfGrounded(50))
            transform.rotation = Quaternion.FromToRotation(Vector3.up, GetHit(Distance).normal);

        if (!Grounded) 
            transform.GetComponent<Rigidbody>().AddForce((Planet.position - transform.position).normalized * AttractionStrength, ForceMode.Force);
        else if (Grounded) transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawRay(transform.position, (Planet.position - transform.position).normalized * Distance);
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, transform.rotation * Vector2.down * GroundedDistance);
    }
    public bool CheckIfGrounded(float dist)
    {
        return Physics.Raycast(transform.position,  (Planet.position - transform.position).normalized, dist, WhatIsGround);
    }
    public RaycastHit GetHit(float dist)
    {
        RaycastHit Hit;
        Physics.Raycast(transform.position, (Planet.position - transform.position).normalized , out Hit, dist, WhatIsGround);
        return Hit;
    }
}
