using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PicUpTools : MonoBehaviour
{
    public GameObject Maincamera;
    public float distance = 2f;
    GameObject toolWeapon;
    bool canPicUp;
    Inventory inventory;
    [HideInInspector]
    public int index;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) PickUp();
        if (Input.GetKeyDown(KeyCode.Q)) Drop();
        
    }
    void PickUp()
    {
        RaycastHit hit;
        if (Physics.Raycast(Maincamera.transform.position, Maincamera.transform.forward, out hit, distance))
        {
            if (hit.transform.tag == "Tools" || hit.transform.tag == "Selectable")
            {
                if (canPicUp) Drop();
                toolWeapon = hit.transform.gameObject;
                toolWeapon.GetComponent<Rigidbody>().isKinematic = true;
                toolWeapon.transform.parent = transform;
                toolWeapon.transform.localPosition = Vector3.zero;
                toolWeapon.transform.localEulerAngles = new Vector3(5f, 0f, 0f);
                canPicUp = true;
            }
        }
    }

    void Drop()
    {

        //// if (canPicUp)
        //// {
        toolWeapon.transform.parent = null;
        toolWeapon.GetComponent<Rigidbody>().isKinematic = false;
        canPicUp = false;
        toolWeapon = null;
        //}
    }
}
