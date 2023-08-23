using UnityEngine;

public class ObjectScreenCoordinates : MonoBehaviour
{
    public Transform targetObject;

    void Update()
    {
        // Проверяем, что объект не равен null, чтобы избежать ошибок
        if (targetObject != null)
        {
            // Получаем компонент камеры
            Camera mainCamera = Camera.main;

            // Получаем мировые координаты объекта
            Vector3 worldPosition = targetObject.position;

            // Преобразуем мировые координаты в экранные
            Vector3 screenPosition = mainCamera.WorldToScreenPoint(worldPosition);

            // Выводим экранные координаты в консоль
            Debug.Log("Screen Coordinates: " + screenPosition);
        }
    }
}
