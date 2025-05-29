using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class CreditsManager : MonoBehaviour
{
    [Header("Scroll Settings")]
    public float scrollSpeed = 50f;
    public RectTransform creditsText;

    private InputSystem_Actions _inputSystem;

    private void Awake()
    {   
        _inputSystem = new InputSystem_Actions();
        _inputSystem.Enable();
        _inputSystem.UI.Enable();

        _inputSystem.UI.Cancel.performed += ExitCredits;
    }
    
    private void Update()
    {
        creditsText.anchoredPosition += Vector2.up * (scrollSpeed * Time.deltaTime);

    }

    private void ExitCredits(CallbackContext callbackContext)
    {
        gameObject.SetActive(false);
    }
}