using UnityEngine;

public class TribeCameraController : MonoBehaviour
{

    [SerializeField]
    private GameObject player;

    [SerializeField]
    public bool CanMoveToNextZone;

    [SerializeField] 
    public bool IsCameraMovementTriggered;

    [SerializeField]
    private float moveSpeed;

    public Transform CurrentZoneConstraint;

    

    // Update is called once per frame
    private void FixedUpdate()
    {
        MoveCamera();
    }


    private void MoveCamera() 
    {
   
        if (transform.position.x < player.transform.position.x 
            && CanMoveToNextZone
            && IsCameraMovementTriggered) 
            transform.position += new Vector3(moveSpeed * Time.deltaTime, 0, 0);
        
        if(Mathf.Approximately(transform.position.x, player.transform.position.x))
           IsCameraMovementTriggered = false;

        if (!(transform.position.x >= CurrentZoneConstraint.position.x)) return;
        CanMoveToNextZone = false;
        IsCameraMovementTriggered = false;


    }

}
