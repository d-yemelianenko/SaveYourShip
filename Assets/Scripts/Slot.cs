using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDropHandler
{
    public Sprite activecell;
    Sprite cell;
    Image _image;

    void Start()
    {
        _image = GetComponent<Image>();
        cell = _image.sprite; //dowolna iconka 
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        DragDrop dragDrop = dropped.GetComponent<DragDrop>();
        dragDrop.parentAfterDrag = transform;
    }

    public void OnPointerEnter(PointerEventData eventData) //kursor nad itemem
    {
        _image.sprite = activecell;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _image.sprite = cell;
    }
}

