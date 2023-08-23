using UnityEngine;

public class ContinuousRotation : MonoBehaviour
{

    [SerializeField] GameObject enemyFinish;
    public float rotationSpeed = 90f; // Угловая скорость поворота (градусы в секунду)
    private float rotationTimer = 1f; // Таймер обнуления поворот
    public GameObject player;
    


    void Update()
    {
        
        // Поворачиваем объект на rotationSpeed градусов в секунду вокруг оси Y (вверх)
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);

        // Уменьшаем таймер обнуления поворота на время текущего кадра
        rotationTimer -= Time.deltaTime;

        // Если таймер обнуления достиг нуля или меньше, обнуляем поворот и сбрасываем таймер на 1 секунду
        if (rotationTimer <= 0f)
        {
            // Обнуляем поворот
            transform.rotation = Quaternion.identity;

            // Сбрасываем таймер на 1 секунду
            rotationTimer = 1f;
        }
    }
   
}
