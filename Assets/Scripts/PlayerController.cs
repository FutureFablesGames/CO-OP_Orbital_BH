using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(GravityAffected))]
public class PlayerController : NetworkBehaviour
{
    [Header("Resources")]
    public float CurrentResources;
    public float MaxResources = 500.0f;

    [Header("Other")]
    [Range(0, 25)] public float Speed;
    private Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //check if we own this current player, and return if not so the other code doesn't run.
        //this means for networking that only your own player will move when you move
        if (!IsOwner) return;
        Vector3 Vec = Vector3.zero;
        Vec += transform.rotation * Vector3.right * Input.GetAxisRaw("Horizontal");
        Vec += transform.rotation * Vector3.forward * Input.GetAxisRaw("Vertical");
        rb.AddForce(Vec.normalized* Speed *2);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Deposit")
        {
            other.GetComponent<ResourceDepot>().Deposit(this, CurrentResources);
        }

        if (other.tag == "Resource")
        {
            other.GetComponent<ResourceNode>().Interact(this);
        }
    }
}
