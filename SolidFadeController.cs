using UnityEngine;

public class SolidFadeController : MonoBehaviour
{
    [SerializeField]
    private Animator solidAnimator;

    [SerializeField]
    private AnimationClip fadeOutClip;

    [SerializeField]
    private AnimationClip fadeInClip;

    [SerializeField]
    private float speed;

    private void Awake()
    {
        PlayFadeOutAnimation();
    }

    public void PlayFadeInAnimation()
    {   
        solidAnimator.speed = speed;
        solidAnimator.Play(fadeInClip.name);
    }

    public void PlayFadeOutAnimation()
    {
        solidAnimator.speed = speed;
        solidAnimator.Play(fadeOutClip.name);
    }
}
