using UnityEngine;
using System;

public abstract class PivotBaseCameraRig : AbstractTargetFollower
{
    protected Transform m_Cam;
    protected Transform m_Pivot;
    protected Vector3 m_LastTargetPosition;

    protected virtual void Awake()
    {
        m_Cam = GetComponentInChildren<Camera>().transform;
        m_Pivot = m_Cam.parent;
    }
}
