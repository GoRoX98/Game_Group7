using System;
using System.Collections.Generic;

public class StateMachine
{
    private StateSM _currentState;
    private Dictionary<Type, StateSM> _states = new Dictionary<Type, StateSM>();

    public StateSM CurrentState => _currentState;

    public void AddState(StateSM state)
    {
        _states.Add(state.GetType(), state);
    }
    public void AddState(List<StateSM> states)
    {
        foreach (var state in states)
        {
            _states.Add(state.GetType(), state);
        }
    }

    public void SetState<T>() where T : StateSM
    {
        var type = typeof(T);

        if (type == _currentState?.GetType())
            return;

        if (_states.TryGetValue(type, out StateSM newState))
        {
            _currentState?.Exit();

            _currentState = newState;

            _currentState.Enter();
        }
    }

    public void Update()
    {
        _currentState?.Update();
    }
}
