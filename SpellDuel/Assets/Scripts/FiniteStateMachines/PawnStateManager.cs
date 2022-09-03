using System;
using FishNet.Component.Animating;
using FishNet.Object;
using UnityEngine;

public class PawnStateManager : NetworkBehaviour
{
    public Animator anima;
    public PawnBaseState currentState;
    
    //States Factory
    public StaffIdle idle;
    public StaffRunRight runRight;
    public StaffRunLeft runLeft;
    public StaffRunForward runForward;
    public StaffRunBack runBack;
    public StaffJump jump;
    public StaffStab stab;
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
        
        currentState = idle;
        currentState.EnterState();
    }
    
    private void Update()
    {
        if (!IsOwner) return;
        currentState.UpdateState();
    }

    public void SwitchState(PawnBaseState nextState)
    {
        if (!IsOwner) return;
        currentState.ExitState();
        currentState = nextState;
        currentState.EnterState();
    }
}
