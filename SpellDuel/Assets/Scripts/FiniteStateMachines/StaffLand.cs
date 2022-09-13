
public class StaffLand : PawnBaseState
{
    const string Act = "Sura Staff Run Left";
    public StaffLand(PawnStateManager manager) : base(manager)
    {
        
    }

    public override void EnterState()
    {
        manager.anima.Play(Act);
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
