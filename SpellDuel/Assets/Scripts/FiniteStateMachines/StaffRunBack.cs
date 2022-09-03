

using UnityEngine;

public class StaffRunBack : PawnBaseState
{
    private const string Act = "Sura Staff Run Back";
    public StaffRunBack(PawnStateManager manager) : base(manager)
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
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            manager.SwitchState(manager.idle);
        }
    }
}
