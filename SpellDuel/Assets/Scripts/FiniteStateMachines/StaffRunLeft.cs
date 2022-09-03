using UnityEngine;

public class StaffRunLeft : PawnBaseState
{
    private const string Act = "Sura Staff Run Left";
    public StaffRunLeft(PawnStateManager manager) : base(manager)
    {
    }

    public override void EnterState()
    {
        manager.netAnima.Play(Act);
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
        if(Input.GetKeyUp(KeyCode.LeftArrow))
            manager.SwitchState(manager.idle);
    }
}
