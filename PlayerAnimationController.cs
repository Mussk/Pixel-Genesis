using System;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] 
    private float animSpeed = 1f;
    [SerializeField] 
    private float offsetXStartAnim = 2f;
    public float OffsetXStartAnim => offsetXStartAnim;

    [SerializeField]
    private float offsetXEndAnim;
    public float OffsetXEndAnim => offsetXEndAnim;

    private Vector3 _startPos;
    private Vector3 _targetPos;

    private async void Start()
    {
        try
        {
            await PlayLerpAnimation(offsetXStartAnim);
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    public async Task PlayLerpAnimation(float offsetX)
    {
        _targetPos = GetPositions(offsetX);

        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * animSpeed;
            transform.position = Vector3.Lerp(_startPos, _targetPos, t);
            await Task.Yield(); 
        }
    }

    private Vector3 GetPositions(float offsetX)
    {
        _startPos = transform.position;
        return _startPos + new Vector3(offsetX, 0, 0);
    }
    
    public void DisablePlayerColliders()
    {
        foreach (Collider2D collider1 in 
            gameObject.GetComponents<Collider2D>()) {

            collider1.enabled = false;
        }
    }
}
