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
                    SceneManager.LoadScene("Scenes/Scene - 01", LoadSceneMode.Single);
                    break;
                case 2:
                    Dictionary<string, object> payload = new Dictionary<string, object>();
                    menuStateMachine.Trigger(MenuStateTransition.ShowOptions, payload);
                    break;
                case 3:
                    Dictionary<string, object> _payload = new Dictionary<string, object>();
                    menuStateMachine.Trigger(MenuStateTransition.ShowMenu, _payload);
                    break;
                case 4:
                    menuStateMachine.Trigger(MenuStateTransition.ShowHighscore);
                    break;
                case 5:
                    #if UNITY_EDITOR
                        Debug.Log("Quit Game");
                    #elif UNITY_STANDALONE
                        Application.Quit(); 
                    #endif
                    break;
                default:
                    throw new NotImplementedException("Not implemented");
            }
        }
    }
}
