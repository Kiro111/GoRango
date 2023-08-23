using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public int damageAmount = 15;
    public GameObject bloodParticlesPrefab;

    private void OnTriggerEnter(Collider other)
    {
        // Получаем компонент EnemyManager у столкнувшегося объекта (если он есть)
        EnemyController enemyManager = other.GetComponent<EnemyController>();

        // Если объект имеет компонент EnemyManager, значит, это враг
        if (enemyManager != null)
        {
            // Наносим урон врагу и уничтожаем шарик
            enemyManager.TakeDamage();
            Instantiate(bloodParticlesPrefab, transform.position, Quaternion.identity);

            Destroy(gameObject,3);
        }
        else
            Destroy(gameObject, 3);
    }
   
    
}
