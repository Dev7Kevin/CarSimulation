using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ForwardBtn : AbstractButton {

    private void Update()
    {
        if(m_IsBtnDown)
        {
            m_CarController.Forward();
        }
    }
}
