using UnityEngine;

public class ProjectileController1 : MonoBehaviour
{
    public int damageAmount = 1;
    public GameObject bloodParticlesPrefab;

    private void OnTriggerEnter(Collider other)
    {
        // Получаем компонент PlayerManager у столкнувшегося объекта (если он есть)
        PlayerManager player = other.GetComponent<PlayerManager>();

        // Если объект имеет компонент EnemyManager, значит, это Player
        if (player != null)
        {
            // Наносим урон Player и уничтожаем шарик
            player.TakeDamage();
            Instantiate(bloodParticlesPrefab, transform.position, Quaternion.identity);

            Destroy(gameObject,3);
        }
        else
            Destroy(gameObject, 3);
    }
   
    
}
