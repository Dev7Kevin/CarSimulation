using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCMove : MonoBehaviour {

    public Transform m_Destination;

    NavMeshAgent m_NavMeshAgent;


	// Use this for initialization
	void Start () {
        m_NavMeshAgent = GetComponent<NavMeshAgent>();

        if(m_NavMeshAgent )        
        {
            SetDestination();
        }
	}
	
	private void SetDestination()
    {
        if(m_Destination != null)
        {
            Vector3 targetVector = m_Destination.transform.position;
            m_NavMeshAgent.SetDestination(targetVector);
        }
    } 
}
