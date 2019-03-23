using UnityEngine;

public class BrakeLight : MonoBehaviour {
    public CarController m_Car;

    private Renderer m_Renderer;

    // Use this for initialization
    void Start () {
        m_Renderer = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
        m_Renderer.enabled = m_Car.m_Brake > 0f;
    }
}
