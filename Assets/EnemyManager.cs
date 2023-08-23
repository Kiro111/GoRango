using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    public int health = 15;
    public bool isHit = false;
    public GameObject player;
    public GameObject projectilePrefab; // Префаб снаряда
    public Transform firePoint; // Место, откуда будет выпускаться снаряд

    private Animator anim;
    private NavMeshAgent agent;
    private bool isNavMeshActive = false;
    private float nextFireTime = 0f; // Время следующего возможного выстрела

    public float fireCooldown = 2f; // Время между выстрелами

    private void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    public void TakeDamage()
    {
        health--;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (player != null)
        {
            Vector3 directionToPlayer = player.transform.position - transform.position;
            directionToPlayer.Normalize();

            if (isNavMeshActive)
            {
                agent.enabled = true;
                agent.SetDestination(player.transform.position);
            }
            else
            {
                agent.enabled = false;
            }

            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

            if (distanceToPlayer > 100f)
            {
                agent.enabled = true;
                anim.SetBool("IsWalking", true);
                anim.SetBool("IsShooting", false);
            }
            else if (distanceToPlayer <= 100f)
            {
                agent.enabled = false;
                anim.SetBool("IsWalking", false);
                anim.SetBool("IsShooting", true);

                // Добавляем проверку времени для стрельбы
                if (Time.time >= nextFireTime)
                {
                    Shoot();
                    nextFireTime = Time.time + fireCooldown;
                }
            }
            else
            {
                anim.SetBool("IsShooting", false);
                anim.SetBool("IsWalking", false);

            }
        }
    }

    public void SetNavMeshActive(bool isActive)
    {
        isNavMeshActive = isActive;
    }

    void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        Vector3 directionToPlayer = (player.transform.position - firePoint.position).normalized;
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        float projectileSpeed = 10f;
        rb.velocity = directionToPlayer * projectileSpeed;
        rb.rotation = Quaternion.LookRotation(directionToPlayer);
        float projectileLifetime = 3f;
        Destroy(projectile, projectileLifetime);
    }
}
