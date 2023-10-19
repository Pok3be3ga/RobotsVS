using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource[] _sound;

    public void FindAudioSourceByClipName(string clipName)
    {
        Debug.LogError(Array.Find(_sound, source => source.clip != null && source.clip.name == clipName));

    }


}