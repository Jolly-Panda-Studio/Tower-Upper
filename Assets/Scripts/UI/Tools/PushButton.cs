using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class PushButton : MonoBehaviour, IUpdateSelectedHandler, IPointerDownHandler, IPointerUpHandler
{
    private void OnEnable()
    {
        GetComponent<Image>().raycastTarget = true;
    }

    public bool isPressed { get; private set; }

    private event Action<bool> onPressed;

    public void RemoveAllListeners()
    {
        onPressed = null;
    }

    public void RemoveListener(Action<bool> action)
    {
        onPressed -= action;
    }

    public void AddListener(Action<bool> action)
    {
        onPressed += action;
    }

    public void OnUpdateSelected(BaseEventData data)
    {
        onPressed?.Invoke(isPressed);
    }
    public void OnPointerDown(PointerEventData data)
    {
        isPressed = true;
    }
    public void OnPointerUp(PointerEventData data)
    {
        isPressed = false;
    }
}
