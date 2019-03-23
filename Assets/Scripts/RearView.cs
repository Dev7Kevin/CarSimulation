using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RearView : MonoBehaviour {

    private Camera m_RearView;

    private void Awake()
    {
        m_RearView = GetComponent<Camera>();
    }

    // OnPreCull() -> OnpreRender() -> Render Objects -> OnPostRender()
    public void OnPreCull()
    {
        m_RearView.ResetProjectionMatrix();
        m_RearView.projectionMatrix = m_RearView.projectionMatrix * Matrix4x4.Scale(new Vector3(-1, 1, 1));
    }

    public void OnPreRender()
    {
         // Opposit BackSpace Culling
        GL.invertCulling = true;
    }

    public void OnPostRender()
    {
        GL.invertCulling = false;
    }
}
