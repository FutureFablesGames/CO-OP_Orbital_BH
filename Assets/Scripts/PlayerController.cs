using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(GravityAffected))]
public class PlayerController : MonoBehaviour
{
    [Range(0, 25)] public float Speed;
    private Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 Vec = Vector3.zero;
        Vec += transform.rotation * Vector3.right * Input.GetAxisRaw("Horizontal");
        Vec += transform.rotation * Vector3.forward * Input.GetAxisRaw("Vertical");
        rb.AddForce(Vec.normalized* Speed *2);
    }

}
