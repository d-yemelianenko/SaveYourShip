using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragDrop : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public static GameObject dragedObject;
    public Sprite emptySlotIcon;
    Inventory inventory;
    void Start()
    {
        
        inventory = GameObject.FindGameObjectWithTag("InventoryManager").GetComponent<Inventory>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        dragedObject = gameObject;
        inventory.dragPrefab.SetActive(true);
       // inventory.dragPrefab.GetComponent<CanvasGroup>().blocksRaycasts = false;
        if (dragedObject.GetComponent<CurrentItem>())
        {
            int index = dragedObject.GetComponent<CurrentItem>().index;
           // inventory.dragPrefab.SetActive(true);
            inventory.dragPrefab.GetComponent<Image>().sprite = inventory.item[index].icon;
            //if(inventory.dragPrefab.GetComponent<Image>().sprite == null)
            //{
            //    dragedObject = null;
            //   // inventory.dragPrefab.SetActive(false);
            //}
           
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        inventory.dragPrefab.SetActive(true);
        inventory.dragPrefab.transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        dragedObject = null ;
       // inventory.dragPrefab.SetActive(false);
      // inventory.dragPrefab.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }


    public void OnDrop(PointerEventData eventData)
    {
        if (dragedObject != null)
        {
            Image droppedSlotImage = GetComponent<Image>();
            Image draggedSlotImage = dragedObject.GetComponent<Image>();
           // inventory.dragPrefab.SetActive(true);

            // Sprawdzanie czy slot docelowy jest pusty
            if (droppedSlotImage.sprite == emptySlotIcon)
            {
                // Zmiana ikony slotu docelowego na ikonê przeci¹ganego przedmiotu
                droppedSlotImage.sprite = draggedSlotImage.sprite;

                // Usuniêcie ikony przeci¹ganego przedmiotu z oryginalnego slotu
                draggedSlotImage.sprite = null;
            }
            else
            {
                // Zamiana ikon miêdzy slotami
                Sprite tempSprite = droppedSlotImage.sprite;
                droppedSlotImage.sprite = draggedSlotImage.sprite;
                draggedSlotImage.sprite = tempSprite;
                inventory.dragPrefab.SetActive(true);
            }
        }
    }
}
