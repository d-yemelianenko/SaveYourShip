using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Zarzad3 : MonoBehaviour
{
    public void PlayMenu()
    {
        SceneManager.LoadScene(1);
    }

    public void Playpowrót()
    {
        SceneManager.LoadScene(2);
    }

    public void QuitPressed()
    {
        Application.Quit();
    }
}
