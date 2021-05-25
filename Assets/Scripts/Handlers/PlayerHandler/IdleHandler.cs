using System.Collections.Generic;
using Player;
using StateMachines;
using UnityEngine;

namespace Handlers.PlayerHandler
{
    /// <summary>
    /// Toggles the player's idle animation sprites.
    /// </summary>
    public class IdleHandler : StateHandler
    {
        // [SerializeField] private PlayerController playerController;
        [SerializeField] private GameObject idleSpriteGameObject;
        public override void OnExit()
        {
            Debug.Log("Exit State: "+$"{PlayerState.Idle}");
            idleSpriteGameObject.SetActive(false);
        }

        public override void OnEnter(Dictionary<string, object> payload = null)
        {
            Debug.Log("Enter State: "+$"{PlayerState.Idle}");
            idleSpriteGameObject.SetActive(true);
        }
    }
}
