using UnityEngine;
using UnityEngine.EventSystems;

public class Rotator : MonoBehaviour
{
    [SerializeField] private EventTrigger trigger;

    [Header("Rotate")]
    [SerializeField, Min(0)] private float rotateSpeed = 1;
    private Transform roatetTransform;
    private float _rotationVelocity;
    private bool _dragged;

    private void Start()
    {
        roatetTransform = Tower.Instance.transform;

        EventTrigger.Entry dragEntry = new EventTrigger.Entry();
        dragEntry.eventID = EventTriggerType.Drag;
        dragEntry.callback.AddListener((data) => OnDrag((PointerEventData)data));
        trigger.triggers.Add(dragEntry);

        EventTrigger.Entry beginDragEntry = new EventTrigger.Entry();
        beginDragEntry.eventID = EventTriggerType.BeginDrag;
        beginDragEntry.callback.AddListener((data) => OnBeginDrag((PointerEventData)data));
        trigger.triggers.Add(beginDragEntry);

        EventTrigger.Entry endDragEntry = new EventTrigger.Entry();
        endDragEntry.eventID = EventTriggerType.EndDrag;
        endDragEntry.callback.AddListener((data) => OnEndDrag((PointerEventData)data));
        trigger.triggers.Add(endDragEntry);
    }

    protected virtual void OnBeginDrag(PointerEventData eventData)
    {
        _dragged = true;
    }

    protected virtual void OnDrag(PointerEventData eventData)
    {
        _rotationVelocity = eventData.delta.x * rotateSpeed;
        roatetTransform.Rotate(Vector3.up, -_rotationVelocity, Space.Self);
    }

    protected virtual void OnEndDrag(PointerEventData eventData)
    {
        _dragged = false;
    }
}
