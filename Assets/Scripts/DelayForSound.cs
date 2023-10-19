using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayForSound : MonoBehaviour
{
    public AudioClip sound;
    [SerializeField] private AudioSource _audioSource;

    public void PlaySound()
    {
        StartCoroutine(PlaySoundWithDelay());
    }

    private IEnumerator PlaySoundWithDelay()
    {
        yield return new WaitForSeconds(Random.Range(0.1f, 0.3f)); 
        _audioSource.PlayOneShot(sound);
    }
}
