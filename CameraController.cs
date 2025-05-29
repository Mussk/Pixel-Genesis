using UnityEngine;


public class CameraController : MonoBehaviour
{
    [SerializeField]
    private GameObject target;
    [SerializeField]
    public float smoothSpeed = 5f;
    [SerializeField]
    public Vector3 offset;

    private void FixedUpdate()
    {
        Vector3 desiredPosition = target.transform.position + offset;
        desiredPosition.z = transform.position.z;

        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
    }
}
