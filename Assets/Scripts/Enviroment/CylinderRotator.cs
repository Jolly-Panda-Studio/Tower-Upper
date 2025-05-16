using JollyPanda.LastFlag.Handlers;
using UnityEngine;

namespace JollyPanda.LastFlag.EnviromentModule
{
    public class CylinderRotator : MonoBehaviour
    {
        public float rotationSpeed = 100f;

        [Header("Step Rotation Settings")]
        public bool useStepRotation = false;
        public float rotationStepSize = 15f;

        [Header("Reset Settings")]
        public float resetDuration = 1f;

        private Vector2 lastMousePosition;
        private bool isActive = false;
        private Quaternion defaultRotation;

        private bool isResetting = false;
        private float resetTimer = 0f;
        private Quaternion startRotation;

        private void Awake()
        {
            defaultRotation = transform.rotation;
        }

        private void OnEnable()
        {
            Informant.OnChangeUIPage += SwitchActive;
        }

        private void OnDisable()
        {
            Informant.OnChangeUIPage -= SwitchActive;
        }

        private void SwitchActive(PageType type)
        {
            switch (type)
            {
                case PageType.HUD:
                    isActive = true;
                    break;
                case PageType.Home:
                    isActive = false;
                    ResetRotation();
                    break;
            }
        }

        void Update()
        {
            if (isResetting)
            {
                resetTimer += Time.deltaTime;
                float t = Mathf.Clamp01(resetTimer / resetDuration);
                transform.rotation = Quaternion.Slerp(startRotation, defaultRotation, t);

                if (t >= 1f)
                    isResetting = false;

                return;
            }

            if (!isActive)
                return;

            if (Input.GetMouseButtonDown(0))
            {
                lastMousePosition = Input.mousePosition;
            }
            else if (Input.GetMouseButton(0))
            {
                Vector2 delta = (Vector2)Input.mousePosition - lastMousePosition;
                float rotationAmount = -delta.x * rotationSpeed * Time.deltaTime;

                if (useStepRotation)
                {
                    float step = Mathf.Round(rotationAmount / rotationStepSize) * rotationStepSize;
                    transform.Rotate(Vector3.up, step);
                }
                else
                {
                    transform.Rotate(Vector3.up, rotationAmount);
                }

                lastMousePosition = Input.mousePosition;
            }
        }

        public void ResetRotation()
        {
            isResetting = true;
            resetTimer = 0f;
            startRotation = transform.rotation;
        }
    }
}
