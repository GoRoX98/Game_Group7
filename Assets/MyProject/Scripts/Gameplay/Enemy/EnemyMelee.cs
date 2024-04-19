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
    public float MaxSpeed => _maxSpeed;
    public float SpeedIncrase => _speedIncrase;

    private void Awake()
    {
        _attacker = _agent.GetComponent<Attacker>();
        
        _stateMachine = new StateMachine();
        _stateMachine.AddState(new IdleState(_stateMachine, _animator));
        _stateMachine.AddState(new MoveState(_stateMachine, _animator, this));
        _stateMachine.SetState<IdleState>();

        _currentHealth = _maxHealth;
    }

    private void Update()
    {
        if (!_agent.hasPath)
        {
            _stateMachine.SetState<IdleState>();
            _agent.SetDestination(TakeNewPath());
        }
        else if (_agent.hasPath)
            _stateMachine.SetState<MoveState>();

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
