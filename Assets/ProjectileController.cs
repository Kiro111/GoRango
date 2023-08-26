using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public int damageAmount =1 ;
    public GameObject bloodParticlesPrefab;

    private void OnTriggerEnter(Collider other)
    {
        // �������� ��������� EnemyManager � �������������� ������� (���� �� ����)
        EnemyController enemyManager = other.GetComponent<EnemyController>();
        
        // ���� ������ ����� ��������� EnemyManager, ������, ��� ����
        if (enemyManager != null)
        {
            // ������� ���� ����� � ���������� �����
            enemyManager.TakeDamage();
            Instantiate(bloodParticlesPrefab, transform.position, Quaternion.identity);

            Destroy(gameObject,3);
        }
        else
            Destroy(gameObject, 3);
    }
   
    
}
