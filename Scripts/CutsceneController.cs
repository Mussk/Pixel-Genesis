using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;


// This sript for in-game cutscenes
public class CutsceneController : MonoBehaviour
{
    [SerializeField]
    protected GameObject cutsceneObject;
    [SerializeField]
    protected GameObject fillerFrame;

    [SerializeField]
    protected bool moveToPlayerPos;

    [SerializeField]
    protected string playerTag;

    protected PlayableDirector PlayableDirector;

    private PlayerController _playerController;

    protected virtual void Awake()
    {
        _playerController = GameObject.FindWithTag(playerTag).GetComponent<PlayerController>();
        PlayableDirector = GetComponent<PlayableDirector>();
        cutsceneObject.SetActive(false);
    }

    protected virtual void OnEnable()
    {
        GameProgress.OnGameProgress += ExecuteCutscene;
        
    }

    protected virtual void OnDisable()
    {
        GameProgress.OnGameProgress -= ExecuteCutscene;
    }

    protected virtual async void ExecuteCutscene()
    {
        try
        {
            PlayOnAwakeSounds.StopAllBgSoundsOnScene();

            if (moveToPlayerPos)
                cutsceneObject.transform.position = _playerController.gameObject.transform.position;

            cutsceneObject.SetActive(true);

            PlayableDirector.Play();

            Time.timeScale = 0;
            _playerController.InputActions.Player.Disable();
            while (PlayableDirector.state == PlayState.Playing)
            {
                await Task.Yield();
            }
            _playerController.InputActions.Player.Enable();
            Time.timeScale = 1;
            fillerFrame.SetActive(true);
        
            MoveToNextScene();
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }
    
    protected static void MoveToNextScene()
    {
        int nextSceneBuildIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextSceneBuildIndex == SceneManager.sceneCountInBuildSettings ? 0 : nextSceneBuildIndex);
    }
}
