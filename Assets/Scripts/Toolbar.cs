using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Toolbar : MonoBehaviour
{

    public RectTransform highlight;
    public ItemSlot[] itemSlots;

    int slotIndex = 0;

    private void Start()
    {
        //TODO wype³nianie slotów przedmiotami!
        foreach (ItemSlot slot in itemSlots)
        {
            //slot.icon.sprite = world.blocktypes[slotItemD].icon;
            slot.icon.enabled = true;
            //highlight.position = itemSlots[slotIndex].icon.transform.position + new Vector3(-10f, -10f, 0f);
        }
    }
    private void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if(scroll != 0)
        {
            if (scroll > 0)
                slotIndex++;
            else
                slotIndex--;
            if (slotIndex > itemSlots.Length - 1)
                slotIndex = 0;
            if (slotIndex < 0)
                slotIndex = itemSlots.Length - 1;

            highlight.position = itemSlots[slotIndex].icon.transform.position + new Vector3(-10f,-10f, 0f);
            //PlayerPrefs.selectedBlock
        }
    }

}

[System.Serializable]
public class ItemSlot
{
    public byte itemID;
    public Image icon;
}