using UnityEngine;

public class ProjectileController1 : MonoBehaviour
{
    public int damageAmount = 1;
    public GameObject bloodParticlesPrefab;

    private void OnTriggerEnter(Collider other)
    {
        // �������� ��������� PlayerManager � �������������� ������� (���� �� ����)
        PlayerManager player = other.GetComponent<PlayerManager>();

        // ���� ������ ����� ��������� EnemyManager, ������, ��� Player
        if (player != null)
        {
            // ������� ���� Player � ���������� �����
            player.TakeDamage();
            Instantiate(bloodParticlesPrefab, transform.position, Quaternion.identity);

            Destroy(gameObject,3);
        }
        else
            Destroy(gameObject, 3);
    }
   
    
}
