using Lindon.TowerUpper.GameController.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lindon.TowerUpper.Enviroment
{
    public class SkyBoxChanger : MonoBehaviour
    {
        [SerializeField] private List<Material> m_SkyBoxes;

        private void OnEnable()
        {
            ReturnHome.OnReturnHome += ChangeSkyBox;
        }

        private void OnDisable()
        {
            ReturnHome.OnReturnHome += ChangeSkyBox;
        }

        [ContextMenu("Change")]
        private void ChangeSkyBox()
        {
            RenderSettings.skybox = m_SkyBoxes.RandomItem();
            DynamicGI.UpdateEnvironment();
        }
    }
}