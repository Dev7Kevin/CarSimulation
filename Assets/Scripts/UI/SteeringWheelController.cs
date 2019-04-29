using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class SteeringWheelController : MonoBehaviour
{
    private CarController m_CarController;

    private Vector3 m_FirstTouch;
    private Vector3 m_LastTouch;
    public float m_RotSpeed = 10f;

    private void Start()
    {
        m_CarController = GameObject.FindGameObjectWithTag("Player").GetComponent<CarController>();
        if (!m_CarController)
        {
            Debug.LogError(m_CarController);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            if (Input.GetMouseButtonDown(0))
            {
                m_FirstTouch.x = Input.mousePosition.x;
            }

            if (Input.GetMouseButton(0))
            {
                m_LastTouch.x = Input.mousePosition.x;

                float angle = (m_LastTouch.x - m_FirstTouch.x) * Time.deltaTime * m_RotSpeed;

                if (angle < 0)
                {
                    m_CarController.Left();
                }
                else if(angle > 0)
                {
                    m_CarController.Right();
                }

                transform.rotation = Quaternion.AngleAxis(angle, -transform.forward);
            }
        }
    }
}
