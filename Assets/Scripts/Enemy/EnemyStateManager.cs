using System.Collections.Generic;
using Enemy.States;
using UnityEngine;
using Zenject;

namespace Enemy
{
    public interface IEnemyState
    {
        void EnterState();
        void ExitState();
        void Update();
        void FixedUpdate();

        void LateUpdate();
    }
    
    public enum EnemyStates
    {
        Idle,
        Attack,
        Follow,
        None
    }
    
    public class EnemyStateManager : ITickable, IFixedTickable
    {
        IEnemyState _currentStateHandler;
        EnemyStates _currentState = EnemyStates.None;
        EnemyView _view;

        List<IEnemyState> _states;
        
        [Inject]
        public void Construct(
            EnemyView view,
            EnemyStateNone none, EnemyStateAttack attack, EnemyStateFollow follow)
        {
            _view = view;
            _states = new List<IEnemyState>
            {
                // This needs to follow the enum order
                none, attack, follow
            };
        }

        public void Tick()
        {
            _currentStateHandler.Update();
        }

        public void FixedTick()
        {
            _currentStateHandler.FixedUpdate();
        }

        public void ChangeState(EnemyStates state)
        {
            if (_currentState == state)
            {
                // Already in state
                return;
            }

            _currentState = state;

            if (_currentStateHandler != null)
            {
                _currentStateHandler.ExitState();
                _currentStateHandler = null;
            }

            _currentStateHandler = _states[(int)state];
            _currentStateHandler.EnterState();
        }
    }
}