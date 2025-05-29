using UnityEngine;

public class FoodBirdController : FoodController
{

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.isTrigger) return;

        PlayerController playerController = other.GetComponent<PlayerController>();

        AudioManager.PlaySound(AudioManager.AudioLibrary.MiscSounds.FoodPickUp);

        if (playerController is null || playerController.IsDead) return;
        playerController.Health.Heal(healAmount);

        ProgressBar?.IncreaseProgress(progressAmount);

        gameObject.SetActive(false);
    }
}
