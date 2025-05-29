using UnityEngine;

namespace Enemy.States
{
    public class ChaseState : EnemyState
    {
        public ChaseState(EnemyController enemy, EnemyStateMachine stateMachine) : base(enemy, stateMachine) { }

        public override void Update()
        {
            Enemy.transform.position = Vector2.MoveTowards(Enemy.transform.position,
                Enemy.Player.transform.position, Enemy.ChaseSpeed * Time.deltaTime);

            float dist = Vector2.Distance(Enemy.transform.position, Enemy.Player.transform.position);

            Vector2 direction = (Enemy.Player.transform.position - Enemy.transform.position).normalized;

            AnimateTribeEnemyMove(direction.x, direction.y);

            if (dist < Enemy.AttackRange)
                StateMachine.ChangeState(Enemy.AttackState);
            else if (dist > Enemy.ChaseRange)
                StateMachine.ChangeState(Enemy.IdleState);
        }

        private void AnimateTribeEnemyMove(float animMoveX, float animMoveY)
        {
            if (Enemy is not EnemyTribeController enemyTribeController) return;

            enemyTribeController.AnimateWalk(animMoveX, animMoveY);
        }
    }
}
