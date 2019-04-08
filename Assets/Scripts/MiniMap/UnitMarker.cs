using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMarker : MonoBehaviour {

    public GameObject m_Unit;

	// Use this for initialization
	void Start () {
        if (!m_Unit)
            Debug.Log(m_Unit.name + "Null");		
	}
	
	// Update is called once per frame
	void Update () {
        if (m_Unit)
        {
            transform.position = new Vector3(m_Unit.transform.position.x, transform.transform.position.y, m_Unit.transform.position.z);
            transform.forward = m_Unit.transform.forward;
        }
	}
}
