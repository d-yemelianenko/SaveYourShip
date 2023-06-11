using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringWheel : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    public float rotateSpeed = 7f;
    public KeyCode interactionKey = KeyCode.E;

    private Transform playerCamera;
    private bool isLookingAtWheel = false;

    private bool interacting = false;
    void Start()
    {
        playerCamera = Camera.main.transform;
    }

    void Update()
    {
        // Check if the player is looking at the steering wheel
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.position, playerCamera.forward, out hit, 7f))
        {
            if (hit.transform == transform)
            {
                isLookingAtWheel = true;
            }
            else
            {
                isLookingAtWheel = false;
            }
        }
        else
        {
            isLookingAtWheel = false;
        }


        if (interacting)
        {
            if (Input.GetKeyDown(interactionKey))
            {
                CharController charController = player.GetComponent<CharController>();
                interacting = false;
                charController.enabled = true;
            }
            float rotationInput = Input.GetAxis("Horizontal");
            float xPosition = transform.parent.position.x + (rotationInput * 0.01f);
            if (xPosition < 22 && xPosition > -26)
            {
                transform.parent.position = new Vector3(xPosition, transform.parent.position.y, transform.parent.position.z);
                float rotationAngleZ = transform.rotation.eulerAngles.z - (rotationInput * rotateSpeed);
                transform.rotation = Quaternion.Euler(0f, 0f, rotationAngleZ);
            }
            else
            {
                if (xPosition < 22) xPosition += 0.1f;
                else xPosition -= 0.1f;
            }
            //float rotationAngleY = transform.rotation.eulerAngles.y - (rotationInput);
            //transform.parent.rotation = Quaternion.Euler(0f, -rotationAngleY, 0f);
        }

        // Rotate the steering wheel based on player input
        else if (isLookingAtWheel)
        {
            CharController charController = player.GetComponent<CharController>();
            if (Input.GetKeyDown(interactionKey))
            {
                if (!interacting)
                {
                    interacting = true;
                    charController.enabled = false;
                }
            }
            else if (Input.GetKeyDown(interactionKey))
            {
                interacting = false;
                charController.enabled = true;
            }
        }
    }
}