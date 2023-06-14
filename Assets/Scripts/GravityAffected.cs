using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GravityAffected : MonoBehaviour
{
    public LayerMask WhatIsGround;

    [Range(0, 5)] public float GroundedDistance;
    [HideInInspector] public float AttractionStrength, Distance;
    public bool Grounded = false;

    [HideInInspector] public GravityField CurrentGravityTarget;

    void Update()
    {
        if (!CurrentGravityTarget)
            return;

        // Not doing anything with Gravity check, but should keep here if we need later.
        Grounded = CheckIfGrounded(GroundedDistance);

        // EDIT: Always pulls to the closest gravity field.  This resolves the issue of spinning out of control and not being oriented to the planet after exiting the gravity zone.
        // By checking only if it's grounded, we reorient ourselves to the global up direction when out the gravity field range.
        // If we want to make it so that they no longer orient themselves to planet when out of orbit, then we can set CurrentGravityTarget to null and apply a rotational force to the player to keep rotating randomly.
        transform.rotation = Quaternion.FromToRotation(Vector3.up, (transform.position - CurrentGravityTarget.transform.position).normalized);

        // EDIT: Always pulls the target towards the planet, even if we aren't grounded
        transform.GetComponent<Rigidbody>().AddForce((CurrentGravityTarget.transform.position - transform.position).normalized * AttractionStrength, ForceMode.Force);


        if (Grounded) transform.GetComponent<Rigidbody>().velocity *= 0.9f;
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, transform.rotation * Vector2.down * GroundedDistance);
        
        if (!CurrentGravityTarget) return;

        Gizmos.color = Color.magenta;
        Gizmos.DrawRay(transform.position, (CurrentGravityTarget.transform.position - transform.position).normalized * Distance);
    }
    public bool CheckIfGrounded(float dist)
    {
        return Physics.Raycast(transform.position, (CurrentGravityTarget.transform.position - transform.position).normalized, dist, WhatIsGround);
    }
    public RaycastHit GetHit(float dist)
    {
        RaycastHit Hit;
        Physics.Raycast(transform.position, (CurrentGravityTarget.transform.position - transform.position).normalized, out Hit, dist, WhatIsGround);
        return Hit;
    }
}
