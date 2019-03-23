using UnityEngine;
using System.Collections;

public class SimpleActivatorMenu : MonoBehaviour {

    public GUIText CamSwitchbutton;
    public GameObject[] Objects;

    private int m_CurrentActiveObject;

    private void OnEnable()
    {
        m_CurrentActiveObject = 0;
        CamSwitchbutton.text = Objects[m_CurrentActiveObject].name;
    }

    public void NextCamera()
    {
        int nextactiveobject = m_CurrentActiveObject + 1 >= Objects.Length ? 0 : m_CurrentActiveObject + 1;

        for (int i = 0; i < Objects.Length; i++)
        {
            Objects[i].SetActive(i == nextactiveobject);
        }

        m_CurrentActiveObject = nextactiveobject;
        CamSwitchbutton.text = Objects[m_CurrentActiveObject].name;
    }
}
