using UnityEngine;
using UnityEngine.Serialization;

public class ObstacleController : MonoBehaviour
{

    [SerializeField]
    private string playerTag;

    [FormerlySerializedAs("damageAmout")] [SerializeField]
    private int damageAmount;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag(playerTag)) return;
        AudioManager.PlaySound(AudioManager.AudioLibrary.BirdSceneSounds.DamageSound);
        collision.gameObject.GetComponent<BirdPlayerController>()
            .Health.TakeDamage(damageAmount);
    }
}
