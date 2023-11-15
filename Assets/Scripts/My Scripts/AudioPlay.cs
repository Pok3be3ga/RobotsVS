using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlay : MonoBehaviour
{
    [SerializeField] AudioSource m_Clip;
    [SerializeField] AudioClip m_AudioSource;
    void Start()
    {
        m_Clip.PlayOneShot(m_AudioSource);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
