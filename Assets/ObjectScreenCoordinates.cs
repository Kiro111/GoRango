using UnityEngine;

public class ObjectScreenCoordinates : MonoBehaviour
{
    public Transform targetObject;

    void Update()
    {
        // ���������, ��� ������ �� ����� null, ����� �������� ������
        if (targetObject != null)
        {
            // �������� ��������� ������
            Camera mainCamera = Camera.main;

            // �������� ������� ���������� �������
            Vector3 worldPosition = targetObject.position;

            // ����������� ������� ���������� � ��������
            Vector3 screenPosition = mainCamera.WorldToScreenPoint(worldPosition);

            // ������� �������� ���������� � �������
            Debug.Log("Screen Coordinates: " + screenPosition);
        }
    }
}
