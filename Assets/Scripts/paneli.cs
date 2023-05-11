using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class paneli : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject PanelOpc;
    public GameObject PanelUst;

    public void Start()
    {
        PanelUst.SetActive(false);
        Time.timeScale = 1f;
    }

    public void ButtonOpenPanelUst()
    {
        Debug.Log("Buton Pauzaust");
        PanelUst.SetActive(true);
        PanelOpc.SetActive(false);
        
    }

    public void PowrótClosePanelUst()
    {
        Debug.Log("Buton Close PanelUst");
        PanelUst.SetActive(false);
        PanelOpc.SetActive(true);
       
    }




    public void PlayMenu()
    {
        Debug.Log("Menu");
        SceneManager.LoadScene(0);
    }
    public void Qpowrót()
    {
        SceneManager.LoadScene(0);
    }
}
