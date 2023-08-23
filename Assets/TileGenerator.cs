using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGenerator : MonoBehaviour
{
    public GameObject[] tilePrefabs;
    private float spawnPos =100;
    private float tileLength =293;
    private float timeToSpawn = 5f; // Время через которое будет создан новый тайл
    private float timeToDelete = 90f; // Время через которое будет удален один из тайлов

    [SerializeField] private Transform player;
    private int startTiles = 1;
    private int maxTiles = 5;

    private List<GameObject> spawnedTiles = new List<GameObject>(); // Список для хранения созданных тайлов

    void Start()
    {
        for (int i = 0; i < startTiles; i++)
        {
            SpawnTile(Random.Range(0, tilePrefabs.Length));
        }
    }

    void Update()
    {
      

        // Проверяем, нужно ли создать новый тайл
        if (Time.time > timeToSpawn)
        {
            SpawnTile(Random.Range(0, tilePrefabs.Length));
            timeToSpawn = Time.time + 5f; // Устанавливаем время для следующей генерации
        }

        // Проверяем, нужно ли удалить тайл
        if (spawnedTiles.Count > 0 && Time.time > timeToDelete)
        {
            Destroy(spawnedTiles[0]); // Удаляем первый тайл из списка
            spawnedTiles.RemoveAt(0); // Удаляем его из списка
            timeToDelete = Time.time + 45f; // Устанавливаем время для следующего удаления
        }
    }

    private void SpawnTile(int tileIndex)
    {
        if (spawnedTiles.Count >= maxTiles)
        {
            return; // Не создавать новый тайл, если достигнуто максимальное количество
        }
            GameObject newTile = Instantiate(tilePrefabs[tileIndex], new Vector3(0, 0, spawnPos), Quaternion.identity);
        spawnedTiles.Add(newTile); // Добавляем новый тайл в список
        spawnPos += tileLength;
    }
}
