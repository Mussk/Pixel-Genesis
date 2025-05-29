using System;
using UnityEngine;

public class BirdEndCutsceneController : CutsceneController
{
    [Header("BirdEndCutsceneController")]
    [SerializeField]
    private PlayerAnimationController playerAnimController;
    [SerializeField]
    private SolidFadeController solidFadeController;

    protected override void Awake()
    {
        
    }
    
    protected override async void ExecuteCutscene()
    {
        try
        {
            Debug.Log("ExecuteCutscene");
            playerAnimController.DisablePlayerColliders();
            AudioManager.PlaySound(AudioManager.AudioLibrary.MiscSounds.WinSound);
            await playerAnimController.PlayLerpAnimation(playerAnimController.OffsetXEndAnim);
            playerAnimController.gameObject.SetActive(false);
            solidFadeController.PlayFadeInAnimation();

            MoveToNextScene();
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    
}
