using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Enemy
{
    public class EnemyTribeController : EnemyController
    {
        private static readonly int Move = Animator.StringToHash("CanMove");
        private static readonly int AnimMoveX = Animator.StringToHash("AnimMoveX");
        private static readonly int AnimMoveY = Animator.StringToHash("AnimMoveY");
        private static readonly int IsAttacking = Animator.StringToHash("IsAttacking");
        private static readonly int IsTakingHit = Animator.StringToHash("IsTakingHit");
        private static readonly int IsDead = Animator.StringToHash("IsDead");

        [Header("Health")]
        [SerializeField]
        private int currentHealth;

        private Health HealhSystem { get; set; }

        public static event Action OnEnemyDeath;

        [Header("Animation")]
        [SerializeField]
        private Animator animator;
        [SerializeField]
        private SpriteRenderer spriteRenderer;
        [SerializeField]
        private string attackColliderTag;
        [SerializeField]
        private int deathDelay;
    

        private Vector3 _cachedPos;

        protected override void Awake()
        {
            base.Awake();

            StateMachine.Animator = animator;

            HealhSystem = new Health(currentHealth, null);

            animator.SetBool(Move, true);
        }

        protected override void Start()
        {

            StateMachine.Initialize(ChaseState);
        }

        protected override void Update()
        {
            base.Update();

            if(!canMove)
                transform.position = _cachedPos;
        }

        private void OnEnable()
        {
            HealhSystem.OnDeath += TriggerDeath;
            HealhSystem.OnTakingDamage += PlayTakingDamageAnimation;
        
        }

        private void OnDisable()
        {
            HealhSystem.OnDeath -= TriggerDeath;
            HealhSystem.OnTakingDamage -= PlayTakingDamageAnimation;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.gameObject.CompareTag(attackColliderTag)) return;
            
            TribePlayerController playerController = 
                Player.GetComponent<TribePlayerController>();
            HealhSystem.TakeDamage(playerController.DamageAmount);
        }

        protected override void OnCollisionEnter2D(Collision2D collision)
        {
       
        }

        private async void TriggerDeath()
        {
            try
            {
                idleDuration = 10;
                StateMachine.ChangeState(IdleState);
                HealhSystem.IsInvurable = true;
                OnEnemyDeath?.Invoke();
                canMove = false;
                PlayDeathAnimation();
                DisableColliders();
                await Task.Delay(deathDelay);
                Destroy(gameObject);
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
            }
        }

        public void AnimateWalk(float animMoveX, float animMoveY)
        {
            animator.SetFloat(AnimMoveX, animMoveX);
            animator.SetFloat(AnimMoveY, animMoveY);

            if(canMove)
            {
                spriteRenderer.flipX = animMoveX < 0;
            }
        }

        public async void AnimateAttack()
        {
            try
            {
                if (animator is null || !canMove) return;
                animator.SetBool(IsAttacking, true);
                await Task.Delay((int)attackCooldown * 1000);
                if(canMove)
                    animator.SetBool(IsAttacking, false);
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
            }
        }

        public void ResetAttackAnimation()
        {
            animator.SetBool(IsAttacking, false);
        }

        private void PlayTakingDamageAnimation()
        {
            animator.SetBool(IsTakingHit, true);
        }

        private void PlayDeathAnimation()
        {
            animator.SetBool(Move, false);
            animator.SetBool(IsDead, true);
            animator.SetBool(IsAttacking, false);
            animator.SetBool(IsTakingHit, false);
            _cachedPos = transform.position;

        }

        private void DisableColliders()
        {
            Collider2D[] colliders = gameObject.GetComponents<Collider2D>();

            foreach (Collider2D collider1 in colliders) 
            { 
                collider1.enabled = false; 
            }
        }
    }
}
