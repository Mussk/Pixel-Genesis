using UnityEngine;

namespace Enemy.States
{
    public class PatrolState : EnemyState
    {   
        private Vector2 _patrolTarget;

        public PatrolState(EnemyController enemy, EnemyStateMachine stateMachine) : base(enemy, stateMachine) { }

        public override void Enter()
        {
            _patrolTarget = new Vector2(Random.Range(-Enemy.PatrolAreaConstraints.x, Enemy.PatrolAreaConstraints.x),
                Random.Range(-Enemy.PatrolAreaConstraints.y, Enemy.PatrolAreaConstraints.y));
       
        }
        public override void Update()
        {
            Enemy.transform.position = Vector2.MoveTowards(Enemy.transform.position, _patrolTarget, (Enemy.MoveSpeed + Random.Range(-Enemy.MoveSpeedOffset, Enemy.MoveSpeedOffset)) * Time.deltaTime);

            if (Vector2.Distance(Enemy.transform.position, _patrolTarget) < 0.1f)
                StateMachine.ChangeState(Enemy.IdleState);
            else if (Vector2.Distance(Enemy.transform.position, Enemy.Player.transform.position) < Enemy.ChaseRange)
                StateMachine.ChangeState(Enemy.ChaseState);
        }
    }
}