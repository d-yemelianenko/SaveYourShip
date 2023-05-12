using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class paneli : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject PanelOpc;
    public GameObject PanelUst;
    public GameObject PanelAutor;

    public void Start()
    {
        PanelUst.SetActive(false);
        PanelAutor.SetActive(false);
        Time.timeScale = 1f;
    }

    public void ButtonOpenPanelUst()
    {
        Debug.Log("Buton Pauzaust");
        PanelUst.SetActive(true);
        PanelOpc.SetActive(false);
        PanelAutor.SetActive(false);

    }

    public void PowrótClosePanelUst()
    {
      
        PanelUst.SetActive(false);
        PanelOpc.SetActive(true);
        PanelAutor.SetActive(false);


    }

    public void ButtonOpenAutor()
    {
        Debug.Log("Buton Auto");
        PanelOpc.SetActive(false);
        PanelAutor.SetActive(true);
        PanelUst.SetActive(false);

    }

    public void PowrótCloseAutor()
    {
        PanelAutor.SetActive(false);
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
