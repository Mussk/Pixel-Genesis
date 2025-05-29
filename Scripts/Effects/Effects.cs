using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Effects
{
    public abstract class Effects
    {
        private static readonly int Blinking = Shader.PropertyToID("_Blinking");
        private static readonly int Fade = Shader.PropertyToID("_Fade");
    
        private static readonly Material DissolveBlinkMat = Resources.Load<Material>("Materials/DissolveBlinkingMaterial");
        
        public static async UniTask PlayDissolve(float duration)
        {   
            DissolveBlinkMat.SetFloat(Blinking, 0);
            float elapsed = 0f;
            float start = 1f;
            float end = 0f;

            while (elapsed < duration)
            {
                elapsed += 1;
                float t = elapsed / duration;
                DissolveBlinkMat.SetFloat(Fade, Mathf.Lerp(start, end, t));
                await UniTask.Delay(16);
            }

            DissolveBlinkMat.SetFloat(Fade, end);
        }

        public static void ResetFade()
        {
            DissolveBlinkMat.SetFloat(Fade, 1f);
        }

        public static void SetBlinking(bool state)
        {
            DissolveBlinkMat.SetFloat(Blinking, state ? 1 : 0);
        }
    }
}
