﻿using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Code.Infrastructure.States
{
  [UsedImplicitly]
  public class GameStateMachine : IGameStateMachine
  {
    private readonly Dictionary<Type, IExitableState> _states = new Dictionary<Type, IExitableState>();
    private IExitableState _activeState;

    public void RegisterState<TState>(TState state) where TState : class, IExitableState =>
      _states.Add(typeof(TState), state);

    public void Enter<TState>() where TState : class, IState
    {
      IState state = ChangeState<TState>();
      state.Enter();
    }

    public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
    {
      TState state = ChangeState<TState>();
      state.Enter(payload);
    }

    private TState ChangeState<TState>() where TState : class, IExitableState
    {
      _activeState?.Exit();

      TState state = GetState<TState>();
      _activeState = state;

      return state;
    }

    private TState GetState<TState>() where TState : class, IExitableState =>
      _states[typeof(TState)] as TState;
  }
}