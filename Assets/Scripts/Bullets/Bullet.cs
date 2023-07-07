using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(GravityBody))]
public class Bullet : MonoBehaviour
{
    [HideInInspector] public Vector3 Target;
    [HideInInspector] public Rigidbody RB;
    [Range(0,50)] public float Speed = 1;

    private void Awake()
    {
       RB = GetComponent<Rigidbody>();
        GetComponent<GravityBody>().attractor = FindObjectOfType<GravityAttractor>();
    }
    void Update()
    {
        if (GetComponent<GravityBody>().Grounded)
            RB.velocity = (transform.rotation*Target*Speed);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + Target);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag=="Player")
        Destroy(gameObject);
    }
}
