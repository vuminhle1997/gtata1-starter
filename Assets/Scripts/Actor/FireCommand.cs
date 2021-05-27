namespace Actor
{
    /// <summary>
    /// Fire bullet command
    /// </summary>
    public class FireCommand: ActorCommand
    {
        public FireCommand(IActor actor) : base(actor)
        {
            
        }

        public override void Execute()
        {
            actor.FireBullet();
        }
        
    }
}