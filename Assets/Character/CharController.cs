using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{
    [Header("Moving stats")]
    [SerializeField]
    private float originSpeed = 7;
    private float speed = 7;
    [SerializeField]
    private float originSprintSpeed = 13;
    private float sprintSpeed = 13;
    [SerializeField]
    private float jumpForce = 6;
    private float ySpeed;
    private float originalStepOffset;

    [SerializeField]
    private float groundRayDistance = 10;
    private RaycastHit slopeHit;
    private float slopeSlideSpeed = 7;
    private bool isSliding;

    private CharacterController characterController;
    private bool isOnLadder;

    private bool inSprint;

    float vertical;
    float horizontal;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        originalStepOffset = characterController.stepOffset;
    }

    void Update()
    {
        horizontal = Input.GetAxis("Horizontal") * speed;
        if (inSprint)
        {
            vertical = Input.GetAxis("Vertical") * sprintSpeed;
        }
        else
        {
            vertical = Input.GetAxis("Vertical") * speed;
        }


        Vector3 MovePosition = transform.right * horizontal + transform.forward * vertical;
        Vector3 NewMovePosition = new Vector3(MovePosition.x, 0, MovePosition.z);
        float magnitude = Mathf.Clamp01(NewMovePosition.magnitude);

        ySpeed += Physics.gravity.y * Time.deltaTime * 2;

        if (OnSteepSlope())
        {
            isSliding = true;
            SteepSlopeMovement(ref MovePosition, ref NewMovePosition);
        }
        else isSliding = false;

            if (characterController.isGrounded)
        {
            characterController.stepOffset = originalStepOffset;

            if (isSliding == false)
            {
                ySpeed = -0.1f;
            }

            if (Input.GetButton("Jump") && !isSliding)
            {
                ySpeed = jumpForce;
            }
        }
        else
        {
            characterController.stepOffset = 0;
        }

        Vector3 velocity = NewMovePosition * magnitude;
        if (Input.GetKey(KeyCode.W) && isOnLadder)
        {
            velocity.y = 5.0f;
        }
        else if (Input.GetKey(KeyCode.LeftShift) && isOnLadder)
        {
            velocity.y = 0f;
        }
        else if (isOnLadder)
        {
            velocity.y = ySpeed * .2f;
        }
        else
        {
            velocity.y = ySpeed;
        }


        characterController.Move(velocity * Time.deltaTime);

        if (Input.GetKey(KeyCode.LeftShift) && GetComponent<CharacterStatus>().GetStamina() > 0)
        {
            inSprint = true;
            GetComponent<CharacterStatus>().ChangeStamina(-10.0f);
        }
        else if(GetComponent<CharacterStatus>().GetStamina() < 100)
        {
            GetComponent<CharacterStatus>().ChangeStamina(5.0f);
            inSprint = false;
        }
    }

    private bool OnSteepSlope()
    {
        if (!characterController.isGrounded) return false;

        if(Physics.Raycast(transform.position, Vector3.down, out slopeHit, (characterController.height / 2) + groundRayDistance))
        {
            float slopeAngle = Vector3.Angle(slopeHit.normal, Vector3.up);
            if (slopeAngle > characterController.slopeLimit)
            {
                return true;
            }
        }
        return false;
    }

    private void SteepSlopeMovement(ref Vector3 movePosition, ref Vector3 newMovePosition)
    {
        Vector3 slopeDirection = Vector3.up - slopeHit.normal * Vector3.Dot(Vector3.up, slopeHit.normal);
        float slideSpeed = speed + slopeSlideSpeed + Time.deltaTime;
        movePosition = slopeDirection * -slideSpeed;
        newMovePosition = new Vector3(movePosition.x, -slopeHit.point.y, movePosition.z );
    }

    private void OnTriggerEnter(Collider other) //Obs³uga drabiny
    {
        if (other.CompareTag("Ladder"))
        {
            isOnLadder = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ladder"))
        {
            isOnLadder = false;
        }
    }

    public void SetSpeedValue(float hunger)
    {
        float speedLimiter = (5 - hunger)/2;
        speed = originSpeed - speedLimiter;
        sprintSpeed = originSprintSpeed - speedLimiter;
    }

}
