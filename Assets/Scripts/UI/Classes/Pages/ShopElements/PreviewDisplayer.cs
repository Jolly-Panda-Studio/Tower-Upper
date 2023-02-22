using Lindon.TowerUpper.Data;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PreviewDisplayer : MonoBehaviour
{
    List<ShopModel> objects;
    [SerializeField] protected Transform parent;
    private GameObject storedObject = null;
    public virtual GameObject ActiveObject
    {
        get => storedObject;
        protected set
        {
            if (storedObject != null)
            {
                storedObject.SetActive(false);
            }
            storedObject = value;
            storedObject.SetActive(true);
        }
    }

    [Header("Rotate")]
    [SerializeField] private bool m_AutoRotate = false;
    [SerializeField] private Transform roatetTransform;
    [SerializeField, Min(0)] private float rotateSpeed = 1;
    [SerializeField] private float cooldown = 3;
    private float _rotationVelocity;
    private float _rotationVelocitx;
    private bool _dragged;
    private bool _autoRotate;
    private float _cooldown = 3;
    Quaternion startedRot;

    [SerializeField] private bool rotateX = false;
    [SerializeField] private bool rotateY = true;

    private void Start()
    {
        startedRot = roatetTransform.localRotation;
    }

    protected virtual void OnEnable()
    {
        _autoRotate = true;
        _cooldown = cooldown;
        roatetTransform.localRotation = startedRot;
    }

    private void OnDisable()
    {
        _autoRotate = false;
    }

    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject?.SetActive(false);
    }

    public void Setup(EventTrigger trigger = null)
    {
        objects = new List<ShopModel>();

        if (trigger != null)
        {
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

        Close();
    }

    #region Rotate & Drag

    protected virtual void OnBeginDrag(PointerEventData eventData)
    {
        _dragged = true;
        _autoRotate = false;
    }

    protected virtual void OnDrag(PointerEventData eventData)
    {
        if (rotateY)
        {
            _rotationVelocity = eventData.delta.x * rotateSpeed;
            roatetTransform.Rotate(Vector3.up, _rotationVelocity, Space.Self);
        }

        if (rotateX)
        {
            _rotationVelocitx = eventData.delta.y * rotateSpeed;
            roatetTransform.Rotate(Vector3.right, -_rotationVelocitx, Space.Self);
        }
    }

    protected virtual void OnEndDrag(PointerEventData eventData)
    {
        _dragged = false;
    }

    protected virtual void Update()
    {
        if (!m_AutoRotate) return;
        if (!_dragged && !_autoRotate)
        {
            if (_cooldown > 0)
            {
                _cooldown -= Time.deltaTime;
                if (_cooldown <= 0)
                {
                    _autoRotate = true;
                    _cooldown = cooldown;
                }
            }
        }
        if (!_dragged && _autoRotate)
        {
            roatetTransform.RotateAround(roatetTransform.position, roatetTransform.up, Time.deltaTime * 90f * rotateSpeed);
        }
    }

    #endregion

    public void Display(int itemId)
    {
        var obj = objects.Find(x => x.Equals(itemId));
        if (obj == null)
        {
            var prefab = GameData.Instance.GetPreviewModel(itemId);
            obj = Instantiate(prefab, parent);
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localEulerAngles = new Vector3(0, 180, 0);
            objects.Add(obj);

            ChangeLayer(obj.gameObject);
        }
        else
        {
            obj.gameObject.SetActive(true);
        }

        ActiveObject = obj.gameObject;
    }

    private void ChangeLayer(GameObject root)
    {
        root.SetLayerRecursively(LayerMask.NameToLayer("Preview"));
    }
}
