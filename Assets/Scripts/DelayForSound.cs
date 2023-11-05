using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayForSound : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _audioClip;
    private bool _isPlaying;

    public void PlaySound()
    {
        if (!_isPlaying)
        {
            StartCoroutine(PlaySoundWithDelay());            
        }
    }

    private IEnumerator PlaySoundWithDelay()
    {
        _isPlaying = true;
        _audioSource.PlayOneShot(_audioClip);
        yield return new WaitForSeconds(0.3f);
        _isPlaying = false;
    }
}
