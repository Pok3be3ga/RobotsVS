using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioVolume : MonoBehaviour
{
    [SerializeField] AudioMixerGroup musicMixer;
    float musicVolume;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void SetVolume(float volume)
    {
        musicMixer.audioMixer.SetFloat("Other", Mathf.Lerp(-80, 0, volume));
    }
    public void SetVolumeMusic(float volume)
    {
        musicMixer.audioMixer.SetFloat("Music", Mathf.Lerp(-80, 0, volume));
    }
}
