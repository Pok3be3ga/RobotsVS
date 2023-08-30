using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ProgressGame : MonoBehaviour
{
    public float Coins;
    public static ProgressGame Instance;
    public int index = 0;

    private void Awake()
    { 
        if (Instance == null)
        {
            transform.parent = null;
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


}
