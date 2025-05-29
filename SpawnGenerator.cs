using UnityEngine;

public enum HorizontalConstraintMode
{ 
    NegativeToZero,
    ZeroToPositive,
    Both
}


public class SpawnGenerator : MonoBehaviour
{
    [SerializeField]
    protected GameObject[] entitiesToSpawn;

    [SerializeField]
    protected int spawnCount;

    [SerializeField]
    protected Vector2 spawnConstraints;

    [SerializeField]
    protected HorizontalConstraintMode constraintMode;

    protected virtual void Start()
    {
        RandomSpawnContent();
        
    }


    protected virtual void RandomSpawnContent() 
    {
        foreach (var entity in entitiesToSpawn) 
        {
            
            for (int i = 0; i < spawnCount; i++)
            {

                Vector3 spawnCoordinates = Vector3.zero;

                switch (constraintMode) 
                {

                    case HorizontalConstraintMode.NegativeToZero:
                        spawnCoordinates = new Vector3(Random.Range(-spawnConstraints.x, 0),
                                                       Random.Range(-spawnConstraints.y, spawnConstraints.y), 0);
                        break;
                    case HorizontalConstraintMode.ZeroToPositive:
                        spawnCoordinates = new Vector3(Random.Range(0, spawnConstraints.x),
                                                       Random.Range(-spawnConstraints.y, spawnConstraints.y), 0);
                        break;
                    case HorizontalConstraintMode.Both:
                        spawnCoordinates = new Vector3(Random.Range(-spawnConstraints.x, spawnConstraints.x),
                                                       Random.Range(-spawnConstraints.y, spawnConstraints.y), 0);
                        break;
                }

                

                Instantiate(entity, spawnCoordinates, Quaternion.identity);
            }
        }

    }
   
}
