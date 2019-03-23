using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// automatically add specific GameObject.
[RequireComponent(typeof (CarController))]
public class CarAudio : MonoBehaviour {

    public enum ENGINESOUNDSTYLE
    {
        Simple,
        FourChannel
    }

    public ENGINESOUNDSTYLE m_EngineSoundStyle = ENGINESOUNDSTYLE.FourChannel;
    public AudioClip m_LowAccelClip;
    public AudioClip m_LowDecelClip;
    public AudioClip m_HighAccelClip;
    public AudioClip m_HighDecelClip;
    public float m_PitchMultiplier = 1f;
    public float m_LowPitchMin = 1f;
    public float m_LowPitchMax = 6f;
    public float m_HighPitchMultiplier = 0.25f;
    public float m_MaxRolloffDistance = 500;
    public float m_DopplerLevel = 1;
    public bool m_UseDopper = true;

    private AudioSource m_LowAccel;
    private AudioSource m_LowDecel;
    private AudioSource m_HighAccel;
    private AudioSource m_HighDecel;
    private bool m_StarteSound = false;
    private float m_Volume;

    private CarController m_CarController;
    private SpeedManager m_SpeedMgr;

    private void Start()
    {
        m_SpeedMgr = SpeedManager.Instance;
        m_CarController = GetComponent<CarController>();
    }

    private void StartSound()
    {
        m_HighAccel = SetUpEngineAudioSource(m_HighAccelClip);

        if(m_EngineSoundStyle == ENGINESOUNDSTYLE.FourChannel)
        {
            m_LowAccel = SetUpEngineAudioSource(m_LowAccelClip);
            m_LowDecel = SetUpEngineAudioSource(m_LowDecelClip);
            m_HighDecel = SetUpEngineAudioSource(m_HighDecelClip);
        }

        m_StarteSound = true;
    }

    private void StopSound()
    {
        foreach(var source in GetComponents<AudioSource>())
        {
            Destroy(source);
        }

        m_StarteSound = false;
    }

	void Update ()
    {
        //float camDist = (Camera.main.transform.position - transform.position).sqrMagnitude;

        if (!m_StarteSound && m_SpeedMgr.Speed > 0)
        {
            StartSound();
        }
        else if(m_StarteSound && m_SpeedMgr.Speed <= 0)
        {
            StopSound();
        }

        if (m_StarteSound)
            FadeOut(m_HighAccel);
        else
            FadeIn(m_HighAccel);
    }

    private void FadeIn(AudioSource source)
    {
        if (source == null) return;

        m_Volume = m_SpeedMgr.Speed * Time.deltaTime;
        source.pitch = m_Volume;
        source.volume = 1;
    }

    private void FadeOut(AudioSource source)
    {
        if (source == null) return;

        m_Volume = m_SpeedMgr.Speed * Time.deltaTime;
        source.volume = m_Volume;
    }

    private AudioSource SetUpEngineAudioSource(AudioClip clip)
    {
        AudioSource source = gameObject.AddComponent<AudioSource>();
        source.clip = clip;
        source.volume = 0;
        source.loop = true;

        source.time = Random.Range(0f, clip.length);
        source.Play();
        source.minDistance = 5;
        source.maxDistance = m_MaxRolloffDistance;
        source.dopplerLevel = 0;

        return source;
    }

    private static float ULerp(float from, float to, float value)
    {
        return (1.0f - value) * from + value * to;
    }
}
