using FishNet.Component.Animating;
using FishNet.Object;
using UnityEngine;

public class PawnStateManager : NetworkBehaviour
{
    public Animator anima;
    public PawnBaseState currentState;
    public PawnBaseState currentBattleState;
    
    //States Factory
    public StaffIdle idle;
    public StaffRunRight runRight;
    public StaffRunLeft runLeft;
    public StaffRunForward runForward;
    public StaffRunBack runBack;
    public StaffJump jump;
    public StaffStab stab;
    public StaffObjectLift objectLift;
    public StaffPush staffPush;
    public StaffShortAndFast shortAndFast;
    public NetworkAnimator netAnima;

    public override void OnStartClient()
    {
        base.OnStartClient();
        if (!IsOwner) return;
        idle = new StaffIdle(this);
        runRight = new StaffRunRight(this);
        runLeft = new StaffRunLeft(this);
        runForward = new StaffRunForward(this);
        runBack = new StaffRunBack(this);
        jump = new StaffJump(this);
        stab = new StaffStab(this);
        objectLift = new StaffObjectLift(this);
        staffPush = new StaffPush(this);
        shortAndFast = new StaffShortAndFast(this);
        
        
        currentState = idle;
        currentState.EnterState();
    }
    
    private void Update()
    {
        if (!IsOwner) return;
        currentState.UpdateState();
        if(currentBattleState == null) return;
        currentBattleState.UpdateState();
    }

    public void SwitchState(PawnBaseState nextState)
    {
        if (!IsOwner) return;
        currentState.ExitState();
        currentState = nextState;
        currentState.EnterState();
    }

    public void SwitchBattleState(PawnBaseState nextState)
    {
        if(!IsOwner) return;
        currentBattleState = nextState;
        nextState.EnterState();
    }
}
