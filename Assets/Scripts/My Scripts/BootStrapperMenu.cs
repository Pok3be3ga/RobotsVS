using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BootStrapperMenu : MonoBehaviour
{
    [SerializeField] Progress _progress;
    [SerializeField] PermanentProgress _permanentProgress;
    [SerializeField] TextMeshProUGUI _coinText;
    private void Awake()
    {
        _progress.Init();
    
    }
    private void Start()
    {
        _coinText.text = Progress.InstanceProgress.ProgressData.Coins.ToString();
    }
}
