using UnityEngine;

public class StaffRunRight : PawnBaseState
{
    private const string Act = "Sura Staff Run Right";
    public StaffRunRight(PawnStateManager manager) : base(manager)
    {
    }

    public override void EnterState()
    {
        manager.netAnima.Play(Act);
    }

    public override void UpdateState()
    {
        CheckSwitchState();
    }

    public override void ExitState()
    {
        
    }

    public override void CheckSwitchState()
    {
        if(Input.GetKeyUp(KeyCode.RightArrow))
            manager.SwitchState(manager.idle);
    }
}
