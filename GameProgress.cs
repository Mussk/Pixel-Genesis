using System;

public abstract class GameProgress
{
    public static event Action OnGameProgress;

    public static void TriggerOnGameProgress()
    {
        OnGameProgress?.Invoke();
    }

}
