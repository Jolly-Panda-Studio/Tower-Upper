using RootMotion.FinalIK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AimIK))]
public class PlayerAimingAssistant : MonoBehaviour
{
    [SerializeField] private AimIK m_AimIK;

    private void Awake()
    {
        m_AimIK = GetComponent<AimIK>();
    }

    public void SetAimTarget(Transform aimTarget)
    {
        m_AimIK.solver.target = aimTarget;
    }

    public void SetAimTransform(Transform aimTransform)
    {

    }
}
