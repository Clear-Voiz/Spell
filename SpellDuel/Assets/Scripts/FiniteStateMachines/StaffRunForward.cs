
using UnityEngine;

public class StaffRunForward : PawnBaseState
{
    const string Act = "Sura Staff Run Forward";
    
    public StaffRunForward(PawnStateManager manager) : base(manager)
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
        if (Input.GetKeyUp(KeyCode.UpArrow)|| Input.GetKeyUp(KeyCode.W))
        {
            manager.SwitchState(manager.idle);
        }
    }
}
