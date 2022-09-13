

using UnityEngine;

public class StaffRunBack : PawnBaseState
{
    const string Act = "Sura Staff Run Back";
    public StaffRunBack(PawnStateManager manager) : base(manager)
    {
        
    }

    public override void EnterState()
    {
        manager.netAnima.Play(Act);
        Debug.Log(manager.netAnima.Animator.GetCurrentAnimatorStateInfo(0).length);
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
        if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S))
        {
            manager.SwitchState(manager.idle);
        }
    }
}
