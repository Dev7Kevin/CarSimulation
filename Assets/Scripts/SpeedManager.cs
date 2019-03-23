using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpeedManager : MonoBehaviour {
    public static SpeedManager Instance { get; private set; }

    public int Speed { get; private set; }

    public  Text m_SpeedTxt;
    public int m_FontSize = 16;
    public  Rigidbody m_VehicleBody;

    void Awake()
    {
        if (Instance == null)
            Instance = this;

        m_SpeedTxt = GetComponent<Text>();
        Speed = 0;
    }

    private void Start()
    {
       m_SpeedTxt.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
        m_SpeedTxt.fontSize = m_FontSize;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_VehicleBody)
        {
            // check out about magnitude value!!
            Speed = (int)(m_VehicleBody.velocity.magnitude * 3.6f);
            m_SpeedTxt.text = string.Format("Speed: {0:0}", Speed);
        }
    }
}
