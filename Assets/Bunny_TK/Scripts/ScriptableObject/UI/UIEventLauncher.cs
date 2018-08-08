using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIEventLauncher : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerClickHandler, IPointerUpHandler
{
    public event Action<UIEventLauncher> OnDrag;
    public event Action<UIEventLauncher> OnPointerDown;
    public event Action<UIEventLauncher> OnPointerUp;
    public event Action<UIEventLauncher> OnPointerClick;

    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        OnPointerUp?.Invoke(this);
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        OnDrag?.Invoke(this);
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        OnPointerClick?.Invoke(this);
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        OnPointerDown?.Invoke(this);
    }
}
