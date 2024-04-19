
using UnityEngine;

public class MoveState : StateSM
{
    private IMoveble _move;
    private float _speed;

    public MoveState(StateMachine machine, Animator animator, IMoveble move) : base(machine, animator)
    {
        _move = move;
    }

    public override void Enter()
    {
        Debug.Log("[ENTER] MoveState");
    }

    public override void Exit()
    {
        Debug.Log("[EXIT] MoveState");
    }

    public override void Update()
    {
        if (_speed < _move.MaxSpeed)
            _speed += _move.SpeedIncrase * Time.deltaTime;

        _animator.SetFloat("Speed", _speed);
    }
}

