using UnityEngine;
using UnityEngine.AI;

public class PersecutionState : StateSM
{
    private Transform _target;
    private NavMeshAgent _agent;
    private Attacker _attacker;
    private IMoveble _moveble;

    private float _speed;
    //¬ременна€ настройка, до какого отдалени€ происходит преследование
    private float _persDistance = 15f;
    private bool PersZone => Vector3.Distance(_target.position, _agent.nextPosition) < _persDistance;
    //—колько времени после выхода из зоны преследовани€ враг пытаетс€ догнать игрока
    private float _persTime = 1f;
    private float _timer = 0f;

    public PersecutionState(StateMachine machine, Animator animator, Attacker attacker, Transform target, 
                            NavMeshAgent agent, IMoveble moveble) : base(machine, animator)
    {
        _attacker = attacker;
        _target = target;
        _agent = agent;
        _moveble = moveble;
        _speed = _animator.GetFloat("Speed");
    }

    public override void Enter()
    {
        _agent.stoppingDistance = _attacker.AttackRadius / 2;
        Debug.Log("[ENTER] Persucution State");
    }

    public override void Exit()
    {
        _agent.stoppingDistance = 0;
        Debug.Log("[Exit] Persucution State");
    }

    public override void Update()
    {
        Debug.Log("[Update] Persucution State");

        _agent.SetDestination(_target.position);

        if (_speed < _moveble.MaxSpeed)
            _speed += _moveble.SpeedIncrase * Time.deltaTime;


        //ѕровер€ем что противник остановилс€ и дистанци€ подходит дл€ атаки
        if (Vector3.Distance(_target.position, _agent.nextPosition) < _attacker.AttackRadius)
        {
            _speed = 0;
            _machine.SetState<AttackState>();
        }

        //ѕроверка вышел ли игрок за зону преследовани€ и в случае истечени€ таймера перейти в состо€ние отдыха
        if (!PersZone && _timer >= _persTime)
            _machine.SetState<IdleState>();
        else if (!PersZone)
            _timer += Time.deltaTime;
        else
            _timer = 0;

        _animator.SetFloat("Speed", _speed);
    }
}
