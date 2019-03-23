using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ResetBtn : AbstractButton {

    private void Update()
    {
        if(m_IsBtnDown)
        {
            m_CarController.ResetPosition();
        }
    }
}
