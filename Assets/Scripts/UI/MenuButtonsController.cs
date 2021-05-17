using System;
using System.Collections;
using System.Collections.Generic;
using StateMachines;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace UI
{
    public class MenuButtonsController : MonoBehaviour
    {
        [SerializeField] private MenuStateMachine menuStateMachine;

        public void OnClick(int i)
        {
            switch (i)
            {
                case 1:
                    Debug.Log("Start Game");
                    SceneManager.LoadScene("Scenes/Scene - 01", LoadSceneMode.Single);
                    break;
                case 2:
                    Debug.Log("Enter option panel");
                    Dictionary<string, object> payload = new Dictionary<string, object>();
                    menuStateMachine.Trigger(MenuStateTransition.ShowOptions, payload);
                    break;
                case 3:
                    // todo: go back to main menu
                    Debug.Log("Go back to menu");
                    Dictionary<string, object> _payload = new Dictionary<string, object>();
                    menuStateMachine.Trigger(MenuStateTransition.ShowMenu, _payload);
                    break;
                case 4:
                    Debug.Log("Enter HighScore board");
                    menuStateMachine.Trigger(MenuStateTransition.ShowHighscore);
                    break;
                case 5:
                    // todo: exit game
                    Debug.Log("Exit Game");
                    break;
                default:
                    throw new NotImplementedException("Not implemented");
            }
        }
    }
}
