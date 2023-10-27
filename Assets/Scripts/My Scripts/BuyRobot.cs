using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyRobot : MonoBehaviour
{
    [SerializeField] int NumberSaveRobots;
    [SerializeField] int _price;
    [SerializeField] TextMeshProUGUI textCoins;
    [SerializeField] AudioSource _audioSourceClosed;
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(BuyRobotButton);
        if (Progress.InstanceProgress.ProgressData.RobotBuy[NumberSaveRobots] == true)
        {
            gameObject.SetActive(false);
        }
    }
    public void BuyRobotButton()
    {
        if(Progress.InstanceProgress.ProgressData.Coins >= _price) 
        {
            Progress.InstanceProgress.ProgressData.Coins -= _price;
            Progress.InstanceProgress.ProgressData.RobotBuy[NumberSaveRobots] = true;
            gameObject.SetActive(false);
            textCoins.text = Progress.InstanceProgress.ProgressData.Coins.ToString();
        }
        else
        {
            _audioSourceClosed.Play();
        }
    }
}
