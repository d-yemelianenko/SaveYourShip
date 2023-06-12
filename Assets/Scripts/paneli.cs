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
    public GameObject PanelInstruction;

    public void Start()
    {
        PanelUst.SetActive(false);
        PanelAutor.SetActive(false);
        PanelInstruction.SetActive(false);
        Time.timeScale = 1f;
    }

    public void ButtonOpenPanelUst()
    {
        PanelUst.SetActive(true);
        PanelOpc.SetActive(false);
        PanelAutor.SetActive(false);
        PanelInstruction.SetActive(false);
    }

    public void PowrótClosePanelUst()
    {  
        PanelUst.SetActive(false);
        PanelOpc.SetActive(true);
        PanelAutor.SetActive(false);
        PanelInstruction.SetActive(false);
    }

    public void ButtonOpenAutor()
    {
        PanelOpc.SetActive(false);
        PanelAutor.SetActive(true);
        PanelUst.SetActive(false);
        PanelInstruction.SetActive(false);
    }

    public void PowrótCloseAutor()
    {
        PanelAutor.SetActive(false);
        PanelUst.SetActive(false);
        PanelOpc.SetActive(true);
        PanelInstruction.SetActive(false);
    }

    public void ButtonOpenInstruction()
    {
        PanelOpc.SetActive(false);
        PanelInstruction.SetActive(true);
        PanelUst.SetActive(false);
        PanelAutor.SetActive(false);
    }

    public void PowrótCloseInstruction()
    {
        PanelInstruction.SetActive(false);
        PanelUst.SetActive(false);
        PanelOpc.SetActive(true);
        PanelAutor.SetActive(false);
    }

    public void PlayMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void Qpowrót()
    {
        SceneManager.LoadScene(0);
    }
}
