using UnityEngine;

public class StaffRunLeft : PawnBaseState
{
    const string Act = "Sura Staff Run Left";
    public StaffRunLeft(PawnStateManager manager) : base(manager)
    {
        
    }

    public override void EnterState()
    {
        manager.netAnima.Play(Act);
        Debug.Log(manager.netAnima.Animator.GetCurrentAnimatorStateInfo(0).length);
        
        //manager.anima.CrossFade();
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
        if(Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A))
            manager.SwitchState(manager.idle);
    }
}
