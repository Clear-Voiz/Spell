

public class StaffObjectLift : PawnBaseState
{
    const string  Act = "Sura Staff Rise";
    private Timers tim;
    
    public StaffObjectLift(PawnStateManager manager) : base(manager)
    {
        tim = new Timers(1);
    }

    public override void EnterState()
    {
        tim.alarm[0] = 0f;
        manager.netAnima.Animator.SetLayerWeight(1,1f);
        manager.netAnima.Play(Act,1);

    }

    public override void UpdateState()
    {
        tim.alarm[0] = tim.Timer(0.867f, tim.alarm[0], CheckSwitchState);
    }

    public override void ExitState()
    {
       
    }

    public override void CheckSwitchState()
    {
        manager.netAnima.Animator.SetLayerWeight(1,0f);
    }
}
