using System;
using System.Collections.Generic;
using Player;
using StateMachines;
using UnityEngine;

namespace Handlers.PlayerHandler
{
    /// <summary>
    /// Toggles the player's jumping animation sprite.
    /// </summary>
    public class JumpingHandler : StateHandler
    {
        // [SerializeField] private PlayerStateMachine playerStateMachine;
        // [SerializeField] private PlayerController playerController;
        [SerializeField] private GameObject jumpSpriteGameObject;
        public override void OnEnter(Dictionary<string, object> payload)
        {
            Debug.Log("Exit State: " + $"{PlayerState.Jump}");
            jumpSpriteGameObject.SetActive(true);
        }

        public override void OnExit()
        {
            Debug.Log("Enter State: " + $"{PlayerState.Jump}");
            jumpSpriteGameObject.SetActive(false);
        }
    }
}