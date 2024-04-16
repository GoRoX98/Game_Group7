using UnityEngine;
using UnityEngine.AI;

public class EnemyMelee : Enemy
{
    [SerializeField] private float _maxSpeed = 5f;
    [SerializeField] private float _speedIncrase = 1f;

    [Space]
    [SerializeField] private Animator _animator;
    [SerializeField] private NavMeshAgent _agent;

    private float _currentSpeed = 0f;

    private void Awake()
    {
        _health = _maxHealth;
    }

    private void Update()
    {
        if (_agent.hasPath && _currentSpeed < _maxSpeed)
        {
            _currentSpeed += _speedIncrase * Time.deltaTime;
        }

        if (!_agent.hasPath)
        {
            _currentSpeed = 0f;
            _agent.SetDestination(TakeNewPath());
        }

        _agent.speed = _currentSpeed;
        _animator.SetFloat("Speed", _currentSpeed);
    }

    private Vector3 TakeNewPath()
    {
        NavMeshTriangulation data = NavMesh.CalculateTriangulation();
        int index = Random.Range(0, data.vertices.Length);
        return data.vertices[index];
    }
}
