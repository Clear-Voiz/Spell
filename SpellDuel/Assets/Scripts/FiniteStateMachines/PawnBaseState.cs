
public abstract class PawnBaseState
{
    protected PawnStateManager manager;

    protected PawnBaseState(PawnStateManager manager)
    {
        this.manager = manager;
    }
    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void ExitState();
    public abstract void CheckSwitchState();

}
