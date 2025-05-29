using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Playables;


// This script for cutscenes on separate scenes (where is only cutscene and no gameplay)
public class OnlyCutsceneController : CutsceneController
{
    
    protected override void Awake()
    {
        PlayableDirector = GetComponent<PlayableDirector>();
        ExecuteCutscene();
    }

    protected override void OnEnable()
    {
       
    }

    protected override void OnDisable()
    {
        
    }

    protected override async void ExecuteCutscene()
    {
        try
        {
            PlayableDirector.Play();
            while (PlayableDirector.state == PlayState.Playing)
            {
                await Task.Yield();
            }

            fillerFrame?.SetActive(true);

            MoveToNextScene();
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }
}
