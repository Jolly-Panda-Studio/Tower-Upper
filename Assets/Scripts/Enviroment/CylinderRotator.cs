using JollyPanda.LastFlag.Handlers;
using UnityEngine;

namespace JollyPanda.LastFlag.EnviromentModule
{
    public class CylinderRotator : MonoBehaviour
    {
        public float rotationSpeed = 100f;
        private Vector2 lastMousePosition;

        private bool isActive = false;

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
                    break;
            }
        }

        void Update()
        {
            if (!isActive)
                return;

            if (Input.GetMouseButtonDown(0))
            {
                lastMousePosition = Input.mousePosition;
            }
            else if (Input.GetMouseButton(0))
            {
                Vector2 delta = (Vector2)Input.mousePosition - lastMousePosition;
                transform.Rotate(Vector3.up, -delta.x * rotationSpeed * Time.deltaTime);
                lastMousePosition = Input.mousePosition;
            }
        }
    }
}