using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct MainMenuSounds
{
    public AudioSource BackgroundMusic;
}

[System.Serializable]
public struct SporeSceneSounds
{
    public AudioSource BackgroundMusic;
    public AudioSource DamageSound;
    
}

[System.Serializable]
public struct BirdSceneCutsceneSounds
{
    public AudioSource BackgroundMusic;
}

[System.Serializable]
public struct BirdSceneSounds
{
    public AudioSource BackgroundMusic;
    public AudioSource DamageSound;
    public AudioSource FlyOne;
    public AudioSource FlyTwo;
    
}

[System.Serializable]
public struct BirdTribemanSceneCutsceneSounds
{
    
}

[System.Serializable]
public struct TribeSceneSounds
{
    public AudioSource BackgroundMusic;
    public AudioSource PlayerAttack;
    public AudioSource PlayerHit;

    public AudioSource SkeletonAttack;
    public AudioSource SkeletonHit;
    public AudioSource SkeletonDeath;

    public AudioSource BatAttack;
    public AudioSource BatHit;
    public AudioSource BatDeath;
}

[System.Serializable]
public struct TribeSpaceCutscene
{
    public AudioSource BackgroundMusic;
    public AudioSource RocketStartSound;
}

[System.Serializable]
public struct SpaceSceneSounds
{
    public AudioSource BackgroundMusic;
    public AudioSource PlayerMoveSound;
    public AudioSource PlayerDamageSound;
    public AudioSource AsteroidBrakeSound;
}

[System.Serializable]
public struct SpaceSceneCutsceneSounds
{
    public AudioSource BackgroundMusic;
}

[System.Serializable]
public struct UISounds
{
    public AudioSource UIClickSound;
}

[System.Serializable]
public struct MiscSounds
{
    public AudioSource FoodPickUp;
    public AudioSource LooseGameSound;
    public AudioSource WinSound;
}

[System.Serializable]
public struct AudioLibrary 
{
    public MainMenuSounds MainMenuSounds;
    public SporeSceneSounds SporeSceneSounds;
    public BirdSceneCutsceneSounds BirdSceneCutsceneSounds;
    public BirdSceneSounds BirdSceneSounds;
    public BirdTribemanSceneCutsceneSounds BirdTribemanSceneCutsceneSounds;
    public TribeSceneSounds TribeSceneSounds;
    public TribeSpaceCutscene TribeSpaceCutscene;
    public SpaceSceneSounds SpaceSceneSounds;
    public SpaceSceneCutsceneSounds SpaceSceneCutsceneSounds;
    public UISounds UISounds;
    public MiscSounds MiscSounds;
}

public class AudioManager : MonoBehaviour
{

    public static AudioLibrary AudioLibrary;

    [SerializeField]
    private AudioLibrary audioLibraryInstance;


    private void Awake()
    {   
           
        AudioLibrary = audioLibraryInstance;
       
    }

    public static void PlaySound(AudioSource soundToPlay) 
    { 
    
        soundToPlay.Play();
       
    }

    public static void StopSound(AudioSource soundToPlay)
    {

        soundToPlay.Stop();

    }
}
