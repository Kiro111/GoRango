using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public int maxHealth = 1; // ������������ �������� �����
    private int health; // ������� �������� �����
    public ContinuousRotation rango;
    private int killedEnemies = 0;
    private Animator anim;
    // ������ �� ��������� �������, ������������ ���������� ������ ������




    private void Start()
    {
        anim = GetComponent<Animator>();
        health = maxHealth; // ������ ��������� �������� �����
    }

    public void TakeDamage()
    {
        health--;

        if (health <= 0)
        {
            killedEnemies++; // ����������� ������� ������ ������
            FindObjectOfType<TouchAimController>()?.IncreaseScore(); // �������� ����� IncreaseScore � ������� TouchAimController\

            anim.SetBool("IsWalking", false);
            anim.SetBool("IsShooting",false);

            anim.SetBool("Death", true);

            Destroy(gameObject, 5f);
        }
    }
    public bool IsAlive()
    {
        return health > 0;
    }

}