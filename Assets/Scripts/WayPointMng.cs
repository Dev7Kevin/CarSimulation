using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointMng : MonoBehaviour {
    public List<WayPoint> m_WayPoints;

    public List<WayPoint> GetWayPoints()
    {
        return m_WayPoints;
    }
}
