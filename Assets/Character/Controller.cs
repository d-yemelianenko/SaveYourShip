using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    Rigidbody Rb;
    GameObject Cam;
    [SerializeField]
    private float MoveSpeed = 0.1f;
    [SerializeField]
    private float SprintSpeed = 0.2f;
    [SerializeField]
    private float JumpForce = 20000;
    [SerializeField]
    private float MouseSensitivity = 1;

    public CharacterStatus characterStatus;

    float Vertical;
    float Horizontal;

    public float MouseX;
    public float MouseY;

    public bool IsGrounded;
    public bool InSprint;
    public delegate void SprintStarted();
    public static event SprintStarted OnSprintStarted;

    float CamRotX;
    void Start()
    {
        Rb = GetComponent<Rigidbody>();
        Cam = transform.Find("Camera").gameObject;
        characterStatus = GetComponent<CharacterStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        Vertical = Input.GetAxis("Vertical");
        Horizontal = Input.GetAxis("Horizontal");
        MouseX = Input.GetAxis("Mouse X");
        MouseY = Input.GetAxis("Mouse Y");

        InSprint = Input.GetKey(KeyCode.LeftShift);

        if(InSprint == true)
        {
            if (OnSprintStarted != null)
            {
                OnSprintStarted();
            }
            Rb.MovePosition((transform.position + (transform.forward) * Vertical * SprintSpeed) + (transform.right * Horizontal * MoveSpeed));
        }
        else
        {
            Rb.MovePosition((transform.position + (transform.forward) * Vertical * MoveSpeed) + (transform.right * Horizontal * MoveSpeed));
        }

        if (IsGrounded == true && Input.GetKeyDown(KeyCode.Space))
        {
            Rb.AddForce(transform.up * JumpForce);
        }

        if(MouseX == 0)
        {
            Rb.constraints = RigidbodyConstraints.FreezeRotation;
        }

        Rb.MoveRotation(Rb.rotation * Quaternion.Euler(new Vector3(0, MouseX * MouseSensitivity, 0)));

        CamRotX -= MouseY * MouseSensitivity;
        CamRotX = Mathf.Clamp(CamRotX, -80, 75);
        Quaternion CamRot = Quaternion.Euler(CamRotX, 0, 0);
        Cam.transform.localRotation = CamRot;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            IsGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            IsGrounded = false;
        }
    }
}
