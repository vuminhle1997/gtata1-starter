namespace Actor
{
    /// <summary>
    /// Jump command
    /// </summary>
    public class JumpCommand: ActorCommand
    {
        public JumpCommand(IActor actor) : base(actor) {}

        public override void Execute()
        {
            actor.Jump();
        }
    }
}