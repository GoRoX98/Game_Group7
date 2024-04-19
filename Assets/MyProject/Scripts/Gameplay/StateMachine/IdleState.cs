using UnityEngine;

public class IdleState : StateSM
{

    public IdleState(StateMachine machine, Animator animator) : base(machine, animator) 
    { 
        
    }


    public override void Enter()
    {
        Debug.Log("[ENTER] Idle");
        _animator.SetFloat("Speed", 0);
    }

    public override void Exit()
    {
        Debug.Log("[EXIT] Idle");
    }

    public override void Update()
    {
        Debug.Log("[UPDATE] Idle");
    }
}
