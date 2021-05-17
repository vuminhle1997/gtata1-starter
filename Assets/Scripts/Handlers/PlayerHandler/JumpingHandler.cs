using System.Collections.Generic;
using Player;
using StateMachines;
using UnityEngine;

namespace Handlers.PlayerHandler
{
    public class JumpingHandler : StateHandler
    {
        [SerializeField] private PlayerStateMachine playerStateMachine;
        [SerializeField] private PlayerController playerController;
    
        public override void OnEnter(Dictionary<string, object> payload)
        {
            Debug.Log("Exit State: " + $"{PlayerState.Jump}");
        }

        public override void OnExit()
        {
            Debug.Log("Enter State: " + $"{PlayerState.Jump}");
        }

        private void FixedUpdate()
        {
            if (playerController.isGrounded && playerStateMachine.GetCurrentState() == PlayerState.Jump)
            {
                playerController.Jump();    
            }
        }
    }
}