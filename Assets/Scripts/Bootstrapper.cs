using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bootstrapper : MonoBehaviour
{

    [SerializeField] private Progress _progress;
    [SerializeField] private PermanentProgress _permanentProgress;
    [SerializeField] private GameStateManager _gameStateManager;
    [SerializeField] private CoinCounter _coinCounter;
    [SerializeField] private ChapterDisplay _chapterDisplay;
    [SerializeField] private EnvironmentManager _environmentManager;

    private void Awake()
    {
        _progress = FindAnyObjectByType<Progress>();
        _progress.Init();
        _permanentProgress.Init(_progress, _coinCounter);
        _gameStateManager.Init();
        _coinCounter.Init(_progress, _permanentProgress);
        _environmentManager.Init();
    }

}
