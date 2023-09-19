using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSell : MonoBehaviour
{
    [SerializeField] CoinCounter CoinCounter;
    [SerializeField] AudioSource _dontAudio;
    [SerializeField] Button _button;


    private void Update()
    {
        if(Progress.InstanceProgress.ProgressData.Coins < 100)
        {
            _button.interactable = false;
        }
    }
    public void BuyRobot1()
    {
        if (Progress.InstanceProgress.ProgressData.Coins > 100)
        {
            Progress.InstanceProgress.ProgressData.Coins -= 100;
            CoinCounter.Display();
            Destroy(gameObject);
        }
        else
        {
            _dontAudio.Play();
        }
    }
    public void Die()
    {
        Destroy(gameObject);
    }
}
