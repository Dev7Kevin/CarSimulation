using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpeedManager : MonoBehaviour {
    public static SpeedManager Instance { get; private set; }

    public int Speed { get; private set; }

    public  Rigidbody m_VehicleBody;

    void Awake()
    {
        if (Instance == null)
            Instance = this;

        Speed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_VehicleBody)
        {
            // check out about magnitude value!!
            Speed = (int)(m_VehicleBody.velocity.magnitude * 3.6f);
        }
    }
}
