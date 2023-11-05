using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class MenuState : GameState
{

    //[SerializeField] private GameObject _startMenuObject;
    [SerializeField] private StartMenu _startMenu;
    [SerializeField] private ChapterDisplay _chapterDisplay;

    protected override void EnterFirstTime()
    {
        //base.EnterFirstTime();
        Debug.Log(_gameStateManager);
        _startMenu.Init(_gameStateManager);
        _chapterDisplay.Set(YandexGame.savesData.Chapter);
    }



    public override void Enter()
    {
        base.Enter();
        _startMenu.Show();
    }

    public override void Exit()
    {
        _startMenu.Hide();
    }

}
