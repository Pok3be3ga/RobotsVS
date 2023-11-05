using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class StartGame : MonoBehaviour
{
    //[SerializeField] CoinCounter _coinCounter;
    [SerializeField] ChoseRobots _choseRobots;
    private void Start()
    {
        YandexGame.NewLeaderboardScores("LevelsPrefs", YandexGame.savesData.Chapter);
    }
    public void StartGameGo()
    {
        SceneManager.LoadScene("Chapter 1");
        //_coinCounter.SaveToProgress();
        _choseRobots.SaveRobotIndex();
        YandexGame.SaveProgress();
    }

    public void GameMenu()
    {
        
        SceneManager.LoadScene("Menu");
        YandexGame.SaveProgress();
        //_coinCounter.SaveToProgress();
    }

}
