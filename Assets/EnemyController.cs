using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public int maxHealth = 1; // Максимальное здоровье врага
    private int health; // Текущее здоровье врага
    public ContinuousRotation rango;
    private int killedEnemies = 0;
   // Ссылка на текстовый элемент, отображающий количество убитых врагов
    



    private void Start()
    {
        health = maxHealth; // Задаем начальное здоровье врага
    }

    public void TakeDamage()
    {
        health--;

        if (health <= 0)
        {
            killedEnemies++; // Увеличиваем счетчик убитых врагов
            FindObjectOfType<TouchAimController>()?.IncreaseScore(); // Вызываем метод IncreaseScore у объекта TouchAimController
            Destroy(gameObject);
        }
    }

}