using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LeftBtn : AbstractButton {

    private void Update()
    {
        if(m_IsBtnDown)
        {
            m_CarController.Left();
        }
    }
}
