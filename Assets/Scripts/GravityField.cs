using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityField : MonoBehaviour
{
    [Range(0, 50)] public float GravityRange, GravityStrength;
    private float BasePlanetCircumference;
    public LayerMask WhatIsGravityAffected;
    public Collider[] colls;
   
    // Update is called once per frame
    void Update()
    {
       BasePlanetCircumference = GetComponent<SphereCollider>().radius * transform.lossyScale.magnitude * (2 / Mathf.PI);
        colls = (Physics.OverlapSphere(transform.position, GravityRange + BasePlanetCircumference, WhatIsGravityAffected));
        foreach(Collider coll in colls)
        {
            if (coll.GetComponent<GravityAffected>() != null)
            {
                coll.transform.GetComponent<GravityAffected>().CurrentGravityTarget = this;
                coll.transform.GetComponent<GravityAffected>().AttractionStrength = GravityStrength;                
                coll.transform.GetComponent<GravityAffected>().Distance = GravityRange;
            }            
        }
    }
    private void OnDrawGizmos()
    {
        BasePlanetCircumference = GetComponent<SphereCollider>().radius * transform.lossyScale.magnitude *(2/ Mathf.PI );
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, GravityRange + BasePlanetCircumference);
    }
}
