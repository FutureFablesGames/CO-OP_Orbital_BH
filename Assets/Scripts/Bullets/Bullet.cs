using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SphereCollider))]
public class Bullet : MonoBehaviour
{
    [HideInInspector] public Vector3 Target;
    [HideInInspector] public Rigidbody RB;
    [Range(0,50)] public float Speed = 1;

    public GameObject Attractor;

    private void Awake()
    {       
       Attractor = FindObjectOfType<GravityAttractor>().gameObject;
    }

    void Update()
    {       
        transform.RotateAround(Attractor.transform.position, Speed * Time.deltaTime);        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + Target);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Destroy(gameObject);
        }
        

        if (other.tag == "Ground")
        {
            Destroy(gameObject);
        }
        
    }
}
