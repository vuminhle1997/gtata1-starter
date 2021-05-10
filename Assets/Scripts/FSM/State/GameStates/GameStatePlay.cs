using System.Collections.Generic;

namespace FSM.State.GameStates
{
    public class GameStatePlay : BaseState
    {
        private BaseSubState<GameStatePlay> _currentSubState;
        public Queue<BaseSubState<GameStatePlay>> SubStates { get; }

        public GameStatePlay(IStateMachine stateMachine) : base(stateMachine)
        {
            SubStates = new Queue<BaseSubState<GameStatePlay>>();
            SubStates.Enqueue(new GameStateRoundFight(this));
            SubStates.Enqueue(new GameStateRoundIntermission(this));
        }

        public void SetNextSubState()
        {
            _currentSubState?.OnExit();
            _currentSubState = SubStates.Dequeue();
        }
        
        public override void RunState()
        {
            SetNextSubState();
            _currentSubState.RunSubState();
        }
    }

    public class GameStateRoundFight : BaseSubState<GameStatePlay>
    {
        public GameStateRoundFight(GameStatePlay state) : base(state) { }

        public override void OnExit()
        {
            // after sub-state is done add it back to sub-state queue
            ParentState.SubStates.Enqueue(this);
        }
    }
    
    public class GameStateRoundIntermission : BaseSubState<GameStatePlay>
    {
        public GameStateRoundIntermission(GameStatePlay state) : base(state) { }

        public override void OnExit()
        {
            // after substate is done add it back to substate queue
            ParentState.SubStates.Enqueue(this);
        }
    }
}