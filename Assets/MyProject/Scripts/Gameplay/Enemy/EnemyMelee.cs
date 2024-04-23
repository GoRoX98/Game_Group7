using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Attacker))]
public class EnemyMelee : Enemy, IMoveble
{
    [SerializeField] private float _maxSpeed = 5f;
    [SerializeField] private float _speedIncrase = 1f;

    [Space]
    [SerializeField] private Animator _animator;
    [SerializeField] private NavMeshAgent _agent;
    private Attacker _attacker;

    private StateMachine _stateMachine;

    public float Speed => _agent.speed;

    private void Start()
    {
        _attacker = _agent.GetComponent<Attacker>();
        
        _stateMachine = new StateMachine();
        _stateMachine.AddState(new IdleState(_stateMachine, _animator));
        _stateMachine.AddState(new MoveState(_stateMachine, _animator, this));
        _stateMachine.AddState(new PersecutionState(_stateMachine, _animator, _attacker, GameManager.PlayerTransform, _agent, this));
        _stateMachine.AddState(new AttackState(_stateMachine, _animator, GameManager.PlayerTransform.GetComponent<Player>(), 
                                                GameManager.PlayerTransform, _attacker));
        _stateMachine.SetState<IdleState>();

        _currentHealth = MaxHealth;
    }

    private void Update()
    {
        if (!_agent.hasPath)
        {
            _stateMachine.SetState<IdleState>();
            _agent.SetDestination(TakeNewPath());
        }
        else if (_agent.hasPath && _stateMachine.CurrentState is IdleState)
            _stateMachine.SetState<MoveState>();

        if (_stateMachine.CurrentState is not PersecutionState && _stateMachine.CurrentState is not AttackState)
        {
            float distance = Vector3.Distance(GameManager.PlayerPos, transform.position);
            if (distance <= _visionRadius && distance > _attacker.AttackRadius)
                _stateMachine.SetState<PersecutionState>();
            else if (distance <= _attacker.AttackRadius)
                _stateMachine.SetState<AttackState>();
        }

        _agent.speed = _animator.GetFloat("Speed");

        _stateMachine.Update();
    }

    private Vector3 TakeNewPath()
    {
        NavMeshTriangulation data = NavMesh.CalculateTriangulation();
        int index = Random.Range(0, data.vertices.Length);
        return data.vertices[index];
    }
}
