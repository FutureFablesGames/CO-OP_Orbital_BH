using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CameraSetting { ThirdPerson, Isometric, Topdown, FirstPerson, IsoWithRotation, TopWithRotation }

public class CameraController : MonoBehaviour
{
   
    [Range(0,20)] public float sensitivity =10.0f;
    [Range(45, 90)] public float lookXLimit = 45.0f;
    public Transform RotatePoint;
    public Vector2 MouseInput;

    [Header("Camera Settings")]
    public Camera cam;
    public GameObject MeshReference;
    private float CameraPitch;
    [HideInInspector] public bool RotateWithMesh = false;

    [Header("Camera Position")]
    public CameraSetting Setting;
    [SerializeField] private Transform TPS_Transform;
    [SerializeField] private Transform FPS_Transform;
    [SerializeField] private Transform ISO_Transform;
    [SerializeField] private Transform TOP_Transform;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Start()
    {
        UpdateCameraSettings(Setting);
    }

    void LateUpdate()
    {
        UpdateCamera();
    }

    private void UpdateCameraSettings(CameraSetting setting)
    {
        switch (setting)
        {
            case CameraSetting.ThirdPerson:
                cam.transform.localPosition = TPS_Transform.localPosition;
                cam.transform.localRotation = TPS_Transform.localRotation;
                RotateWithMesh = true;
                break;
            case CameraSetting.FirstPerson:
                cam.transform.localPosition = FPS_Transform.localPosition;
                cam.transform.localRotation = FPS_Transform.localRotation;
                RotateWithMesh = true;
                break;
            case CameraSetting.Isometric:
                cam.transform.localPosition = ISO_Transform.localPosition;
                cam.transform.localRotation = ISO_Transform.localRotation;
                RotateWithMesh = false;
                break;
            case CameraSetting.Topdown:
                cam.transform.localPosition = TOP_Transform.localPosition;
                cam.transform.localRotation = TOP_Transform.localRotation;
                RotateWithMesh = false;
                break;
            case CameraSetting.IsoWithRotation:
                cam.transform.localPosition = ISO_Transform.localPosition;
                cam.transform.localRotation = ISO_Transform.localRotation;
                RotateWithMesh = true;
                break;
            case CameraSetting.TopWithRotation:
                cam.transform.localPosition = TOP_Transform.localPosition;
                cam.transform.localRotation = TOP_Transform.localRotation;
                RotateWithMesh = true;
                break;

        }
    }

    private void UpdateCamera()
    {
        // Mouse Input
        MouseInput.x = Input.GetAxis("Mouse X") * sensitivity;
        MouseInput.y = Input.GetAxis("Mouse Y") * sensitivity;

        
        
        // Horizontal Rotations (YAW)
        float xRotationRate = MouseInput.x * sensitivity * Time.deltaTime;
        MeshReference.transform.RotateAround(RotatePoint.position, RotatePoint.rotation * Vector3.up, xRotationRate);

        // Vertical Rotations (PITCH)
        if (Setting == CameraSetting.FirstPerson || Setting == CameraSetting.ThirdPerson)
        {
            // TODO: Replace '-' sign with boolean for inverted camera control in the future
            float yRotationRate = -MouseInput.y * sensitivity * Time.deltaTime;                    
            CameraPitch += yRotationRate;
            CameraPitch = Mathf.Clamp(CameraPitch, -lookXLimit, lookXLimit);

            // TODO: Need to have third person camera controller rotate rather than the camera itself.
            // Currently doesn't work because it conflicts with the code below that is responsible for rotating controller with the mesh.
            cam.transform.localRotation = Quaternion.Euler(CameraPitch, 0, 0);
        }


        // Disable me to prevent camera rotations, but allow player rotations        
        if (RotateWithMesh)
        {
            transform.localRotation = MeshReference.transform.localRotation;
        }
        
    }
    
}
