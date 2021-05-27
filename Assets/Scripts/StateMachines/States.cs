using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachines
{
    public enum GameState
    {
        Play,
        GameOver,
        Menu,
        Winning,
        LevelSuccess
    }

    public enum MenuState
    {
        Menu,
        Option,
        HighScore
    }

    public enum MenuStateTransition
    {
        ShowOptions,
        ShowMenu,
        ShowHighScore
    }

    public enum PlayerTransition
    {
        IsIdle,
        IsJumping,
        IsShooting,
        IsDeath,
        IsWalking,
        IsFallingDown
    }

    public enum PlayerState
    {
        Idle,
        Jump,
        Walk,
        Shoot,
        Death
    }

    public enum GameTransition
    {
        ResumePlaying,
        PausePlaying,
        ShowGameOver,
        ShowWinningScreen,
        ShowLevelSuccess,
        ReturnToMenu
    }
}