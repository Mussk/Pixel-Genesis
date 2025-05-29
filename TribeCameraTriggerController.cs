using UnityEngine;

public class TribeCameraTriggerController : MonoBehaviour
{

    [SerializeField]
    private string playerTag;

    [SerializeField]
    private TribeCameraController cameraController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag(playerTag) &&
            cameraController.CanMoveToNextZone)
        {

            cameraController.IsCameraMovementTriggered = true;
           
        }
    }

   
}
