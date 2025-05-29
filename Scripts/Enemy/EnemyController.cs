using System.Collections;
using Enemy.States;
using UnityEngine;

namespace Enemy
{
    public class EnemyController : MonoBehaviour
    {
        [Header("States Configuration")]
        [Header("Idle")]
        [SerializeField]
        protected float idleDuration = 2f;
        public float IdleDuration => idleDuration;

        [Header("Attack")]
        [SerializeField]
        protected float attackCooldown = 1.5f;
        public float AttackCooldown => attackCooldown;

        [SerializeField]
        private float stunDuration;

        [SerializeField]
        private float knockbackStrength;

        [SerializeField]
        protected float attackRange = 1.5f;
        public float AttackRange => attackRange;

        [SerializeField]
        protected int damageAmount = 15;
        public int DamageAmount => damageAmount;

        [Header("Patrol")]
        [SerializeField]
        protected float moveSpeed = 5f;
        public float MoveSpeed => moveSpeed;

        [SerializeField]
        protected float moveSpeedOffset = 0.3f;
        public float MoveSpeedOffset => moveSpeedOffset;

        [SerializeField]
        [Tooltip("Draws a square within spawn takes place, reflected by zero coordinate." +
                 "Ex: 100,100 draws a square -100,-100,100,100")]
        protected Vector2 patrolAreaConstraints;
        public Vector2 PatrolAreaConstraints => patrolAreaConstraints;

        [Header("Chase")]
        [SerializeField]
        protected float chaseSpeed = 5f;
        public float ChaseSpeed => chaseSpeed;

        [SerializeField]
        protected float chaseRange = 5f;
        public float ChaseRange => chaseRange;

        public GameObject Player { get; private set; }

        [SerializeField]
        protected string playerTag;

        [SerializeField]
        protected bool canMove = true;
        public bool CanMove => canMove;

        public EnemyStateMachine StateMachine;

        public IdleState IdleState;
        public PatrolState PatrolState;
        public ChaseState ChaseState;
        public AttackState AttackState;

    

        protected virtual void Awake()
        {
            //rb = GetComponent<Rigidbody2D>();
            StateMachine = new EnemyStateMachine();

            Player = GameObject.FindGameObjectWithTag(playerTag);

            IdleState = new IdleState(this, StateMachine);
            PatrolState = new PatrolState(this, StateMachine);
            ChaseState = new ChaseState(this, StateMachine);
            AttackState = new AttackState(this, StateMachine);

        
        }

        protected virtual void Start()
        {

            StateMachine.Initialize(IdleState);
        }

        protected virtual void Update()
        {
            StateMachine.Update();

        }

        protected virtual void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.isTrigger)
                return;

            if (!collision.gameObject.CompareTag(playerTag)) return;
            
            Rigidbody2D rbOther = collision.rigidbody;
            Rigidbody2D rbSelf = GetComponent<Rigidbody2D>();

            if (rbOther is null || rbSelf is null) return;
            // Vector from self to other
            Vector2 direction = (collision.transform.position - transform.position).normalized;
            
            // Apply opposite forces
            rbSelf.AddForce(-direction * knockbackStrength, ForceMode2D.Impulse);
            StartCoroutine(ApplyKnockback(direction, knockbackStrength));
        }

        private IEnumerator ApplyKnockback(Vector2 dir, float force)
        {
            AudioManager.PlaySound(AudioManager.AudioLibrary.SporeSceneSounds.DamageSound);
            PlayerController playerController = Player.GetComponent<PlayerController>();
            Rigidbody2D playerRb = Player.GetComponent<Rigidbody2D>();

            playerController.CanMove = false;
            playerRb.linearVelocity = Vector2.zero;
            playerRb.AddForce(dir * force, ForceMode2D.Impulse);
            yield return new WaitForSeconds(stunDuration);
            if (playerController.IsDead)
            {
                playerRb.linearVelocity = Vector2.zero;
                playerRb.angularVelocity = 0f;
            }
            else
            {
                playerController.CanMove = true;
            }
            
        }

   
    }
}
