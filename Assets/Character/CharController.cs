using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{
    public float Speed = 7;
    public float SprintSpeed = 13;
    public float JumpForce = 6;
    private float ySpeed;
    private float originalStepOffset;

    private CharacterController characterController;

    public bool InSprint;

    float Vertical;
    float Horizontal;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        originalStepOffset = characterController.stepOffset;
    }

    void Update()
    {
        Horizontal = Input.GetAxis("Horizontal") * Speed;
        if (InSprint)
        {
            Vertical = Input.GetAxis("Vertical") * SprintSpeed;
        }
        else
        {
            Vertical = Input.GetAxis("Vertical") * Speed;
        }

        Vector3 MovePosition = transform.right * Horizontal + transform.forward * Vertical;
        Vector3 NewMovePosition = new Vector3(MovePosition.x, 0, MovePosition.z);
        float magnitude = Mathf.Clamp01(NewMovePosition.magnitude);

        ySpeed += Physics.gravity.y * Time.deltaTime;

        if (characterController.isGrounded)
        {
            characterController.stepOffset = originalStepOffset;
            ySpeed = -0.1f;
            if (Input.GetButton("Jump"))
            {
                ySpeed = JumpForce;
            }
        }
        else
        {
            characterController.stepOffset = 0;
        }

        Vector3 velocity = NewMovePosition * magnitude;
        velocity.y = ySpeed;


        characterController.Move(velocity * Time.deltaTime);

        if (Input.GetKey(KeyCode.LeftShift) && characterController.isGrounded)
        {
            InSprint = true;
        }
        else
        {
            InSprint = false;
        }
    }
}
