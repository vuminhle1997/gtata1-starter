using UnityEngine;

namespace FSM.State
{
    public abstract class BaseState : IBaseState
    {
        private IStateMachine stateMachine;
        protected IStateMachine StateMachine { get; private set; }
        
        protected BaseState(IStateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }
        
        /**
         * Execute this state
         */
        public virtual void RunState()
        {
#if UNITY_EDITOR
            
            Debug.Log($"running state {this}");
            
#endif
        }
    }

    public abstract class BaseSubState<TState>
    {
        private TState parentState;
        protected TState ParentState { get => parentState; private set => parentState = value; }

        protected BaseSubState(TState state)
        {
            ParentState = state;
        }

        #region OnEnter / OnExit Behavior

        /**
         * Define state entering behavior
         */
        public virtual void OnEnter()
        {
            return;
        }
        
        /**
         * Define state exiting behavior
         */
        public virtual void OnExit()
        {
            return;
        }

        #endregion
        
        /**
         * Execute sub state logic
         */
        public virtual void RunSubState()
        {
#if UNITY_EDITOR
            
            Debug.Log($"running sub-state {this}");
#endif
        }
    }
}