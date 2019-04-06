using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    public static SoundManager Instace { get; private set; }

    private AudioSource m_AudioSource;

	// Use this for initialization
	void Start () {
        if (Instace == null)
            Instace = this;

        m_AudioSource = GetComponent<AudioSource>();
        m_AudioSource = gameObject.AddComponent<AudioSource>();        
    }

    public void FadeIn(AudioClip audioClip, float fadeTime)
    {
        if (m_AudioSource.clip != audioClip)
        {
            m_AudioSource.clip = audioClip;
            m_AudioSource.volume = 0;
            m_AudioSource.loop = true;
            m_AudioSource.Play();
        }

        if (m_AudioSource.volume >= 1f)
            return;


        while (m_AudioSource.volume < 1)
        {
            m_AudioSource.volume += Mathf.Lerp(0f, 1f, Time.deltaTime / fadeTime);
            return;
        }

        m_AudioSource.volume = 1f;
    }

    public void FadeOut(AudioClip audioClip, float fadeTime)
    {
        if(m_AudioSource.clip != audioClip)
        {
            m_AudioSource.clip = audioClip;
        }

        if (m_AudioSource.volume < 0f)
            return;

        while(m_AudioSource.volume > 0f)
        {
            m_AudioSource.volume -= Mathf.Lerp(0f, 1f, Time.deltaTime / fadeTime);
            return;
        }

        m_AudioSource.volume = 0f;
        if(m_AudioSource.isPlaying)
        {
            m_AudioSource.Stop();
        }
    }

    public bool IsAudioPlaying()
    {
        return  m_AudioSource.isPlaying;
    }
}
