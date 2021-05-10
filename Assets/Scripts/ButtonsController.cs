using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ButtonsController : MonoBehaviour
{
    [SerializeField] private GameStateMachine gameStateMachine;
    public void ButtonPressed(int type)
    {
        switch (type)
        {
            case 1:
                Debug.Log("Pause/Resume pressed");
                var state = gameStateMachine.GetCurrentState();
                if (state == GameState.Pause)
                {
                    gameStateMachine.Trigger(GameTransition.ResumePlaying);
                }
                else if (state == GameState.Play)
                {
                    gameStateMachine.Trigger(GameTransition.StopPlaying);
                }
                break;
            case 2:
                Debug.Log("Menu pressed");
                // gameStateMachine.Trigger(GameTransition.ShowMenu);
                break;
            default:
                throw new NotImplementedException("No button logic implemented");
        }
    }
}
