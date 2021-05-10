using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Play,
    GameOver,
    Pause,
    Menu,
    Winning
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
    StartGame,
    ShowMenu
}

public interface IStateHandler
{
    void OnEnter(Dictionary<string, object> payload = null);
    void OnExit();
}