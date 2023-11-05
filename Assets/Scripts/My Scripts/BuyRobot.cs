using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class BuyRobot : MonoBehaviour
{
    [SerializeField] int NumberSaveRobots;
    [SerializeField] int _price;
    [SerializeField] TextMeshProUGUI textCoins;
    [SerializeField] AudioSource _audioSourceClosed;
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(BuyRobotButton);
        if (YandexGame.savesData.RobotBuy[NumberSaveRobots] == true)
        {
            gameObject.SetActive(false);
        }
    }
    public void BuyRobotButton()
    {
        if(YandexGame.savesData.Coins >= _price) 
        {
            YandexGame.savesData.Coins -= _price;
            YandexGame.savesData.RobotBuy[NumberSaveRobots] = true;
            gameObject.SetActive(false);
            textCoins.text = YandexGame.savesData.Coins.ToString();
        }
        else
        {
            _audioSourceClosed.Play();
        }
        YandexGame.SaveProgress();
    }
}
