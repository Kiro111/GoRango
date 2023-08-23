using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public int maxHealth = 1; // ������������ �������� �����
    private int health; // ������� �������� �����
    public ContinuousRotation rango;
    private int killedEnemies = 0;
   // ������ �� ��������� �������, ������������ ���������� ������ ������
    



    private void Start()
    {
        health = maxHealth; // ������ ��������� �������� �����
    }

    public void TakeDamage()
    {
        health--;

        if (health <= 0)
        {
            killedEnemies++; // ����������� ������� ������ ������
            FindObjectOfType<TouchAimController>()?.IncreaseScore(); // �������� ����� IncreaseScore � ������� TouchAimController
            Destroy(gameObject);
        }
    }

}