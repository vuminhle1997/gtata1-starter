using System.Collections.Generic;
using Player;
using StateMachines;
using UnityEngine;

namespace Handlers
{
    public class IdleHandler : StateHandler
    {
        [SerializeField] private PlayerController playerController;
        public override void OnExit()
        {
            Debug.Log("Exit State: "+$"{PlayerState.Idle}");
        }

        public override void OnEnter(Dictionary<string, object> payload = null)
        {
            Debug.Log("Enter State: "+$"{PlayerState.Idle}");
        }
    }
}
