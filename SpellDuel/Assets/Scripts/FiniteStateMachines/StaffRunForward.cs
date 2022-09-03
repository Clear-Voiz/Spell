
using UnityEngine;

public class StaffRunForward : PawnBaseState
{
    private const string Act = "Sura Staff Run Forward";
    
    public StaffRunForward(PawnStateManager manager) : base(manager)
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
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            manager.SwitchState(manager.idle);
        }
    }
}
