using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGenerator : MonoBehaviour
{
    public GameObject[] tilePrefabs;
    private float spawnPos =100;
    private float tileLength =293;
    private float timeToSpawn = 5f; // ����� ����� ������� ����� ������ ����� ����
    private float timeToDelete = 90f; // ����� ����� ������� ����� ������ ���� �� ������

    [SerializeField] private Transform player;
    private int startTiles = 1;
    private int maxTiles = 5;

    private List<GameObject> spawnedTiles = new List<GameObject>(); // ������ ��� �������� ��������� ������

    void Start()
    {
        for (int i = 0; i < startTiles; i++)
        {
            SpawnTile(Random.Range(0, tilePrefabs.Length));
        }
    }

    void Update()
    {
      

        // ���������, ����� �� ������� ����� ����
        if (Time.time > timeToSpawn)
        {
            SpawnTile(Random.Range(0, tilePrefabs.Length));
            timeToSpawn = Time.time + 5f; // ������������� ����� ��� ��������� ���������
        }

        // ���������, ����� �� ������� ����
        if (spawnedTiles.Count > 0 && Time.time > timeToDelete)
        {
            Destroy(spawnedTiles[0]); // ������� ������ ���� �� ������
            spawnedTiles.RemoveAt(0); // ������� ��� �� ������
            timeToDelete = Time.time + 45f; // ������������� ����� ��� ���������� ��������
        }
    }

    private void SpawnTile(int tileIndex)
    {
        if (spawnedTiles.Count >= maxTiles)
        {
            return; // �� ��������� ����� ����, ���� ���������� ������������ ����������
        }
            GameObject newTile = Instantiate(tilePrefabs[tileIndex], new Vector3(0, 0, spawnPos), Quaternion.identity);
        spawnedTiles.Add(newTile); // ��������� ����� ���� � ������
        spawnPos += tileLength;
    }
}
