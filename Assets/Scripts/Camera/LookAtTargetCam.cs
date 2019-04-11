using System;
using UnityEngine;

public class LookAtTargetCam : AbstractTargetFollower
{
    [SerializeField]    private Vector2 m_RotationRange;
    [SerializeField]    private float m_FollowSpeed = 1;

    private Vector3 m_FollowAngles;
    private Quaternion m_OriginalRotation;

    protected Vector3 m_FollowVelocity;

    private Camera m_Camera;
    public float m_Speed = 10f;

    protected override void Start()
    {
        base.Start();
        m_OriginalRotation = transform.localRotation;
        m_Camera = GetComponent<Camera>();
    }

    protected override void FollowTarget(float deltaTime)
    {
        transform.localRotation = m_OriginalRotation;

        Vector3 localTarget = transform.InverseTransformPoint(m_Target.position);
        float yAngle = Mathf.Atan2(localTarget.x, localTarget.z) * Mathf.Rad2Deg;

        yAngle = Mathf.Clamp(yAngle, -m_RotationRange.y * 0.5f, m_RotationRange.y * 0.5f);
        transform.localRotation = m_OriginalRotation * Quaternion.Euler(0, yAngle, 0);

        localTarget = transform.InverseTransformPoint(m_Target.position);
        float xAngle = Mathf.Atan2(localTarget.y, localTarget.z) * Mathf.Rad2Deg;
        xAngle = Mathf.Clamp(xAngle, -m_RotationRange.x * 0.5f, m_RotationRange.x * 0.5f);
        var targetAngles = new Vector3(m_FollowAngles.x + Mathf.DeltaAngle(m_FollowAngles.x, xAngle),
                                       m_FollowAngles.y + Mathf.DeltaAngle(m_FollowAngles.y, yAngle));

        m_FollowAngles = Vector3.SmoothDamp(m_FollowAngles, targetAngles, ref m_FollowVelocity, m_FollowSpeed);

        transform.localRotation = m_OriginalRotation * Quaternion.Euler(-m_FollowAngles.x, m_FollowAngles.y, 0);
    }

    private void Update()
    {
        Zoom();
    }

    public void Zoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel") * m_Speed;
        
        if (m_Camera.fieldOfView <= 20f && scroll > 0)
        {
            m_Camera.fieldOfView = 20f;
        }
        else if(m_Camera.fieldOfView >= 85f && scroll < 0)
        {
            m_Camera.fieldOfView = 85f;
        }
        else
        {
            m_Camera.fieldOfView -= scroll;
        }
    }
}