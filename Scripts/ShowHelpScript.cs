using UnityEngine;
using Cysharp.Threading.Tasks;
public class ShowHelpScript : MonoBehaviour
{
    [SerializeField]
    private GameObject helpUIWindow;

    [SerializeField]
    private int milSecToShow;

    private void Awake()
    {
        helpUIWindow.SetActive(true);
    }


    private void OnEnable()
    {
        ShowHelpUIWindow(milSecToShow).Forget();
    }

    private async UniTaskVoid ShowHelpUIWindow(int delay)
    {   
        helpUIWindow.SetActive(true);
        await UniTask.Delay(delay);
        helpUIWindow.SetActive(false);
    }
}
