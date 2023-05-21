using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchFlash : MonoBehaviour
{
    
    public void  switchFlashM(GameObject flash1)
    {
        flash1.SetActive(!flash1.activeSelf);
    }

    public void switchFlashW(GameObject flash2)
    {
        flash2.SetActive(!flash2.activeSelf);
        
    }

    public void switchFlashP(GameObject flash3)
    {
        flash3.SetActive(!flash3.activeSelf);
    }

   
}
