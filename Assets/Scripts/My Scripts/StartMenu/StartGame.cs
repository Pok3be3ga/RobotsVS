using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    [SerializeField] CoinCounter _coinCounter;
    [SerializeField] ChoseRobots _choseRobots;
    public void StartGameGo()
    {
        SceneManager.LoadScene("Chapter 1");
        _coinCounter.SaveToProgress();
        _choseRobots.SaveRobotIndex();
    }

    public void GameMenu()
    {
        
        SceneManager.LoadScene("Menu");
        _coinCounter.SaveToProgress();
    }

}
