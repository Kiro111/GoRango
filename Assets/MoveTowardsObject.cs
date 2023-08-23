using UnityEngine;

public class MoveTowardsObject : MonoBehaviour
{
    public Transform targetObject; // Перетащите сюда другой объект, к которому нужно двигаться
    public float moveSpeed = 5f; // Скорость движения

    private void Update()
    {
        if (targetObject != null)
        {
            Vector3 targetPosition = targetObject.position;
            Vector3 moveDirection = targetPosition - transform.position;
            float distanceToTarget = moveDirection.magnitude;

            if (distanceToTarget > 0.1f) // Можно настроить этот порог по вашему усмотрению
            {
                Vector3 moveDelta = moveDirection.normalized * moveSpeed * Time.deltaTime;
                transform.position += moveDelta;
            }
        }
    }
}
