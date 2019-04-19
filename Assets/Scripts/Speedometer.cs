using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Speedometer : MonoBehaviour
{
    private const float MAX_SPEED_ANGLE = -40;
    private const float ZERO_SPEED_ANGLE = 230;

    private Transform m_Needle;
    private Transform m_SpeedLabel;

    private float m_SpeedMax;
    [SerializeField] private float m_Speed;

    private CarController m_CarController;

    private void Start()
    {
        m_CarController = GameObject.FindGameObjectWithTag("Player").GetComponent<CarController>();
        if (!m_CarController)
        {
            Debug.LogError(m_CarController);
        }

        m_Needle = transform.Find("Needle");
        m_SpeedLabel = transform.Find("SpeedLabel");
        m_SpeedLabel.gameObject.SetActive(false);

        m_Speed = 0f;
        m_SpeedMax = 200f;

        CreateSpeedLabels();
    }

    private void Update()
    {
        HandlePlayerInput();

        m_Needle.eulerAngles = new Vector3(0, 0, GetSpeedRotation());
    }

    private void CreateSpeedLabels()
    {
        int labelAmount = 10;
        float totalAngleSize = ZERO_SPEED_ANGLE - MAX_SPEED_ANGLE;

        for(int i = 0; i <= labelAmount; i++)
        {
            Transform speedLabel = Instantiate(m_SpeedLabel, transform);
            float labelSpeedNomalized = (float)i / labelAmount;
            float speedLabelAngle = ZERO_SPEED_ANGLE - labelSpeedNomalized * totalAngleSize;
            speedLabel.eulerAngles = new Vector3(0, 0, speedLabelAngle);
            speedLabel.Find("SpeedText").GetComponent<Text>().text = Mathf.RoundToInt(labelSpeedNomalized * m_SpeedMax).ToString();
            speedLabel.Find("SpeedText").eulerAngles = Vector3.zero;
            speedLabel.gameObject.SetActive(true);
        }

        m_Needle.SetAsLastSibling();
    }

    private void HandlePlayerInput()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            float acceleration = 80f;
            m_Speed += acceleration * Time.deltaTime;
        }
        else
        {
            float deceleration = 20f;
            m_Speed -= deceleration * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            float brakeSpeed = 100f;
            m_Speed -= brakeSpeed * Time.deltaTime;
        }

        m_Speed = m_CarController.m_CurrentSpeed * Time.deltaTime;
        if (m_Speed < 0f) m_Speed = 0f;
        else if (m_Speed > m_SpeedMax) m_Speed = m_SpeedMax;

        m_Speed = Mathf.Clamp(m_Speed, 0f, m_SpeedMax);
    }

    private float GetSpeedRotation()
    {
        float totalAngleSize = ZERO_SPEED_ANGLE - MAX_SPEED_ANGLE;

        float speedNomalized = m_Speed / m_SpeedMax;

        return ZERO_SPEED_ANGLE - speedNomalized * totalAngleSize;
    }
}
