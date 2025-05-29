using System;
using UnityEngine;


public class GameEnd : MonoBehaviour
{
    [SerializeField]
    private string gameEndScreenTag;

    private GameObject _gameEndScreen;

    private static event Action OnGameEnd;

    private void OnEnable()
    {
        OnGameEnd += ShowGameEndScreen;
        _gameEndScreen = GameObject.FindWithTag(gameEndScreenTag);
        Debug.Log(_gameEndScreen);

        _gameEndScreen.SetActive(false);

    }

    private void OnDisable()
    {
        OnGameEnd -= ShowGameEndScreen;
    }

    private void ShowGameEndScreen()
    {
        Time.timeScale = 0;
        _gameEndScreen.SetActive(true);

    }

    public static void TriggerOnGameEnd()
    {
        OnGameEnd?.Invoke();
    }
}
