
using System;
using Cysharp.Threading.Tasks;
using UnityEditor.Scripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    protected float moveSpeed = 5f;

    [SerializeField]
    private float sizeIncrease;
    public float SizeIncrease 
    { get => sizeIncrease;
        private set => sizeIncrease = value;
    }

    [SerializeField]
    protected int maxHealth;
    [SerializeField]
    protected float invulnerabilityDuration;
    
    protected Rigidbody2D Rb;
    protected InputSystem_Actions inputActions;
    public InputSystem_Actions InputActions => inputActions;
    protected Vector2 MoveInput;

    public Health Health { get; protected set; }

    [SerializeField]
    protected HealthBar healthBar;

    [SerializeField]
    protected bool canMove;
    public bool CanMove { get => canMove;
        set => canMove = value;
    }

    protected bool isDead = false;
    public bool IsDead => isDead;
    
    [SerializeField]
    private float cameraEdgeMargin = 0.5f;

    protected Vector2 RawInput;
    
    [SerializeField]
    protected float shaderAnimDuration = 100f;

    protected virtual void Awake()
    {   
        CanMove = true;
        Rb = GetComponent<Rigidbody2D>();
        inputActions = new InputSystem_Actions();
        Effects.Effects.ResetFade();
        
        Health = new Health(maxHealth, healthBar);
    }

    protected virtual void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Move.performed += OnMovePerformed;
        inputActions.Player.Move.canceled += OnMoveCanceled;
        Health.OnTakingDamage += EnterInvulState;
        Health.OnDeath += Die;
        
    }

    protected virtual void OnDisable()
    {
        inputActions.Player.Disable();
        inputActions.Player.Move.performed -= OnMovePerformed;
        inputActions.Player.Move.canceled -= OnMoveCanceled;
        Health.OnTakingDamage -= EnterInvulState;
        Health.OnDeath -= Die;
    }

    protected virtual void OnMovePerformed(InputAction.CallbackContext context)
    {
        RawInput = context.ReadValue<Vector2>();
        UpdateMoveInput();
    }

    protected void OnMoveCanceled(InputAction.CallbackContext context)
    {
        RawInput = Vector2.zero;
        UpdateMoveInput();
    }

    public virtual void UpdateMoveInput()
    {
        MoveInput = CanMove ? RawInput : Vector2.zero;
    }

    protected virtual void Update()
    {
        MovePlayer();
        RestrainPlayerMovementWithinCamera();
    }

    protected virtual void MovePlayer()
    {
        if (CanMove)
        {
            Rb.linearVelocity = Vector2.Lerp(Rb.linearVelocity, MoveInput * moveSpeed, 0.1f);
        }
        else
        {
            MoveInput = Vector2.zero;
           
        }
    }

    
    protected virtual async void Die()
    {
        try
        {
            AudioManager.PlaySound(AudioManager.AudioLibrary.MiscSounds.LooseGameSound);
            inputActions.Player.Move.Disable();
            DisablePlayerColliders();
            Debug.Log("Player died!");
            CanMove = false;
            isDead = true;
            await Effects.Effects.PlayDissolve(shaderAnimDuration);
            GameEnd.TriggerOnGameEnd();
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    protected async void EnterInvulState()
    {
        try
        {
            Health.IsInvurable = true;
            Effects.Effects.SetBlinking(true);
            await UniTask.Delay((int)(invulnerabilityDuration * 1000));
            Health.IsInvurable = false;
            Effects.Effects.SetBlinking(false);
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    private void RestrainPlayerMovementWithinCamera() 
    {
        Vector3 pos = transform.position;
        Camera mainCam = Camera.main;    
        
        Vector3 minBounds = mainCam.ViewportToWorldPoint(new Vector3(0, 0, mainCam.nearClipPlane));
        Vector3 maxBounds = mainCam.ViewportToWorldPoint(new Vector3(1, 1, mainCam.nearClipPlane));

        pos.x = Mathf.Clamp(pos.x, minBounds.x + cameraEdgeMargin, maxBounds.x - cameraEdgeMargin);
        pos.y = Mathf.Clamp(pos.y, minBounds.y + cameraEdgeMargin, maxBounds.y - cameraEdgeMargin);
        transform.position = pos;

    }

    private void DisablePlayerColliders()
    {
        Collider2D[] colliders = GetComponents<Collider2D>();

        foreach (Collider2D collider1 in colliders)
        {
            collider1.enabled = false;
        }
    }
}
