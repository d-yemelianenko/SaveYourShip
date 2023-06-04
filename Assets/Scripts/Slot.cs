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
        cell = _image.sprite;//defolna iconka 
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        DragDrop dragDrop = dropped.GetComponent<DragDrop>();
        dragDrop.parentAfterDrag = transform;
        //var draggedImage = eventData.pointerDrag.GetComponent<Image>();
        //if (draggedImage != null)
        //{
        //    var tempPosition = _image.rectTransform.anchoredPosition;
        //    _image.rectTransform.anchoredPosition = draggedImage.rectTransform.anchoredPosition;
        //    draggedImage.rectTransform.anchoredPosition = tempPosition;
        //    SetImage(draggedImage.sprite);
        //}
    }

    //public void SetImage(Sprite sprite)
    //{
    //    _image.sprite = sprite;
    //}

    public void OnPointerEnter(PointerEventData eventData)//kursor nad itemom
    {
        _image.sprite = activecell;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _image.sprite = cell;
    }
}

