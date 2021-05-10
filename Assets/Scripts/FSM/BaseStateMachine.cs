using System.Collections.Generic;
using FSM.State;
using ScriptableObjects;
using UnityEngine;

namespace FSM
{
    /**
     * Every state machine will know all its states from the start. The states will not change during the game and
     * can therefore be instantiated at the start of the game as a reference inside the respective state machine
     * they belong to. In this way we save resources instantiating the new next currentState every time a state changes. 
     */
    public abstract class BaseStateMachine : MonoBehaviour, IStateMachine
    {
        private IBaseState currentState;
        public IBaseState CurrentState { get => currentState; protected set => currentState = value; }

        [SerializeField] protected List<BaseState> States;
        private void Awake()
        {
            States = new List<BaseState>();
            Initialize();
        }

        /**
         * Virtual method to be used to add states to the used states of the state machine
         */
        protected virtual void Initialize()
        {
            // add states to List of states used in state machine
        }
        
        #region OnEnable / OnDisable

        private void OnEnable()
        {
            StateEvent.OnStateChange += DoState;
        }
        
        private void OnDisable()
        {
            StateEvent.OnStateChange -= DoState;
        }

        #endregion
        
        public virtual void DoState(IBaseState state)
        {
            // do state
            currentState = state;
            currentState.RunState();
        }
    }
}