using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class Exit : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ExitGame();
            YandexGame.SaveProgress();
        }
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
