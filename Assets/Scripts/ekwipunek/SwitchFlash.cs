using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchFlash : MonoBehaviour
{
    public Inventory inventory;
    [SerializeField]
    private GameObject playerObj;
    [SerializeField]
    private float warmChangeValue = 0.2f;
    public bool[] toolsTable = new bool[3];     // 0 - mlot, 1 - wedka, 2 - pochodnia
    [SerializeField]
    private GameObject mlot, wedka, pochodnia;
    public void switchFlashM()
    {
        if (toolsTable[1])
        {
            wedka.SetActive(!wedka.activeSelf);
        }
        if (toolsTable[2])
        {
            pochodnia.SetActive(!pochodnia.activeSelf);
            CharacterStatus player = playerObj.GetComponent<CharacterStatus>();
            player.ColdChange(warmChangeValue*(-1));
        }
        mlot.SetActive(!mlot.activeSelf);
        SetActiveTool(0);
        
    }

    public void switchFlashW()
    {
        if (toolsTable[0])
        {
            mlot.SetActive(!mlot.activeSelf);
        }
        if (toolsTable[2])
        {
            pochodnia.SetActive(!pochodnia.activeSelf);
            CharacterStatus player = playerObj.GetComponent<CharacterStatus>();
            player.ColdChange(warmChangeValue*(-1));
        }
        SetActiveTool(1);
        wedka.SetActive(!wedka.activeSelf);
    }

    public void switchFlashP()
    {
        if (toolsTable[0])
        {
            mlot.SetActive(!mlot.activeSelf);
        }
        if (toolsTable[1])
        {
            wedka.SetActive(!wedka.activeSelf);
        }
        SetActiveTool(2);
        pochodnia.SetActive(!pochodnia.activeSelf);
        CharacterStatus player = playerObj.GetComponent<CharacterStatus>();
        player.ColdChange(warmChangeValue);
    }
    public void SetActiveTool(int clicked)
    {
        for (int i = 0; i < toolsTable.Length; i++)
        {
            if (i == clicked && !toolsTable[clicked]) toolsTable[clicked] = true;
            else toolsTable[i] = false;
        }
    }
}
