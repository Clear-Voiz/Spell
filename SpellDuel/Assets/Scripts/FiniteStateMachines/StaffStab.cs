using UnityEngine;

public class StaffStab : PawnBaseState
{
    private const string Act = "Sura Staff Stab";
    private AnimatorStateInfo stateInfo;
    private Timers tim;
    
    public StaffStab(PawnStateManager manager) : base(manager)
    {
    }

    public override void EnterState()
    {
        manager.netAnima.Play(Act);
        tim = new Timers(1);
        stateInfo = manager.anima.GetCurrentAnimatorStateInfo(0);
        Debug.Log(stateInfo.length);
        
    }

    public override void UpdateState()
    {
        tim.alarm[0] = tim.Timer(2.13f, tim.alarm[0], CheckSwitchState);
    }

    public override void ExitState()
    {
       
    }

    public override void CheckSwitchState()
    {
        manager.SwitchState(manager.idle);
    }
}
