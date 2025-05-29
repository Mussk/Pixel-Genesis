using UnityEngine;

[ExecuteAlways]
public class ParallaxControl : MonoBehaviour
{
    private static readonly int ScrollEnabled = Shader.PropertyToID("_ScrollEnabled");

    public Material material;
    
    private void Update()
    {
        material.SetFloat(ScrollEnabled, Application.isPlaying ? 1f : 0f);
    }
}
