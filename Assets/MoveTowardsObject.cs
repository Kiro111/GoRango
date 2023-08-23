using UnityEngine;

public class MoveTowardsObject : MonoBehaviour
{
    public Transform targetObject; // ���������� ���� ������ ������, � �������� ����� ���������
    public float moveSpeed = 5f; // �������� ��������

    private void Update()
    {
        if (targetObject != null)
        {
            Vector3 targetPosition = targetObject.position;
            Vector3 moveDirection = targetPosition - transform.position;
            float distanceToTarget = moveDirection.magnitude;

            if (distanceToTarget > 0.1f) // ����� ��������� ���� ����� �� ������ ����������
            {
                Vector3 moveDelta = moveDirection.normalized * moveSpeed * Time.deltaTime;
                transform.position += moveDelta;
            }
        }
    }
}
