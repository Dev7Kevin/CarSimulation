using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Start_Game : MonoBehaviour {

    private SoundManager m_SoundMgr;
    private bool m_GoNextScene = false;

    public UISlider m_UiSlider;

    public AudioClip m_BMClip;
    [Range(0f, 100f)] public float m_FadeTime;

    private void Start()
    {
        m_SoundMgr = SoundManager.Instace;
        if (!m_SoundMgr)
            return;

        if (!m_UiSlider)
            return;
    }

    private void Update()
    {
        if (!m_GoNextScene)
        {
            m_SoundMgr.FadeIn(m_BMClip, m_FadeTime);
            m_UiSlider.value = 0;
        }
           
        else
        {
            m_SoundMgr.FadeOut(m_BMClip, m_FadeTime);
            if(!m_SoundMgr.IsAudioPlaying())
            {
                StartGamePlay();
            }

            if (m_UiSlider.value < 1f)
                m_UiSlider.value += Mathf.Lerp(0f, 1f, Time.deltaTime / m_FadeTime);
        }
    }

    public void StartGamePlay()
    {
        SceneManager.LoadScene("GamePlay");
    }

    public void GoNextScene()
    {
        m_GoNextScene = true;
    }
}
