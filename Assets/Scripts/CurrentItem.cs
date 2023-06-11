using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CurrentItem : MonoBehaviour, IPointerClickHandler 
{
      [HideInInspector]
    public int index;

    GameObject handObj;
    SwitchFlash switchFlash;
    GameObject inventoryObj;
    Inventory inventory;
    GameObject cannonObj;
    Cannon cannon;
    void Start()
    {
        handObj = GameObject.FindGameObjectWithTag("Hand");
        switchFlash = handObj.GetComponent<SwitchFlash>();
        inventoryObj = GameObject.FindGameObjectWithTag("InventoryManager");
        inventory = inventoryObj.GetComponent<Inventory>();
        cannonObj = GameObject.FindGameObjectWithTag("Cannon");
        cannon = cannonObj.GetComponent<Cannon>();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        cannon.SetItemId(index);
        if (eventData.button == PointerEventData.InputButton.Left) // U¿ycie przedmiotu
        {
            if (inventory.item[index].isDroped) // sprawdzenie czy  pusty slot czy nie
            { 
                if (inventory.item[index].customEvent != null)
                {
                    inventory.item[index].customEvent.Invoke(); // invent zadzia³a przy klikaniu lewym przyiskiem myszy za pomoca Invoke
                }
                if (inventory.item[index].isRemovable) // przedmiot mozna  usun¹æ
                {
                    Remove(index);
                }
            }
               
            inventory.DisplayItems();
        }

        if (eventData.button == PointerEventData.InputButton.Right) // Wyrzucenie przedmiotu
        {
            if (inventory.item[index].isDroped)// sprawdzenie czy pusty slot czy nie
            {
                Drop();
                Remove(index);
            }
            inventory.DisplayItems();
        }
    }

    public void ChooseActiveItem()
    {
        cannon.SetItemId(index);
            if (inventory.item[index].isDroped) // sprawdzenie czy pusty slot czy nie
            {
                if (inventory.item[index].customEvent != null)
                {
                    inventory.item[index].customEvent.Invoke();
                }
                if (inventory.item[index].isRemovable) // przedmiot mozna  usun¹æ
                {
                    Remove(index);
                }
            }
            inventory.DisplayItems();
    }

    public void Remove(int index) // zmniejszenia iloœci item in colection
    {
        Debug.Log(index);
        if (inventory.item[index].countItem > 1)
        {
            inventory.item[index].countItem--; // iloœæ przedmiotów w inwentarzu
        }
        else
        {
            inventory.item[index] = new Item();
        }
        inventory.DisplayItems();
    }

    void Drop()
    {
        if (inventory.item[index].id != 0)// sprawdzenie czy pusty slot czy nie
        {
            for(int i=0; i< inventory.database.transform.childCount; i++)
            {
                Item item = inventory.database.transform.GetChild(i).GetComponent<Item>();
                if(item)
                { 
                    if (inventory.item[index].id == item.id)
                    {
                        if((item.id == 4 && switchFlash.toolsTable[0]) || (item.id == 8 && switchFlash.toolsTable[1]) || (item.id == 6 && switchFlash.toolsTable[2]) || (item.id == 3 && switchFlash.toolsTable[3]))
                        {
                            inventory.item[index].customEvent.Invoke();
                        }
                        GameObject dropedObj = Instantiate(item.gameObject); //dodanie na scene za pomoc¹ foldera
                        dropedObj.transform.position = Camera.main.transform.position + Camera.main.transform.forward; //pozycja odnosznie kamery + 1m
                    }
                }
            }
        }
    }
}
