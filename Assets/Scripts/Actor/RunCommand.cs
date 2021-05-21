namespace Actor
{
    public class RunCommand: ActorCommand
    {
        public RunCommand(IActor actor) : base(actor)
        {
            
        }

        public override void Execute()
        {
            actor.Run();
        }
    }
}