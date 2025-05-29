using System;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Threading.Tasks;
using Enemy;
using UnityEngine;

[Serializable]
public struct Stage
{
    public int amountEnemies;
    public Transform stageMark;
    public GameObject enemiesOfStage;
}

public class TribeStageManager : MonoBehaviour
{
    [SerializeField]
    private List<Stage> stages;

    [SerializeField]
    private TribeCameraController cameraController; 

    private int _currentStage;

    private int _enemiesDiedOnCurrentStage;

    [SerializeField]
    private int cameraOffset;

    [SerializeField]
    private GameObject goSignUI;

    [SerializeField]
    private int showTimeMilSec;

    private async void Start()
    {
        try
        {
            _currentStage = 0;
            _enemiesDiedOnCurrentStage = 0;
            cameraController.CurrentZoneConstraint = stages[_currentStage].stageMark;
            stages.ForEach(stage => stage.enemiesOfStage.gameObject.SetActive(false));
            await Task.Delay(5000);
            stages[_currentStage].enemiesOfStage.gameObject.SetActive(true);
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    private void Update()
    {
        CheckEnemiesSpawn();
    }

    private void OnEnable()
    {
        EnemyTribeController.OnEnemyDeath += CheckForNextStage;
    }

    private void OnDisable()
    {
        EnemyTribeController.OnEnemyDeath -= CheckForNextStage;
    }

    private void CheckForNextStage()
    {
        _enemiesDiedOnCurrentStage++;
        Debug.Log(_enemiesDiedOnCurrentStage);
        if (_enemiesDiedOnCurrentStage == stages[_currentStage].amountEnemies
            && _currentStage < stages.Count - 1)
        {
            ShowGoSignUI();
            _currentStage++;
            _enemiesDiedOnCurrentStage = 0;
            cameraController.CanMoveToNextZone = true;
            cameraController.CurrentZoneConstraint = stages[_currentStage].stageMark;
            
        }

        CheckStageEnd();
    }

    private void CheckEnemiesSpawn()
    {
        if (stages[_currentStage].stageMark.transform.position.x <= 
            cameraController.gameObject.transform.position.x + cameraOffset)
        {
            stages[_currentStage].enemiesOfStage.gameObject.SetActive(true);
        }
    }

    private async void CheckStageEnd()
    {
        try
        {
            if (_currentStage != stages.Count - 1 ||
                _enemiesDiedOnCurrentStage != stages[_currentStage].amountEnemies) return;
            await UniTask.Delay(2000);
            GameProgress.TriggerOnGameProgress();
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    private async void ShowGoSignUI()
    {
        try
        {
            goSignUI.SetActive(true);
            await UniTask.Delay(showTimeMilSec);
            goSignUI.SetActive(false);
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }
}
