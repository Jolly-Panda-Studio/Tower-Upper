using JollyPanda.LastFlag.Handlers;
using System;
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
            Informant.OnStart += SetActive;
            Informant.OnWaveEnd += SetDeactive;
        }

        private void OnDisable()
        {
            Informant.OnStart -= SetActive;
            Informant.OnWaveEnd -= SetDeactive;
        }

        private void SetActive()
        {
            isActive = true;
        }

        private void SetDeactive(int arg1, int arg2)
        {
            isActive = false;
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