using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(GravityAffected))]
public class PlayerController : MonoBehaviour
{
    [Header("Resources")]
    public float CurrentResources;
    public float MaxResources = 500.0f;

    [Header("UI")]
    public TMP_Text PromptDisplay = null;

    [Header("Other")]
    [Range(0, 25)] public float Speed;
    private Rigidbody rb;
    public GameObject mesh;
    public CameraController camController;

    public float JumpForce = 1.0f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Motion();
        Jump();
    }

    private void Motion()
    {
        Vector3 Vec = Vector3.zero;

        // Get direction of movement based on whether we're rotating the camera with the mesh or not
        Quaternion direction = (camController.RotateWithMesh) ?
            mesh.transform.rotation : camController.cam.transform.rotation;

        // X Axis Movement (Left to Right)
        Vec += direction * ((camController.RotateWithMesh) ? Vector3.right : Vector3.right) * Input.GetAxisRaw("Horizontal");

        // Z Axis Movement (Forward/Backwards)
        Vec += direction * ((camController.RotateWithMesh) ? Vector3.forward : Vector3.up) * Input.GetAxisRaw("Vertical");

        // Add Movement Force
        rb.AddForce(Vec.normalized * Speed * 2);
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(transform.up * JumpForce);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Interactable")
        {
            if (!PromptDisplay.enabled) {
                PromptDisplay.text = "'E' to Interact";
                PromptDisplay.enabled = true;
            }
            
            if (Input.GetKeyDown(KeyCode.E))
            {
                other.GetComponent<Interactable>().Interact(this);
                PromptDisplay.text = "";
                PromptDisplay.enabled = false;
            }
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Interactable")
        {
            if (PromptDisplay.enabled)
            {
                PromptDisplay.text = "";
                PromptDisplay.enabled = false;
            }
        }
    }
}
