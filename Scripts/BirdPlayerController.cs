using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BirdPlayerController : PlayerController
{
    [SerializeField]
    protected float verticalForce;

    public override void UpdateMoveInput()
    {
        base.UpdateMoveInput();
        if (MoveInput.y < 0f) MoveInput.y = 0f;

        if (MoveInput.y > 0f) Rb.AddForceY(verticalForce);
    }

    protected override void Update()
    {   
        base.Update();
        if (!canMove)
            Rb.linearVelocity = Vector2.zero;
    }

    //this fuction triggers by animation
    public void PlaySoundOnFlyOneAnimation(int index)
    {
        switch (index)
        {
            case 1:
                AudioManager.PlaySound(AudioManager.AudioLibrary.BirdSceneSounds.FlyOne);
                break;
            case 2:
                AudioManager.PlaySound(AudioManager.AudioLibrary.BirdSceneSounds.FlyTwo);
                break;
        }
    }

   
}
