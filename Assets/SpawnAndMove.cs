using UnityEngine;

public class SpawnAndMove : MonoBehaviour
{
    public GameObject prefabToSpawn; // Префаб объекта, который будет создаваться
    public Transform targetObject; // Целевой объект, к которому будут двигаться созданные объекты
    public float spawnInterval = 2f; // Интервал между созданием объектов
    public float moveSpeed = 5f; // Скорость движения созданных объектов

    private float lastSpawnTime;

    private void Start()
    {
        lastSpawnTime = Time.time;
    }

    private void Update()
    {
        if (Time.time - lastSpawnTime >= spawnInterval)
        {
            SpawnObject();
            lastSpawnTime = Time.time;
        }
    }

    private void SpawnObject()
    {
        GameObject newObject = Instantiate(prefabToSpawn, transform.position, Quaternion.identity);
        MoveTowardsTarget moveScript = newObject.AddComponent<MoveTowardsTarget>();
        moveScript.targetObject = targetObject;
        moveScript.moveSpeed = moveSpeed;
    }
}
