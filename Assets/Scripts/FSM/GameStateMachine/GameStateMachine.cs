using System;
using System.Collections.Generic;
using FSM.State;
using FSM.State.GameStates;
using UnityEngine;

namespace FSM.GameStateMachine
{
    public class GameStateMachine : BaseStateMachine
    {
        protected override void Initialize()
        {
            States.AddRange(
                new List<BaseState>
                { new GameStatePause(this), new GameStatePlay(this), new GameStateExit(this) }
            );
        }

        public IBaseState GetState(Type type)
        {
            return States.Find(state => state.GetType() == type);
        }
    }
}