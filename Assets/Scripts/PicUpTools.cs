using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PicUpTools : MonoBehaviour
{
    public GameObject Maincamera;
    public float distance = 2f;
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
                if (canPicUp)
                    Drop();
               
                toolWeapon = hit.transform.gameObject;
                toolWeapon.GetComponent<Rigidbody>().isKinematic = true;
                toolWeapon.transform.parent = transform;
                Vector3 toolPosition = new Vector3(0.1f, -0.15f, 0.4f);
                toolWeapon.transform.localPosition =Vector3.zero + toolPosition;
               

               toolWeapon.transform.localEulerAngles = new Vector3(10f, 0f, 20f);
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
