using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour,IDropHandler
{

    private Image _image;

    void Start()
    {
        _image = GetComponentInChildren<Image>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        var draggedImage = eventData.pointerDrag.GetComponent<Image>();
        if (draggedImage != null)
        {
            var tempPosition = _image.rectTransform.anchoredPosition;
            _image.rectTransform.anchoredPosition = draggedImage.rectTransform.anchoredPosition;
            draggedImage.rectTransform.anchoredPosition = tempPosition;
            SetImage(draggedImage.sprite);
        }
    }

    public void SetImage(Sprite sprite)
    {
        _image.sprite = sprite;
    }
}

