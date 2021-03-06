using System;
using System.Collections;
using System.Collections.Generic;
using Handlers;
using UnityEngine;

namespace StateMachines
{
    public class PlayerStateMachine : MonoBehaviour
    {
        [SerializeField] private PlayerState currentState;
        [SerializeField] private StateHandler jumpingHandler, walkingHandler, idleHandler;

        /// <summary>
        /// triggers a transition.
        /// if a trigger is submitted, perform the OnExit and OnEnter methods in the handler
        /// optional: pass a payload for further processing
        /// </summary>
        /// <param name="triggerType"></param>
        /// <param name="payload"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public bool Trigger(PlayerTransition triggerType, Dictionary<string, object> payload = null)
        {
            switch (triggerType)
            {
                case PlayerTransition.IsWalking:
                    if (currentState == PlayerState.Idle)
                    {
                        TransitionTo(PlayerState.Walk, payload);
                        return true;
                    }

                    return false;
                case PlayerTransition.IsIdle:
                    if (currentState == PlayerState.Idle || currentState == PlayerState.Walk)
                    {
                        TransitionTo(PlayerState.Idle, payload);
                        return true;
                    }

                    return false;
                case PlayerTransition.IsJumping:
                    if (currentState == PlayerState.Idle || currentState == PlayerState.Walk)
                    {
                        TransitionTo(PlayerState.Jump, payload);
                        return true;
                    }
            
                    return false;
                case PlayerTransition.IsFallingDown:
                    if (currentState == PlayerState.Jump)
                    {
                        TransitionTo(PlayerState.Idle, payload);
                        // StartCoroutine(DelayTransitionState(PlayerState.Idle, payload, 1f));
                        return true;
                    }

                    return false;
                default:
                    throw new ArgumentOutOfRangeException(nameof(triggerType), triggerType, null);
            }
        }

        /// <summary>
        /// makes transition to a specific state
        /// passed additional data for more details by injecting a Dictionary
        /// processes the payload in the corresponding handler class
        /// </summary>
        /// <param name="newState"></param>
        /// <param name="payload"></param>
        private void TransitionTo(PlayerState newState, Dictionary<string, object> payload = null)
        {
            if (newState == currentState)
            {
                return;
            }

            var currentHandler = GetHandler(currentState);
            var nextHandler = GetHandler(newState);
        
            currentHandler.OnExit();
            nextHandler.OnEnter(payload);

            currentState = newState;
        }

        #region Getters

        /// <summary>
        /// returns state's handler
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        private StateHandler GetHandler(PlayerState state)
        {
            switch (state)
            {
                case PlayerState.Idle:
                    return idleHandler;
                case PlayerState.Jump:
                    return jumpingHandler;
                case PlayerState.Walk:
                    return walkingHandler;
                case PlayerState.Shoot:
                    // TODO: Shoot State
                    return null;
                case PlayerState.Death:
                    // TODO: Death State
                    return null;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }

        /// <summary>
        /// returns player's current state
        /// </summary>
        /// <returns></returns>
        public PlayerState GetCurrentState()
        {
            return currentState;
        }

        #endregion
    }
}
