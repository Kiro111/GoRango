using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchCharacterController : MonoBehaviour, IDragHandler
{
    private Vector3 targetPosition;
    private bool isMoving = false;

    [SerializeField]
    private float movementSpeed = 5f;

    public PlayerManager playerManager; // Ссылка на компонент PlayerManager

    public void OnDrag(PointerEventData eventData)
    {
        if (!isMoving)
        {
            targetPosition = Camera.main.ScreenToWorldPoint(eventData.position);
            targetPosition.z = transform.position.z;
            StartCoroutine(MoveToTarget());

            // Вычисляем направление движения от текущей позиции до целевой позиции
            Vector3 moveDirection = (targetPosition - transform.position).normalized;

            // Вызываем метод SetMovingDirection из PlayerManager, чтобы установить направление движения персонажа
            playerManager.SetMovingDirection(moveDirection);
        }
    }

    private IEnumerator MoveToTarget()
    {
        isMoving = true;
        Vector3 startPosition = transform.position;
        float distance = Vector3.Distance(startPosition, targetPosition);

        while (distance > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, movementSpeed * Time.deltaTime);
            distance = Vector3.Distance(transform.position, targetPosition);
            yield return null;
        }

        isMoving = false;
    }
}
