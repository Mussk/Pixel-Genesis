using Enemy;
using UnityEngine;

public class BackgroundMove : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private float horizontalConstraint;

    [SerializeField]
    private GameObject parentObjectToReactivate;

    
    // Update is called once per frame
    private void FixedUpdate()
    {
        MoveBackground();
    }


    private void MoveBackground()
    {
        transform.position -= new Vector3(moveSpeed * Time.deltaTime, 0, 0);

        if (!(transform.position.x <= horizontalConstraint)) return;
        
        transform.position = new Vector3(-horizontalConstraint, 0, 0);

        if(parentObjectToReactivate is not null)
            ReactivateObjects();
    }

    private void ReactivateObjects()
    {
        
        foreach (Transform objectToActivate in parentObjectToReactivate.transform) 
        {
            
            objectToActivate.gameObject.SetActive(true);

            if (!objectToActivate.gameObject.TryGetComponent(out AsteroidEnemyController asteroidEnemyController))
                continue;
            
            asteroidEnemyController.gameObject.transform.localPosition =
                asteroidEnemyController.InitialPos;

            asteroidEnemyController.gameObject.transform.rotation =
                Quaternion.identity;

        }
    }
}
