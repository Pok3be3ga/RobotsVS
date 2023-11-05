using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using YG;

public class BootStrapperMenu : MonoBehaviour
{
    [SerializeField] Progress _progress;
    [SerializeField] PermanentProgress _permanentProgress;
    [SerializeField] TextMeshProUGUI _coinText;
    [SerializeField] TextMeshProUGUI _levelText;
    private void Awake()
    {
        _progress.Init();
    
    }
    private void Update()
    {
        _coinText.text = YandexGame.savesData.Coins.ToString();
        _levelText.text = YandexGame.savesData.Chapter.ToString();

    }
}
