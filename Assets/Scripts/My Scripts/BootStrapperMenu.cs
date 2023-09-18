using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BootStrapperMenu : MonoBehaviour
{
    [SerializeField] Progress _progress;
    [SerializeField] PermanentProgress _permanentProgress;
    [SerializeField] CoinCounter _coinCounter;
    private void Awake()
    {
        _progress.Init();
        _coinCounter.Init(_progress, _permanentProgress);
    }
}
