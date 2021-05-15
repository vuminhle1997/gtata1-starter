using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachines
{
    public enum GameState
    {
        Play,
        GameOver,
        Pause,
        Menu,
        Winning
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
        ShowHighscore
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
        StopPlaying,
        ShowGameOver,
        ShowIntro,
        ShowWinningScreen,
        StartGame
    }
}