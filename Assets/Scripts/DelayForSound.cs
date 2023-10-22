using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayForSound : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    private bool _isPlaying;

    public void PlaySound()
    {
        if (!_isPlaying)
        {
            StartCoroutine(PlaySoundWithDelay());
            _isPlaying = false;
        }
    }

    private IEnumerator PlaySoundWithDelay()
    {
        _isPlaying = true;
        _audioSource.Play();
        yield return new WaitForSeconds(0.3f);
    }
}
