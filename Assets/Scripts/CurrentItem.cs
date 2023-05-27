using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CurrentItem : MonoBehaviour, IPointerClickHandler 
{
      [HideInInspector]
    public int index;

    GameObject inventoryObj;
    Inventory inventory;
    GameObject toolWeapon;
    void Start()
    {
        inventoryObj = GameObject.FindGameObjectWithTag("InventoryManager");
        inventory = inventoryObj.GetComponent<Inventory>();
    }
    public void OnPointerClick(PointerEventData eventData)
    {

        if (eventData.button == PointerEventData.InputButton.Left) // U¿ycie przedmiotu
        {
            if (inventory.item[index].isDroped)// sprawdzenie czy  pusty slot czy nie
            { 
                if (inventory.item[index].customEvent != null)
                {
                    inventory.item[index].customEvent.Invoke(); // invent zadzia³a przy klikaniu na lew¹ czeœæ myszy za pomoca Invoke
                }
                if (inventory.item[index].isRemovable)// przedmiot mozna  delete
                {
                    Remove();
                }

            }
               
            inventory.DisplayItems();
        }

        if (eventData.button == PointerEventData.InputButton.Right) // Wyrzucenie przedmiotu
        {
            if (inventory.item[index].isDroped)// sprawdzenie czy  pusty slot czy nie
            {
                Drop();
                Remove();
            }
            inventory.DisplayItems();
        }
    }

        void Remove() // zmniejszenia illoœci item in colection
        {
            if (inventory.item[index].countItem > 1)
            {
                inventory.item[index].countItem--; // illoœæ przedmiotó w inwentarze
            }
            else
            {
                inventory.item[index] = new Item();
            }
        }

        void Drop()
        {
            if (inventory.item[index].id != 0)// sprawdzenie czy  pusty slot czy nie
            {
                for(int i=0; i< inventory.database.transform.childCount; i++)
                {
                    Item item = inventory.database.transform.GetChild(i).GetComponent<Item>();
                    if(item)
                    { 
                        if (inventory.item[index].id == item.id)
                        {
                            GameObject dropedObj = Instantiate(item.gameObject);//dodanie na scene za pomoc¹ foldera
                            dropedObj.transform.position = Camera.main.transform.position + Camera.main.transform.forward; //pozycja odnosznie kamery + 1m
                        }

                    }
                   
                }
                
            }
        }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject dragedObject = DragDrop.dragedObject;
        if (dragedObject == null)
        {
            return;
        }
        CurrentItem currentdragedItem = dragedObject.GetComponent<CurrentItem>();
        if (currentdragedItem)
        {
            Item currentItem = inventory.item[GetComponent<CurrentItem>().index];
            inventory.item[GetComponent<CurrentItem>().index] = inventory.item[currentdragedItem.index];
            inventory.item[currentdragedItem.index] = currentItem;
            inventory.DisplayItems();// pererisowywajem

        }
    }
}
