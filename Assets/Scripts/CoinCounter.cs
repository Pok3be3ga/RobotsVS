using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class CoinCounter : MonoBehaviour
{

    //[DllImport("__Internal")]
    //private static extern void AddCoinsExtern();


    [SerializeField] private Button _multiplyCoinsButton;
    [SerializeField] private Button _multiplyCoinsButton2;
    [SerializeField] private Transform _counterTransform;
    [SerializeField] private TextMeshProUGUI _coinsText;
    [SerializeField] private AnimationCurve _scaleCurve;

    [SerializeField] AudioSource _dontAudio;
    private Progress _progress;
    private PermanentProgress _permanentProgress;

    // Ёто число должно быть float. ѕотому что из стартового меню можно увеличить процент прибыли
    // ≈сли он будет int то например 4 + 4% будет равно 4 и ничего не помен€етс€
    public float NumberInLevel { get; private set; }
    private void Start()
    {
        Display();
    }
    public void Init(Progress progress, PermanentProgress permanentProgress)
    {

        _progress = progress;
        _permanentProgress = permanentProgress;
        Display();
    }
    public void AddCoins(int number)
    {
        NumberInLevel += number * (1 + _permanentProgress.GetLoot());
        YandexGame.savesData.Coins += number;
        Display();
        StartCoroutine(CounterAnimation());
        

    }



    private IEnumerator CounterAnimation()
    {
        for (float t = 0; t < 1f; t += Time.unscaledDeltaTime * 2f)
        {
            float scale = _scaleCurve.Evaluate(t);
            _counterTransform.localScale = Vector3.one * scale;
            yield return null;
        }
        _counterTransform.localScale = Vector3.one;
    }

    public void SpendCoins(int value)
    {
        YandexGame.savesData.Coins -= value;
        Display();
    }

    public void Display()
    {
         if(YandexGame.savesData.Coins != 0) 
            _coinsText.text = YandexGame.savesData.Coins.ToString();
    }
    public void SaveToProgress()
    {
        YandexGame.SaveProgress();
        //SaveSystem.Save(_progress.ProgressData);
    }
    public void AddCoin()
    {
        YandexGame.RewVideoShow(0);
    }

    public void MultiplyCoins()
    {
        // “ут надо вызвать показ рекламы
        YandexGame.RewVideoShow(0);
        if (_multiplyCoinsButton != null) _multiplyCoinsButton.gameObject.SetActive(false);
        if (_multiplyCoinsButton != null) _multiplyCoinsButton2.gameObject.SetActive(false);

        // ”множаем количество монет, заработанных на уровне на 3
        int coinsToAdd = Mathf.RoundToInt(NumberInLevel) * 3;
        AddCoins(coinsToAdd);
        Display();
        YandexGame.SaveProgress();
    }


}
