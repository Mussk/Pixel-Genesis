using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayOnAwakeSounds : MonoBehaviour
{
    
    private void Awake()
    {
       switch (SceneManager.GetActiveScene().buildIndex)
       {
            //MainMenuScene
            case 0:
                AudioManager.PlaySound(AudioManager.AudioLibrary.MainMenuSounds.BackgroundMusic);
                break;
            //SporeScene
            case 1:
                AudioManager.PlaySound(AudioManager.AudioLibrary.SporeSceneSounds.BackgroundMusic);
                break;
            //BirdSceneCutscene
            case 2:
                break;
            //BirdScene
            case 3:
                AudioManager.PlaySound(AudioManager.AudioLibrary.BirdSceneSounds.BackgroundMusic);
                break;
            //BirdTribemanSceneCutscene
            case 4:
                break;
            //TribeScene
            case 5:
                AudioManager.PlaySound(AudioManager.AudioLibrary.TribeSceneSounds.BackgroundMusic);
                break;
            //TribeSpaceSceneCutscene
            case 6:
                break;
            //SpaceScene
            case 7:
                AudioManager.PlaySound(AudioManager.AudioLibrary.SpaceSceneSounds.BackgroundMusic);
                AudioManager.PlaySound(AudioManager.AudioLibrary.SpaceSceneSounds.PlayerMoveSound);
                break;
            //EndCutscene
            case 8:
                break;
       }
    }

    public static void StopAllBgSoundsOnScene()
    {
        switch (SceneManager.GetActiveScene().buildIndex)
        {
            //MainMenuScene
            case 0:
                AudioManager.StopSound(AudioManager.AudioLibrary.MainMenuSounds.BackgroundMusic);
                break;
            //SporeScene
            case 1:
                AudioManager.StopSound(AudioManager.AudioLibrary.SporeSceneSounds.BackgroundMusic);
                break;
            //BirdSceneCutscene
            case 2:
                break;
            //BirdScene
            case 3:
                AudioManager.StopSound(AudioManager.AudioLibrary.BirdSceneSounds.BackgroundMusic);
                break;
            //BirdTribemanSceneCutscene
            case 4:
                break;
            //TribeScene
            case 5:
                AudioManager.StopSound(AudioManager.AudioLibrary.TribeSceneSounds.BackgroundMusic);
                break;
            //TribeSpaceSceneCutscene
            case 6:
                break;
            //SpaceScene
            case 7:
                AudioManager.StopSound(AudioManager.AudioLibrary.SpaceSceneSounds.BackgroundMusic);
                AudioManager.StopSound(AudioManager.AudioLibrary.SpaceSceneSounds.PlayerMoveSound);
                break;
            //EndCutscene
            case 8:
                break;
        }
    }
}
