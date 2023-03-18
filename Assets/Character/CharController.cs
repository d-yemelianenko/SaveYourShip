using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{
    private Rigidbody RB;

    public float Speed = 7;
    public float SprintSpeed = 13;
    public float JumpForce = 6;

    public float Gravity = -15.0f;
    private float verticalVelocity;

    public bool InSprint;
    public bool IsGrounded;
    public LayerMask GroundMask;

    float Vertical;
    float Horizontal;
    void Start()
    {
        RB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        IsGrounded = Physics.CheckSphere(new Vector3(transform.position.x, transform.position.y - 1.6f, transform.position.z), 0.3f, GroundMask);

        if(InSprint)
        {
            Horizontal = Input.GetAxis("Horizontal") * Speed;
            Vertical = Input.GetAxis("Vertical") * SprintSpeed;
        }
        else
        {
            Horizontal = Input.GetAxis("Horizontal") * Speed;
            Vertical = Input.GetAxis("Vertical") * Speed;
        }

        Vector3 MovePosition = transform.right * Horizontal + transform.forward * Vertical;
        Vector3 NewMovePosition = new Vector3(MovePosition.x, RB.velocity.y, MovePosition.z);

        RB.velocity = NewMovePosition;

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded)
        {
            RB.velocity = new Vector3(RB.velocity.x, JumpForce, RB.velocity.z);
        }

        if (Input.GetKey(KeyCode.LeftShift) && IsGrounded)
        {
            InSprint = true;
        }
        else
        {
            InSprint = false;
        }
    }
}
