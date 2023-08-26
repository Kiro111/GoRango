using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    private EnemyController health;
    public bool isHit = false;
    public GameObject player;
    public GameObject projectilePrefab; // ������ �������
    public Transform firePoint; // �����, ������ ����� ����������� ������
    public EnemyController enemyController;

    private Animator anim;
    private NavMeshAgent agent;
    private bool isNavMeshActive = false;
    private float nextFireTime = 0f; // ����� ���������� ���������� ��������

    public float fireCooldown = 2f; // ����� ����� ����������

    private void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

 

    void Update()
    {
        if (player != null && enemyController != null && enemyController.IsAlive())
        {
            Vector3 directionToPlayer = player.transform.position - transform.position;
            directionToPlayer.Normalize();

            if (isNavMeshActive)
            {
                agent.enabled = true;
               
                agent.SetDestination(player.transform.position);

                anim.SetBool("IsWalking", true); // �������� �������� ������
            }
            else
            {
                agent.enabled = false;
            }

            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

         
            if (distanceToPlayer <= 100f)
            {
                agent.enabled = false;
                anim.SetBool("IsWalking", false);
                anim.SetBool("IsShooting", true);

                // ��������� �������� ������� ��� ��������
                if (Time.time >= nextFireTime)
                {
                    Shoot();
                    nextFireTime = Time.time + fireCooldown;
                }
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
        float projectileLifetime = 2f;
        Destroy(projectile, projectileLifetime);
    }
   
}
