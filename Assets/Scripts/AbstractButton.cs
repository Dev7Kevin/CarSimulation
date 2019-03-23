using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class AbstractButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public CarController m_CarController;

    protected bool m_IsBtnDown = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        m_IsBtnDown = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        m_IsBtnDown = false;
    }

}