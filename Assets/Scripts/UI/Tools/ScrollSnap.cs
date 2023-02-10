using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(EventTrigger))]
public class ScrollSnap : MonoBehaviour
{
    [SerializeField] private RectTransform content;
    private List<Transform> children;
    Transform current;
    int index = 0;

    [Header("Buttons")]
    [Tooltip("Button to go to the previous page (optional)")]
    [SerializeField] private Button prevButton;
    [Tooltip("Button to go to the next page (optional)")]
    [SerializeField] private Button nextButton;

    [Header("Page Selection")]
    [Tooltip("Container with page images (optional)")]
    [SerializeField] private RectTransform pageSelectionIcons;
    [Tooltip("Sprite for unselected page")]
    public Sprite unselectedPage;
    [Tooltip("Sprite for selected page")]
    public Sprite selectedPage;
    Image[] images;

    private Vector2 startedPos;
    private Vector2 finishedPos;

    public Action<int> OnPanelOpen;

    public int Index
    {
        get => index;
        set
        {
            index = value;
            if (Index >= children.Count)
            {
                Index = children.Count - 1;
            }
            else if (Index < 0)
            {
                Index = 0;
            }

            current?.gameObject.SetActive(false);
            current = children[Index];
            current.gameObject.SetActive(true);

            OnPanelOpen?.Invoke(index);
        }
    }

    public void Setup()
    {
        if (content.childCount == 0) return;

        children = new List<Transform>();
        for (var i = 0; i < content.childCount; i++)
        {
            var child = content.GetChild(i);
            child.gameObject.SetActive(false);
            children.Add(child);
        }

        if(pageSelectionIcons != null)
        {
            //var missingImageCount = children.Count - pageSelectionIcons.childCount;
            for (int i = pageSelectionIcons.childCount; i < children.Count; i++)
            {
                var newImageObject = new GameObject($"Point ({i + 1})");
                newImageObject.transform.SetParent(pageSelectionIcons);
                newImageObject.transform.SetAsLastSibling();
                newImageObject.AddComponent<Image>().sprite = unselectedPage;
            }

            images = new Image[children.Count];
            for (var i = 0; i < pageSelectionIcons.childCount; i++)
            {
                var child = pageSelectionIcons.GetChild(i);
                images[i] = child.gameObject.GetComponent<Image>();
            }
            OnPanelOpen += SetOn;
        }

        //hasSelection = children.Count == pageSelectionIcons.childCount;
        //if (hasSelection)
        //{
        //    images = new Image[children.Count];
        //    for (var i = 0; i < pageSelectionIcons.childCount; i++)
        //    {
        //        var child = pageSelectionIcons.GetChild(i);
        //        images[i] = child.gameObject.GetComponent<Image>();
        //    }
        //    OnPanelOpen += SetOn;
        //}

        SetEventTrigger();

        SetButtons();

        Index = 0;
    }

    #region EventTrigger

    private void SetEventTrigger()
    {
        var m_EventTrigger = GetComponent<EventTrigger>();
        EventTrigger.Entry beginDragEntry = new EventTrigger.Entry();
        beginDragEntry.eventID = EventTriggerType.BeginDrag;
        beginDragEntry.callback.AddListener((data) => { OnBeginDragDelegate((PointerEventData)data); });
        m_EventTrigger.triggers.Add(beginDragEntry);

        EventTrigger.Entry endDragEntry = new EventTrigger.Entry();
        endDragEntry.eventID = EventTriggerType.EndDrag;
        endDragEntry.callback.AddListener((data) => { OnEndDragDelegate((PointerEventData)data); });
        m_EventTrigger.triggers.Add(endDragEntry);
    }

    private void OnBeginDragDelegate(PointerEventData data)
    {
        startedPos = data.position;
    }

    private void OnEndDragDelegate(PointerEventData data)
    {
        finishedPos = data.position;
        CalculateDrag();
    }

    private void CalculateDrag()
    {
        if (startedPos.x > finishedPos.x)
        {
            // drag to right
            Index++;
        }
        else if (startedPos.x < finishedPos.x)
        {
            // drag to left
            Index--;
        }
        else
        {
            // nothing
        }
    }

    #endregion

    private void SetButtons()
    {
        if (prevButton == null || nextButton == null)
        {
            return;
        }

        prevButton.onClick.RemoveAllListeners();
        prevButton.onClick.AddListener(() =>
        {
            index--;
        });

        nextButton.onClick.RemoveAllListeners();
        nextButton.onClick.AddListener(() =>
        {
            index++;
        });
    }

    private void SetOn(int index)
    {
        for (var i = 0; i < images.Length; i++)
        {
            images[i].sprite = i == index ? selectedPage : unselectedPage;
        }
    }
}