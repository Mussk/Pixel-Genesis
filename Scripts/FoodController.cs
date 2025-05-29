using UnityEngine;
public class FoodController : MonoBehaviour
{
    [SerializeField]   
    protected int healAmount = 15;
    public int HealAmount => healAmount;
    [SerializeField]   
    private string progressBarTag;

    [SerializeField]
    protected int progressAmount = 50;

    protected ProgressBar ProgressBar;


    protected void Awake()
    {
        if(progressBarTag != "")
            ProgressBar = GameObject.FindWithTag(progressBarTag).GetComponent<ProgressBar>();
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.isTrigger) return;

        PlayerController playerController = other.GetComponent<PlayerController>();

        AudioManager.PlaySound(AudioManager.AudioLibrary.MiscSounds.FoodPickUp);

        if (playerController is null || playerController.IsDead) return;
        playerController.Health.Heal(healAmount);
        playerController.gameObject.transform.localScale +=
            Vector3.one * playerController.SizeIncrease;

        ProgressBar?.IncreaseProgress(progressAmount);

        Destroy(gameObject);
    }
}
