using UnityEngine;

namespace Enemy.States
{
    public class AttackState : EnemyState
    {
    
        private float _cooldownTimer;

        public AttackState(EnemyController enemy, EnemyStateMachine stateMachine) : base(enemy, stateMachine) { }

        public override void Enter()
        {
            _cooldownTimer = 0f;
        }

        public override void Update()
        {
            float dist = Vector2.Distance(Enemy.transform.position, Enemy.Player.transform.position);

            if (dist > Enemy.AttackRange)
            {
                StateMachine.ChangeState(Enemy.ChaseState);
                return;
            }

            _cooldownTimer -= Time.deltaTime;

            if (!(_cooldownTimer <= 0f)) return;
        
            AttackPlayer();
            AnimateTribeEnemyAttack();
            _cooldownTimer = Enemy.AttackCooldown;


        }

        private void AttackPlayer()
        {
            if (!Enemy.CanMove) return;
        
            Debug.Log("Enemy attacks the player!");
            Enemy.Player.gameObject.GetComponent<PlayerController>().
                Health.TakeDamage(Enemy.DamageAmount);
        }

        private void AnimateTribeEnemyAttack()
        {
            if (Enemy is EnemyTribeController enemyTribeController)
            {
                enemyTribeController.AnimateAttack();
            }
        }

    }
}
