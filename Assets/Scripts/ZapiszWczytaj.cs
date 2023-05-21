using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZapiszWczytaj : MonoBehaviour
{
    private float punktyHealhZapis;
    void Start()
    {
        punktyHealhZapis = PlayerPrefs.GetFloat("punktZapis", 0);

        GameObject.Find("CharakterStatus").GetComponent<CharacterStatus>().health = punktyHealhZapis;
    }

    // Update is called once per frame
    void Update()
    {
        punktyHealhZapis = GameObject.Find("CharakterStatus").GetComponent<CharacterStatus>().health;
    }

    public void zapisz()
    {
        PlayerPrefs.SetFloat("punktyZapis", punktyHealhZapis);
        PlayerPrefs.Save();
    }

    public void OnAplicationQuit()
    {
        PlayerPrefs.SetFloat("punktyZapis", punktyHealhZapis);
        PlayerPrefs.Save();
    }
}
