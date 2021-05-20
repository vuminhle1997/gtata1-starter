namespace Actor
{
    public abstract class ActorCommand: IActorCommand
    {
        protected IActor actor;

        protected ActorCommand(IActor actor)
        {
            this.actor = actor;
        }

        public abstract void Execute();
    }
}