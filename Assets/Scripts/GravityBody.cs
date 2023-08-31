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

    public Vector3 Up;
    public bool IsGrounded;

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
        CheckGround();

        // Orient the body to face the direction of gravity
        Quaternion targetRotation = Quaternion.FromToRotation(transform.up, Up) * transform.rotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 5 * Time.deltaTime);

        if (!IsGrounded)
        {            
            // Apply Gravity Force
            rb.AddForce(-10 * WeightMultiplier * Up);
        }
    }

    // ================================================
    // FUNCTIONS / FUNCTIONS / FUNCTIONS / FUNCTIONS / 
    // ================================================
    
    private void CheckGround()
    {
        if (Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, 1.5f))
        {
            IsGrounded = true;

            Vector3 offsetOrigin = hit.point + (-1 * hit.normal) * 100;

            int counter = 0;
            Vector3 backHit = Vector3.zero;
            Vector3 PreviousPoint = Vector3.zero;

            bool calculating = true;
            while (calculating == true)
            {
                counter++;                
                if (Physics.Linecast(offsetOrigin, hit.point, out RaycastHit hit2))
                {
                    if (counter > 100)
                    {
                        backHit = hit.point;
                        counter = 0;
                        calculating = false;     
                        break;
                    }    
                    
                    PreviousPoint = hit2.point;
                    offsetOrigin = hit2.point + (hit.normal/10000f);
                }

                else
                {
                    if (PreviousPoint == Vector3.zero)
                    {
                        backHit = hit.point;
                    } else
                    {
                        backHit = PreviousPoint;
                    }

                    calculating = false;
                    counter = 0;
                }
            }

            

            // continue with backHit
            Vector3 midPoint = (hit.point + backHit) / 2;
            Debug.Log("Hit Point: " + hit.point + "\nBack Hit: " + backHit + "\nMid Point: " + midPoint);

            Up = (hit.point - midPoint).normalized;            
        }

        else
        {
            IsGrounded = false;
            Up = transform.up;
        }
    }

}
