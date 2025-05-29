using System;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public Slider progressBarSlider;

    [SerializeField]
    private int maxProgress = 100;

    private void Start()
    {

        SetMaxProgress(maxProgress);

    }


    public void SetMaxProgress(int maxProgress)
    {
        progressBarSlider.maxValue = maxProgress;
        progressBarSlider.value = 0;
    }

    public void IncreaseProgress(int progressAmount)
    {
        progressBarSlider.value += progressAmount;

        CheckIfProgressReachMax();
    }


    private void CheckIfProgressReachMax()
    {
        if (progressBarSlider.value >= maxProgress)
            GameProgress.TriggerOnGameProgress();
    }
 
}
