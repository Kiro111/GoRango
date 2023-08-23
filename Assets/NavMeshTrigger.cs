using UnityEngine;

public class NavMeshTrigger : MonoBehaviour
{
    public EnemyManager enemyManager; // Перетащите сюда компонент EnemyManager из объекта в Hierarсhy

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enemyManager.SetNavMeshActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enemyManager.SetNavMeshActive(false);
        }
    }
}
