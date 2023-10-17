using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoinCounter : MonoBehaviour
{

    [DllImport("__Internal")]
    private static extern void AddCoinsExtern();


    [SerializeField] private Button _multiplyCoinsButton;
    [SerializeField] private Button _multiplyCoinsButton2;
    [SerializeField] private Transform _counterTransform;
    [SerializeField] private TextMeshProUGUI _coinsText;
    [SerializeField] private AnimationCurve _scaleCurve;

    [SerializeField] AudioSource _dontAudio;

    public ProgressData ProgressData;
    private Progress _progress;
    private PermanentProgress _permanentProgress;

    // ��� ����� ������ ���� float. ������ ��� �� ���������� ���� ����� ��������� ������� �������
    // ���� �� ����� int �� �������� 4 + 4% ����� ����� 4 � ������ �� ����������
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
        _progress.ProgressData.Coins += number;
        //CoinsInGame += number;

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
        Progress.InstanceProgress.ProgressData.Coins -= value;
        Display();
    }

    public void Display()
    {
         if(Progress.InstanceProgress.ProgressData.Coins != 0) 
            _coinsText.text = Progress.InstanceProgress.ProgressData.Coins.ToString();
    }
    public void SaveToProgress()
    {
        //SaveSystem.Save(_progress.ProgressData);
    }
    public void AddCoin()
    {
        AddCoinsExtern();
    }

    public void MultiplyCoins()
    {
        // ��� ���� ������� ����� �������

        if(_multiplyCoinsButton != null) _multiplyCoinsButton.gameObject.SetActive(false);
        if (_multiplyCoinsButton != null) _multiplyCoinsButton2.gameObject.SetActive(false);

        // �������� ���������� �����, ������������ �� ������ �� 3
        int coinsToAdd = Mathf.RoundToInt(NumberInLevel) * 3;
        AddCoins(coinsToAdd);
        Display();
#if UNITY_WEBGL
        Progress.InstanceProgress.Save();
#endif
    }


}
