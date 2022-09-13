


public class StaffPush : PawnBaseState
{
  
    const string Act = "Sura Staff Spell Impulse 02";
    private Timers tim;
    public StaffPush(PawnStateManager manager) : base(manager)
    {
        tim = new Timers(1);
    }

    public override void EnterState()
    {
        tim.alarm[0] = 0f;
        manager.netAnima.Play(Act);
    }

    public override void UpdateState()
    {
        tim.alarm[0] = tim.Timer(2f, tim.alarm[0], CheckSwitchState);
    }

    public override void ExitState()
    {
        
    }

    public override void CheckSwitchState()
    {
        manager.SwitchState(manager.idle);
    }
}
