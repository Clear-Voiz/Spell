using UnityEngine;

public class StaffRunRight : PawnBaseState
{
    const string Act = "Sura Staff Run Right";
    public StaffRunRight(PawnStateManager manager) : base(manager)
    {
        
    }

    public override void EnterState()
    {
        manager.netAnima.Play(Act,0);
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
        if(Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D))
            manager.SwitchState(manager.idle);
    }
}
