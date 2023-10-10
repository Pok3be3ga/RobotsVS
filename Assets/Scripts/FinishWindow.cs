using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinishWindow : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _coinsText;
    [SerializeField] private Button _continueButton;
    private CoinCounter _coinCounter;
    private GameManager _gameManager;

    

    public void Show(CoinCounter coinCounter, GameManager gameManager)
    {
        gameObject.SetActive(true);
        _coinCounter = coinCounter;
        _gameManager = gameManager;
        SetCoinsText();
        _continueButton.onClick.AddListener(Continue);
    }

    public void Hide()
    {
    }


    private void SetCoinsText() {
        _coinsText.text = Mathf.RoundToInt(_coinCounter.NumberInLevel).ToString();
    }

    private void Continue()
    {
        SceneManager.LoadScene("Menu");
        _coinCounter.SaveToProgress();
    }
}
