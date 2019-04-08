using UnityEngine;
using System.Collections;

public class FreeLookCam : PivotBaseCameraRig
{
    [SerializeField]    private float m_MoveSpeed = 1f;
    [Range(100f, 500f)] [SerializeField]    private float m_turnSpeed = 200f;
    [SerializeField]    private float m_turnSmoothing = 0f;
    [SerializeField]    private float m_TiltMax = 75f;
    //[SerializeField]    private float m_TiltMin = 45f;
    [SerializeField]    private float m_ZoomMax = 20f;
    [SerializeField]    private float m_ZoomMin = 60f;
    [SerializeField]    private bool m_LockCursor = false;
    [SerializeField]    private bool m_VerticalAutoReturn = false;

    private float m_RotateX;
    private float m_RotateY;
    //private float m_MoveZ;
    
    private const float m_cLookDistanc = 100f;
    private Vector3 m_PivotEulers;
    private Quaternion m_PivotTargetRot;
    private Quaternion m_TransformTargetRot;

    private Camera m_Camera;

    protected override void Awake()
    {
        base.Awake();

        Cursor.lockState = m_LockCursor ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !m_LockCursor;
        m_PivotEulers = m_Pivot.rotation.eulerAngles;

        m_PivotTargetRot = m_Pivot.transform.localRotation;
        m_TransformTargetRot = transform.localRotation;

        m_Camera = GetComponentInChildren<Camera>();
    }

    protected void Update()
    {
        HandleRotationMovement();
        if (m_LockCursor && Input.GetMouseButton(0))
        {
            Cursor.lockState = m_LockCursor ? CursorLockMode.Locked : CursorLockMode.None;
        }
    }

    private void OnEnable()
    {
        Transform Player = GameObject.FindGameObjectWithTag("Player").transform;
        transform.LookAt(Player);
    }

    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    protected override void FollowTarget(float deltaTime)
    {
        if (m_Target == null) return;
        transform.position = Vector3.Lerp(transform.position, m_Target.position, deltaTime * m_MoveSpeed);
    }


    private void HandleRotationMovement()
    {
        if (Time.timeScale < float.Epsilon) return;

        //float x = Input.mousePosition.x;
        //float y = Input.mousePosition.y;

        float x = Input.GetAxis("Mouse X") * Time.deltaTime;
        float y = Input.GetAxis("Mouse Y") * Time.deltaTime;
        float z = Input.GetAxis("Mouse ScrollWheel") * m_MoveSpeed;

        m_RotateY += x * m_turnSpeed;
        // Only Rotate Y axis.
        m_TransformTargetRot = Quaternion.Euler(0f, m_RotateY, 0f);

        if (m_VerticalAutoReturn)
        {
            m_RotateX = y > 0 ? Mathf.Lerp(0, -m_TiltMax, y) : Mathf.Lerp(0, m_TiltMax, -y);
        }
        else
        {
            m_RotateX += y * m_turnSpeed;
            //m_TiltAngle = Mathf.Clamp(m_TiltAngle, -m_TiltMin, m_TiltMax);
        }

        // Only Rotate X axis
        m_PivotTargetRot = Quaternion.Euler(-m_RotateX, m_PivotEulers.y, m_PivotEulers.z);

        // Zoom
        if (m_Camera.fieldOfView <= m_ZoomMax && z < 0)
        {
            m_Camera.fieldOfView = m_ZoomMax;
        }
        else if (m_Camera.fieldOfView >= m_ZoomMin && z > 0)
        {
            m_Camera.fieldOfView = m_ZoomMin;
        }
        else
        {
            m_Camera.fieldOfView += z;
        }
        
        if (m_turnSmoothing > 0)
        {
            m_Pivot.localRotation = Quaternion.Slerp(m_Pivot.localRotation, m_PivotTargetRot, m_turnSmoothing * Time.deltaTime);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, m_TransformTargetRot, m_turnSmoothing * Time.deltaTime);
        }
        else
        {
            m_Pivot.localRotation = m_PivotTargetRot; // x
            transform.localRotation = m_TransformTargetRot; // y
        }

        //transform.Translate(0f, 0f, m_MoveZ);
    }
}
