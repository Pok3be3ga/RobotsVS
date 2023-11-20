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
    // Подписываемся на событие GetDataEvent в OnEnable
    private void OnEnable() => YandexGame.GetDataEvent += Disaplay;

    // Отписываемся от события GetDataEvent в OnDisable
    private void OnDisable() => YandexGame.GetDataEvent -= Disaplay;
    private void Awake()
    {
        _progress.Init();
    
    }
    private void Start()
    {
        Disaplay();

    }
    public void Disaplay()
    {
        _coinText.text = YandexGame.savesData.Coins.ToString();
        _levelText.text = YandexGame.savesData.Chapter.ToString();
    }
    public void GetLoad()
    {
    }

}
