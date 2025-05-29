namespace Enemy
{
    public abstract class EnemyState
    {
        protected readonly EnemyController Enemy;
        protected readonly EnemyStateMachine StateMachine;

        protected EnemyState(EnemyController enemy, EnemyStateMachine stateMachine)
        {
            Enemy = enemy;
            StateMachine = stateMachine;
        }

        public virtual void Enter() { }
        public virtual void Update() { }
        public virtual void Exit() { }
    }
}
