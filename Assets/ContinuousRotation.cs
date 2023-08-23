using UnityEngine;

public class ContinuousRotation : MonoBehaviour
{

    [SerializeField] GameObject enemyFinish;
    public float rotationSpeed = 90f; // ������� �������� �������� (������� � �������)
    private float rotationTimer = 1f; // ������ ��������� �������
    public GameObject player;
    


    void Update()
    {
        
        // ������������ ������ �� rotationSpeed �������� � ������� ������ ��� Y (�����)
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);

        // ��������� ������ ��������� �������� �� ����� �������� �����
        rotationTimer -= Time.deltaTime;

        // ���� ������ ��������� ������ ���� ��� ������, �������� ������� � ���������� ������ �� 1 �������
        if (rotationTimer <= 0f)
        {
            // �������� �������
            transform.rotation = Quaternion.identity;

            // ���������� ������ �� 1 �������
            rotationTimer = 1f;
        }
    }
   
}
