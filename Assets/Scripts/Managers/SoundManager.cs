using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    private AudioClip m_HitBlock;

    private AudioSource m_AudioSource;

    public static SoundManager m_Instance;

    private void Start()
    {
        m_Instance = this;
        m_AudioSource = GetComponent<AudioSource>();
    }

    public void BlockDestroyed()
    {
        m_AudioSource.clip = m_HitBlock;
        m_AudioSource.Play();
    }

}
