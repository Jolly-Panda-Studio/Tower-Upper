using Lindon.TowerUpper.GameController;
using UnityEngine;
using UnityEngine.EventSystems;

public class Rotator : MonoBehaviour
{
    [SerializeField] private EventTrigger trigger;

    [Header("Rotate")]
    [SerializeField, Min(0)] private float rotateSpeed = 1;
    private Transform roatetTransform;
    private float _rotationVelocity;

    private void Start()
    {
        roatetTransform = GameManager.Instance.Tower.transform;

        EventTrigger.Entry dragEntry = new EventTrigger.Entry();
        dragEntry.eventID = EventTriggerType.Drag;
        dragEntry.callback.AddListener((data) => OnDrag((PointerEventData)data));
        trigger.triggers.Add(dragEntry);
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rotationVelocity = eventData.delta.x * rotateSpeed;
        roatetTransform.Rotate(Vector3.up, -_rotationVelocity, Space.Self);
        Debug.Log(_rotationVelocity);
    }
}
