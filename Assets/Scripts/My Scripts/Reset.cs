using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using YG;

public class Reset : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _textMeshPro;
    private void Start()
    {
        _textMeshPro.text = YandexGame.EnvironmentData.language;
    }
    public void ResetProgress()
    {
        YandexGame.ResetSaveProgress();
        YandexGame.SaveProgress();
    }
}
