namespace Actor
{
    /// <summary>
    /// Run command
    /// </summary>
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