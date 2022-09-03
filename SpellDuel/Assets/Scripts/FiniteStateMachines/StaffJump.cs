

public class StaffJump : PawnBaseState
{
    private const string Act = "Sura Staff Jump";
    public StaffJump(PawnStateManager manager) : base(manager)
    {
    }

    public override void EnterState()
    {
        manager.netAnima.Play(Act);
    }

    public override void UpdateState()
    {
        
    }

    public override void ExitState()
    {
      
    }

    public override void CheckSwitchState()
    {
      
    }
}
