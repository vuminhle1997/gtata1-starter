using FSM.State;

namespace FSM
{
    /**
     * Base state machine interface
     */
    public interface IStateMachine
    {
        /**
         * Execute the current state of the state machine
         * <param name="state">State to execute</param>
         */
        public void DoState(IBaseState state);
    }
}
