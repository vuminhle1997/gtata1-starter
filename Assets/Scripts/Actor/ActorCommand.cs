namespace Actor
{
    public abstract class ActorCommand
    {
        protected IActor actor;

        protected ActorCommand(IActor actor)
        {
            this.actor = actor;
        }

        public abstract void Execute();
    }
}