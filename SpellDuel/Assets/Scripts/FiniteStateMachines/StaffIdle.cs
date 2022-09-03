

using UnityEngine;

public class StaffIdle : PawnBaseState
{
    private const string Act = "Sura Staff Idle";
    public StaffIdle(PawnStateManager manager) : base(manager)
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
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            manager.SwitchState(manager.runRight);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            manager.SwitchState(manager.runLeft);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            manager.SwitchState(manager.runForward);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            manager.SwitchState(manager.runBack);
        }
        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            manager.SwitchState(manager.jump);
        }*/
    }
}
