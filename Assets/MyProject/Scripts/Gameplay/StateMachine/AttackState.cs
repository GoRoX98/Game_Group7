using UnityEngine;
using UnityEngine.AI;

public class AttackState : StateSM
{
    private Transform _attackerTransform;
    private Transform _targetTransform;
    private bool CanAttack => Vector3.Distance(_attackerTransform.position, _targetTransform.position) <= _attacker.AttackRadius;
    private IDamageble _target;
    private Attacker _attacker;

    public AttackState(StateMachine machine, Animator animator, IDamageble target, Transform targetTransform, Attacker attacker) : base(machine, animator)
    {
        _target = target;
        _attacker = attacker;
        _attackerTransform = attacker.GetComponent<Transform>();
        _targetTransform = targetTransform;
    }

    public override void Enter()
    {
        Debug.Log("[ENTER] Attack State");
    }

    public override void Exit()
    {
        Debug.Log("[EXIT] Attack State");
    }

    public override void Update()
    {
        Debug.Log("[UPDATE] Attack State");
        
        if (_target.Health <= 0)
            _machine.SetState<IdleState>();
        
        if (CanAttack)
            _attacker.Attack();
        else
            _machine.SetState<PersecutionState>();
    }
}
