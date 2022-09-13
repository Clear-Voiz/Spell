using UnityEngine;

public class StaffShortAndFast : PawnBaseState
{
    public const string Act = "ShortAndFast";
    private AnimatorStateInfo stateInfo;
    private Timers tim;
    
    public StaffShortAndFast(PawnStateManager manager) : base(manager)
    {
        tim = new Timers(1);
    }

    public override void EnterState()
    {
        tim.alarm[0] = 0f;
        manager.netAnima.Animator.SetLayerWeight(1,1f);
        manager.netAnima.Play(Act,1);
    }

    public override void UpdateState()
    {
        tim.alarm[0] = tim.Timer(0.667f, tim.alarm[0], CheckSwitchState);
    }

    public override void ExitState()
    {
        //manager.netAnima.Animator.SetLayerWeight(1,0f);
    }

    public override void CheckSwitchState()
    {
        manager.netAnima.Animator.SetLayerWeight(1,0f);
        //manager.SwitchState(manager.idle);
    }
}
