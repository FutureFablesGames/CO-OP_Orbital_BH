using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
   
    [Range(0,20)] public float sensitivity =10.0f;
    [Range(45, 90)] public float lookXLimit = 45.0f;
    public Transform RotatePoint;
    private Quaternion Direction;
    public Vector2 MouseInput;
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void LateUpdate()
    {
        //Mouse Input
        MouseInput.x = Input.GetAxis("Mouse X") * sensitivity;
        MouseInput.y = Input.GetAxis("Mouse Y") * sensitivity;
        
        Direction = Quaternion.Euler(MouseInput.y, 0, 0);
        MouseInput.y = Mathf.Clamp(MouseInput.y, -lookXLimit, lookXLimit);

        //Set Rotations
        // transform.parent.GetChild(0).localRotation = Quaternion.Euler(0, MouseInput.x, 0);    
        transform.parent.RotateAround(RotatePoint.position, RotatePoint.rotation * Vector3.up, MouseInput.x);
        //if(transform.parent.GetComponent<GravityAffected>().Grounded==false)
        //    MouseInput.x = 0;
               
        gameObject.transform.localRotation = Direction;
    }
}
