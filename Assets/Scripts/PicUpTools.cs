using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PicUpTools : MonoBehaviour
{
    public GameObject Maincamera;
    public float distance = 7f;
    GameObject toolWeapon;
    bool canPicUp;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) PicUp();
        if (Input.GetKeyDown(KeyCode.Q)) Drop();
        
    }
    void PicUp()
    {
        RaycastHit hit;
        if(Physics.Raycast(Maincamera.transform.position,Maincamera.transform.forward,out hit, distance))
        {
            if (hit.transform.tag == "Tools")
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
        toolWeapon.transform.parent = null;
        toolWeapon.GetComponent<Rigidbody>().isKinematic = false;
        canPicUp = false;
        toolWeapon = null;


    }
}
