using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public void StartGameGo()
    {
        SceneManager.LoadScene("Chapter 1");
    }

    public void GameMenu()
    {
        SceneManager.LoadScene("Menu");
    }

}
