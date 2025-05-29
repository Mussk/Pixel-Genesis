using UnityEngine;

namespace Enemy
{
    public class EnemyStateMachine
    {
        private EnemyState CurrentState { get; set; }
        
        public Animator Animator { get; set; }
        
        public void Initialize(EnemyState startState)
        {
            CurrentState = startState;
            CurrentState.Enter();
        }

        public void ChangeState(EnemyState newState)
        {
            CurrentState.Exit();
            CurrentState = newState;
            CurrentState.Enter();
        }

        public void Update()
        {
            CurrentState?.Update();
        }
    }
}
