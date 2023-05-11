using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragDrop : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private RectTransform _rectTransform;
    private Canvas _mainCanvas;
    private RectTransform _slotRectTransform;
    private Transform _currentSlotTransform;

    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _mainCanvas = GetComponentInParent<Canvas>();
        _slotRectTransform = transform.parent.GetComponent<RectTransform>();
        _currentSlotTransform = transform.parent;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta / _mainCanvas.scaleFactor;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _rectTransform.SetParent(_mainCanvas.transform);
        _rectTransform.anchoredPosition += eventData.delta / _mainCanvas.scaleFactor;
        _currentSlotTransform = transform.parent;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        var slot = eventData.pointerEnter.GetComponent<Slot>();
        if (slot != null && slot.transform != _currentSlotTransform)
        {
            _rectTransform.SetParent(slot.transform);
            _rectTransform.anchoredPosition = Vector2.zero;
            _currentSlotTransform = slot.transform;
        }
        else
        {
            _rectTransform.SetParent(_slotRectTransform);
            _rectTransform.anchoredPosition = Vector2.zero;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnEndDrag(eventData);
    }
}
