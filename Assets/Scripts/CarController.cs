using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{

    public WheelCollider[] m_wheelColliders = new WheelCollider[4];
    public GameObject[] m_wheel = new GameObject[4];

    private Rigidbody m_Rigidbody;

    public float m_TopSpeed = 250f;
    float m_CurrentSpeed;

    public float m_MaxTorque = 200f;
    float m_CurrentTorque;
    public float m_Accel = 10f;
    private bool m_AccelCheck = false;

    public float m_MaxSteerAngle = 45f;
    float m_SteerAngle = 0.5f;
    private float m_CurrentSteerAngle;
    private bool m_AngleCheck = false;

    public float m_MaxBrakeTorque = 2200f;

    private float m_Forward { get; set; }
    private float m_Turn { get; set; }
    public float m_Brake { get; set; }

    // ResetPosition
    private List<WayPoint> m_WayPoints;

    private void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();

        WayPointMng WayPointMng = FindObjectOfType<WayPointMng>();
        m_WayPoints = WayPointMng.m_WayPoints;
    }
    public void Forward()
    {
        m_CurrentTorque += m_Accel;
        m_AccelCheck = true;
    }

    public void Back()
    {
        m_CurrentTorque -= m_Accel;
        m_AccelCheck = true;
    }

    public void GoingStop()
    {
        // decrease spped will be changed.
        m_CurrentTorque -= m_Accel / 2;
    }

    public void Right()
    {
        m_CurrentSteerAngle += m_SteerAngle;
        m_AngleCheck = true;
    }

    public void Left()
    {
        m_CurrentSteerAngle -= m_SteerAngle;
        m_AngleCheck = true;
    }

    public void GoingZeroAngle()
    {
        m_CurrentSteerAngle = 0f;
    }

    public void Brake()
    {
        m_Brake = m_MaxBrakeTorque;
    }

    private void AdjustMove()
    {
        m_CurrentTorque = Mathf.Clamp(m_CurrentTorque, 0, m_MaxTorque);
        m_Forward = m_CurrentTorque * Time.deltaTime;

        m_CurrentSteerAngle = Mathf.Clamp(m_CurrentSteerAngle, -m_MaxSteerAngle, m_MaxSteerAngle);
        m_Turn = m_CurrentSteerAngle * Time.deltaTime;

        m_AccelCheck = m_AngleCheck = false;
    }

    void Update()
    {
        Quaternion tempQuat;
        Vector3 tempVec3;

        for (int i = 0; i < 4; i++)
        {
            m_wheelColliders[i].GetWorldPose(out tempVec3,out tempQuat);
            m_wheel[i].transform.position = tempVec3;
            m_wheel[i].transform.rotation = tempQuat;
        }

        if (Input.GetKey(KeyCode.W))
        {
            Forward();
        }

        if (Input.GetKey(KeyCode.S))
        {
            Back();
        }

        if (!m_AccelCheck)
            GoingStop();

        if (Input.GetKey(KeyCode.A))
        {
            Left();
        }

        if (Input.GetKey(KeyCode.D))
        {
            Right();
        }

        if (!m_AngleCheck)
            GoingZeroAngle();

        if (Input.GetKey(KeyCode.Space))
        {
            Brake();
        }

        if (Input.GetKey(KeyCode.R))
        {
            ResetPosition();
        }


        AdjustMove();
        Move(m_Forward, m_Turn, m_Brake);

        if(Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    private void Move(float Forward, float Turn, float Brake)
    {
        m_wheelColliders[0].steerAngle = m_MaxSteerAngle * Turn;
        m_wheelColliders[1].steerAngle = m_MaxSteerAngle * Turn;

        if (m_CurrentSpeed < m_TopSpeed)
        {
            m_wheelColliders[0].motorTorque = m_MaxTorque * Forward;
            m_wheelColliders[1].motorTorque = m_MaxTorque * Forward;
        }

        for (int i = 0; i < 4; i++)
        {
            m_wheelColliders[i].brakeTorque = m_MaxBrakeTorque * Brake;
        }

        m_CurrentSpeed = m_Rigidbody.velocity.magnitude * 3.6f;
    }

    public void ResetPosition()
    {
        Transform closest = m_WayPoints[0].transform;

        for(int i = 0; i < m_WayPoints.Count; ++i)
        {
            Transform spawnTransform = m_WayPoints[i].transform;

            float distanceToClosest = Vector3.Distance(closest.position, transform.position);
            float distanceToThis = Vector3.Distance(spawnTransform.position, transform.position);

            if(distanceToThis < distanceToClosest)
            {
                closest = spawnTransform;
            }

            transform.rotation = closest.rotation;
            transform.position = closest.position;
        }
    }
}