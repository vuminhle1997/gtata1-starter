using FSM.GameStateMachine;
using FSM.State;
using FSM.State.GameStates;
using ScriptableObjects;
using UnityEngine;

namespace Test
{
    public class TesterClass : MonoBehaviour
    {
        private GameStateMachine gameStateMachine;

        private void Awake()
        {
            gameStateMachine = FindObjectOfType<GameStateMachine>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                StateEvent.TriggerStateChange(
                    gameStateMachine.GetState(typeof(GameStatePause)));
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                StateEvent.TriggerStateChange(
                    gameStateMachine.GetState(typeof(GameStatePlay)));
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                StateEvent.TriggerStateChange(
                    gameStateMachine.GetState(typeof(GameStateExit)));
            }
        }
    }
}