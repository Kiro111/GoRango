using UnityEngine;

public class NavMeshTrigger : MonoBehaviour
{
    public EnemyManager enemyManager; // ���������� ���� ��������� EnemyManager �� ������� � Hierar�hy

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
