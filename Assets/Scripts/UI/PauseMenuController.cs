using System;
using ScriptableObjects;
using UnityEngine;

namespace UI
{
    
    [Obsolete("Not needed currently, but maybe later?")]
    public class PauseMenuController : MonoBehaviour
    {
        [SerializeField] private GameObject pauseMenu;

        #region OnEnable / OnDisable

        private void OnEnable()
        {
            pauseMenu.SetActive(false);
            GlobalEvents.OnPauseGame += PauseGame;
        }

        private void OnDisable()
        {
            GlobalEvents.OnPauseGame -= PauseGame;
        }

        #endregion
        
        private void PauseGame()
        {
            pauseMenu.SetActive(!pauseMenu.activeSelf);
        }
    }
}