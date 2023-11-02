using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource[] _skillsSounds;

    public AudioSource FindAudioSourceByClipName(string clipName)
    {
        return (Array.Find(_skillsSounds, source => source.clip != null && source.clip.name == clipName));
    }

    public bool CheckAvailability(string clipName)
    {
        return (Array.Find(_skillsSounds, source => source.clip != null && source.clip.name == clipName));
    }
}