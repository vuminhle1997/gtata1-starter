namespace Actor
{
    public abstract class ActorCommand: IActorCommand
    {
        protected IActor actor;

        protected ActorCommand(IActor actor)
        {
            this.actor = actor;
        }

        /// <summary>
        /// Executes player's commands.
        /// Classes which inherit this abstract class need to override this method
        /// </summary>
        public abstract void Execute();
    }
}