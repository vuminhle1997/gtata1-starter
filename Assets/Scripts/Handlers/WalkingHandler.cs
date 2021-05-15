using System.Collections.Generic;
using Player;
using StateMachines;
using UnityEngine;

namespace Handlers
{
    public class WalkingHandler : StateHandler
    {
        [SerializeField] private PlayerController playerController;
        public override void OnEnter(Dictionary<string, object> payload = null)
        {
            // throw new System.NotImplementedException();
            Debug.Log("Exit State: " + $"{PlayerState.Walk}");
        }

        public override void OnExit()
        {
            // throw new System.NotImplementedException();
            Debug.Log("Enter State: " + $"{PlayerState.Walk}");
        }

        /// <summary>
        /// source: https://answers.unity.com/questions/1117035/how-to-flip-2d-character-walk-movement.html
        /// </summary>
        private void FixedUpdate()
        {
            var dirX = playerController.GetDirX();

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

            var isShiftDown = Input.GetKey(KeyCode.LeftShift);
            if (isShiftDown)
            {
                playerController.TweakMovementSpeed(250f);
            }
            else
            {
                playerController.TweakMovementSpeed(100f);
            }
        }
    }
}
