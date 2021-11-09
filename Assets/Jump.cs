public class Jump : Action
{
    protected override void instantiateAction()
    {
        Cooldown = 0.1f;
        Name = "Jump";
    }
}
