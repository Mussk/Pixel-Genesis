using UnityEngine;

namespace Enemy.States
{
    public class IdleState : EnemyState
    {
    
        private float _idleTimer;

        public IdleState(EnemyController enemy, EnemyStateMachine stateMachine) : base(enemy, stateMachine) { }

        public override void Enter()
        {
            _idleTimer = Enemy.IdleDuration;
        }

        public override void Update()
        {
            _idleTimer -= Time.deltaTime;

            if (Vector2.Distance(Enemy.transform.position, Enemy.Player.transform.position) < Enemy.ChaseRange)
                StateMachine.ChangeState(Enemy.ChaseState);
            else if (_idleTimer <= 0)
                StateMachine.ChangeState(Enemy.PatrolState);
       
        }
    }
}