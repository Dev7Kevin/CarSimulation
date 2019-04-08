using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCMoveWaypoint : MonoBehaviour {

    [SerializeField] bool m_PatrolWaiting;
    [SerializeField] float m_TotalWaitTime = 3.0f;
    //[SerializeField] float m_SwitchProbablility = 2.0f;

    // ResetPosition
    private List<WayPoint> m_WayPoints;

    NavMeshAgent m_NavMeshAgent;
    int m_CurWayPointIndex;
    bool m_Trabelling;
    bool m_Waiting;
    bool m_Forward = true;
    float m_WaitTimer;

	// Use this for initialization
	void Start () {
        m_NavMeshAgent = this.GetComponent<NavMeshAgent>();
        if(m_NavMeshAgent)
        {
            WayPointMng WayPointMng = FindObjectOfType<WayPointMng>();
            m_WayPoints = WayPointMng.GetWayPoints();

            if (m_WayPoints != null && m_WayPoints.Count >= 2)
            {
                m_CurWayPointIndex = 0;
                SetDestination();
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
		if(m_Trabelling && m_NavMeshAgent.remainingDistance <= 1f)
        {
            m_Trabelling = false;

            if(m_PatrolWaiting)
            {
                m_Waiting = true;
                m_WaitTimer = 0f;
            }
            else
            {
                ChangeWaypoint();
                SetDestination();
            }

            if(m_Waiting)
            {
                m_WaitTimer += Time.deltaTime;
                if(m_WaitTimer >= m_TotalWaitTime)
                {
                    m_Waiting = false;

                    ChangeWaypoint();
                    SetDestination();
                }
            }
        }
	}

    private void SetDestination()
    {
        if (m_WayPoints != null)
        {
            Vector3 targetVector = m_WayPoints[m_CurWayPointIndex].transform.position;
            m_NavMeshAgent.SetDestination(targetVector);
            m_Trabelling = true;
        }
    }

    private void ChangeWaypoint()
    {
        /*
        if(Random.Range(0f, 1f) <= m_SwitchProbablility)
        {
            m_Forward = !m_Forward;
        }
        */
        if(m_Forward)
        {
            m_CurWayPointIndex = (m_CurWayPointIndex + 1) % m_WayPoints.Count;
        }
        else
        {
            if(--m_CurWayPointIndex < 0)
            {
                m_CurWayPointIndex = m_WayPoints.Count - 1;
            }
        }
    }
}
