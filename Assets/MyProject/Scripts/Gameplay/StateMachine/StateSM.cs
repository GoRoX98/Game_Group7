using UnityEngine;

public abstract class StateSM
{
    protected readonly StateMachine _machine;
    protected Animator _animator;

    public StateSM(StateMachine machine)
    {
        _machine = machine;
    }
    public StateSM(StateMachine machine, Animator animator)
    {
        _machine = machine;
        _animator = animator;
    }

    public abstract void Enter();
    public abstract void Exit();
    public abstract void Update();
}
