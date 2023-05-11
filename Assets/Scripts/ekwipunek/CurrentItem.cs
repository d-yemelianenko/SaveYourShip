using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CurrentItem : MonoBehaviour, IPointerClickHandler ,IDropHandler
{
    [HideInInspector]
    public int index;
    GameObject inventoryObj;
    Inventory inventory;
    public GameObject Maincamera;
    public float distance = 7f;
    GameObject foodlWeapon;
    bool canPicUp;
    void Start()
    {
        inventoryObj = GameObject.FindGameObjectWithTag("InventoryManager");
        inventory = inventoryObj.GetComponent<Inventory>();
    }
    public void OnPointerClick(PointerEventData eventData) //wywalenie elementa
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if(inventory.item[index].id != 0)
            {
              //  GameObject dropedObj = Instantiate(Resources.Load<GameObject>(inventory.item[index].pathPrefab));
              //  dropedObj.transform.position = Camera.main.transform.position + Camera.main.transform.forward;
                if (inventory.item[index].countItem > 1)
                {
                    
                    inventory.item[index].countItem--;


                }
                else
                {
                    inventory.item[index] = new Item();
                }

            }
            
           
            inventory.DisplayItems();
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }
}
