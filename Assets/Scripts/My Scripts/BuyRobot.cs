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
    [SerializeField] AudioSource _audioSourcePay;
    [SerializeField] BootStrapperMenu bootStrapperMenu;
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(BuyRobotButton);
        if (YandexGame.savesData.RobotBuy[NumberSaveRobots] == true)
        {
            gameObject.SetActive(false);
        }
        bootStrapperMenu.Disaplay();
    }
    public void BuyRobotButton()
    {
        if(YandexGame.savesData.Coins >= _price) 
        {
            YandexGame.savesData.Coins -= _price;
            YandexGame.savesData.RobotBuy[NumberSaveRobots] = true;
            gameObject.SetActive(false);
            textCoins.text = YandexGame.savesData.Coins.ToString();
            _audioSourcePay.Play();
        }
        else
        {
            _audioSourceClosed.Play();
        }
        YandexGame.SaveProgress();
        bootStrapperMenu.Disaplay();
    }
}
