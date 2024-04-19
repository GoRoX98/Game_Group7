using UnityEngine;
using UnityEngine.AI;

public class PersecutionState : StateSM
{
    private Transform _target;
    private NavMeshAgent _agent;
    private Attacker _attacker;
    private IMoveble _move;

    private float _speed;

    public PersecutionState(StateMachine machine, Animator animator, Attacker attacker, Transform target, 
                            NavMeshAgent agent, IMoveble moveble) : base(machine, animator)
    {
        _attacker = attacker;
        _target = target;
        _agent = agent;
        _move = moveble;
        _speed = _animator.GetFloat("Speed");
    }

    public override void Enter()
    {
        _agent.stoppingDistance = _attacker.AttackRadius / 2;
        Debug.Log("[ENTER] Attack State");
    }

    public override void Exit()
    {
        _agent.stoppingDistance = 0;
        Debug.Log("[Exit] Attack State");
    }

    public override void Update()
    {
        Debug.Log("[Update] Attack State");

        _agent.SetDestination(_target.position);

        if (_speed < _move.MaxSpeed)
            _speed += _move.SpeedIncrase * Time.deltaTime;

        _animator.SetFloat("Speed", _speed);

        if (_agent.isStopped)
            _machine.SetState<AttackState>();
    }
}
