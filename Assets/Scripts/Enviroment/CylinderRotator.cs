using UnityEngine;

namespace JollyPanda.LastFlag.EnviromentModule
{
    public class CylinderRotator : MonoBehaviour
    {
        public float rotationSpeed = 100f;
        private Vector2 lastMousePosition;

        void Update()
        {
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