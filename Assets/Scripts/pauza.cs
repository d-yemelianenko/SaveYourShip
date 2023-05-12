using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class pauza : MonoBehaviour
{
    public bool Menu;
    public GameObject Panel;
    public GameObject PlayerInterface;
    public GameObject InventoryPanel;

    public void Start()
    {
       Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Panel.SetActive(false);
        InventoryPanel.SetActive(true);
        Time.timeScale = 1f;
    }

    /*public void ButtonOpenPanel()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Debug.Log("Buton Pauza");
        Panel.SetActive(true);
        Time.timeScale = 0f;
    }
    */

    public void ButtonClosePanel()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false; 
        PlayerInterface.SetActive(true);
        InventoryPanel.SetActive(true);
        Panel.SetActive(false);
        Time.timeScale = 1f;
    }


    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
            Menu = !Menu;

            if (Panel == Menu)
            {
                Panel.SetActive(true);
                Time.timeScale = 0;
                PlayerInterface.SetActive(false);
                InventoryPanel.SetActive(false);
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                Panel.SetActive(false);
                Time.timeScale = 1f;
                PlayerInterface.SetActive(true);
                InventoryPanel.SetActive(true);
            }
        }
    }

    public void PlayMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu 2");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
