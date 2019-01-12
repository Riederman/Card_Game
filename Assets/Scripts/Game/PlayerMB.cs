public class PlayerMB : ActorMB
{
    protected override int GetSelectedIndex()
    {
        return -1;
    }

    protected override bool IsPlayer()
    {
        return true;
    }
}