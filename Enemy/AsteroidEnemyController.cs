using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Enemy
{
    public enum AsteroidType
    {
        Hostile,
        Friendly
    }

    [RequireComponent(typeof(CircleCollider2D))]
    public class AsteroidEnemyController : MonoBehaviour
    {
        [SerializeField] private AsteroidType asteroidType;
        [SerializeField] private int delayBeforeDestruction = 100;
        [SerializeField] private string playerTag = "Player";
        [SerializeField] private int healAmount = 10;
        [SerializeField] private int damageAmount = 10;
        [SerializeField] private string progressBarTag;
        private ProgressBar _progressBar;
        [SerializeField] private int progressAmount = 10;

        [Header("Border Hit Settings")]
        [SerializeField] private float outerRadius = 1.0f; // Visible radius (outer)
        [SerializeField] private float innerDeadZone = 0.5f; // Inner "safe" zone radius

        private bool _isTouched;
        private Vector3 _initialPos;
        public Vector3 InitialPos => _initialPos;

        private void Awake()
        {
            _initialPos = transform.localPosition;

            if (progressBarTag != "")
                _progressBar = GameObject.FindWithTag(progressBarTag).GetComponent<ProgressBar>();

            // Set the collider size to outer radius
            var col = GetComponent<CircleCollider2D>();
            col.isTrigger = true;
            col.radius = outerRadius;
        }

        private void OnEnable()
        {
            _isTouched = false;
        
        }

        private async void OnTriggerEnter2D(Collider2D collision)
        {
            try
            {
                if (!collision.CompareTag(playerTag))
                    return;

                if (!collision.isTrigger)
                    return;

                if (!_isTouched)
                    _isTouched = true;
                else return;

                // Check distance between player and asteroid center
                float distance = Vector2.Distance(collision.transform.position, transform.position);

                if (distance < innerDeadZone)
                {
                    // Player touched inside the center — ignore
                    return;
                }


                await UniTask.Delay(delayBeforeDestruction);

                var player = collision.GetComponent<PlayerController>();
                if (player is null) return;

                if (asteroidType == AsteroidType.Friendly) 
                {
                    AudioManager.PlaySound(AudioManager.AudioLibrary.SpaceSceneSounds.AsteroidBrakeSound);
                    player.Health.Heal(healAmount);
                
                    Debug.Log("Trigger friendly asteroid");
                    _progressBar.IncreaseProgress(progressAmount);
               
                }   
                else 
                {
                    AudioManager.PlaySound(AudioManager.AudioLibrary.SpaceSceneSounds.PlayerDamageSound);
                    player.Health.TakeDamage(damageAmount);
                }
            
                gameObject.SetActive(false);
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
            }
        }

        // Optional: Draw gizmos to visualize the "ring"
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, outerRadius);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, innerDeadZone);
        }
    
    }
}