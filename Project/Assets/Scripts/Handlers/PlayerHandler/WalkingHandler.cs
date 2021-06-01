using System;
using System.Collections.Generic;
using Player;
using StateMachines;
using UnityEngine;

namespace Handlers.PlayerHandler
{
    /// <summary>
    /// Toggles the player's walking animation sprite.
    /// </summary>
    public class WalkingHandler : StateHandler
    {
        [SerializeField] private PlayerController playerController;
        [SerializeField] private GameObject walkingSpriteGameObject;
        
        public override void OnEnter(Dictionary<string, object> payload = null)
        {
#if UNITY_EDITOR
            Debug.Log("Exit State: " + $"{PlayerState.Walk}");
#endif
            walkingSpriteGameObject.SetActive(true);
        }

        public override void OnExit()
        {
#if UNITY_EDITOR
            Debug.Log("Enter State: " + $"{PlayerState.Walk}");
#endif
            walkingSpriteGameObject.SetActive(false);
        }

        private void Update()
        {
            var dirX = playerController.GetDirX();

            // switches the sprite's the direction based on the faced vector direction
            if (dirX < 0f)
            {
                playerController.transform.eulerAngles = new Vector3(0, 180, 0);
            }
            else if (dirX > 0f)
            {
                playerController.transform.eulerAngles = new Vector3(0, 0, 0);
            }

            playerController.transform.position = new Vector2(playerController.transform.position.x + dirX, 
                playerController.transform.position.y);
        }
    }
}
