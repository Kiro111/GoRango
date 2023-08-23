using UnityEngine;

public class SpawnAndMove : MonoBehaviour
{
    public GameObject prefabToSpawn; // ������ �������, ������� ����� �����������
    public Transform targetObject; // ������� ������, � �������� ����� ��������� ��������� �������
    public float spawnInterval = 2f; // �������� ����� ��������� ��������
    public float moveSpeed = 5f; // �������� �������� ��������� ��������

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
