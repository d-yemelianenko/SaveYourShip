using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trzymanie : MonoBehaviour
{
    public GameObject mainCamera;
    public GameObject carriedObject;
    public float distance = 2f;

    private bool canCarry;

    // Update is called once per frame
    void Update()
    {
        if (canCarry && Input.GetKeyDown(KeyCode.E))
        {
            DropObject();
        }
        else if (carriedObject == null && Input.GetKeyDown(KeyCode.E))
        {
            PickupObject();
        }
    }

    void FixedUpdate()
    {
        if (carriedObject != null)
        {
            CarryObject();
        }
    }

    void PickupObject()
    {
        RaycastHit hit;
        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, distance))
        {
            if (hit.transform.tag == "Tools")
            {
                canCarry = true;
                carriedObject = hit.transform.gameObject;
                carriedObject.GetComponent<Rigidbody>().isKinematic = true;
            }
        }
    }

    void CarryObject()
    {
        carriedObject.transform.position = mainCamera.transform.position + mainCamera.transform.forward * distance;
    }

    void DropObject()
    {
        canCarry = false;
        carriedObject.GetComponent<Rigidbody>().isKinematic = false;
        carriedObject = null;
    }
}
