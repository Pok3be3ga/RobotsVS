using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using YG;

public class Reset : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _textMeshPro; 
    [SerializeField] TextMeshProUGUI _textMeshPro2;
    private void Start()
    {
        _textMeshPro.text = YandexGame.savesData.language.ToString();
        _textMeshPro2.text = YandexGame.EnvironmentData.language;
    }
    public void ResetProgress()
    {
        YandexGame.ResetSaveProgress();
        YandexGame.SaveProgress();
    }
}
