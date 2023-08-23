using UnityEngine;
using System.Collections;
using Cinemachine;
using System;

[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private GameObject head;
    [SerializeField] private Rigidbody _rigidbody;

    public float moveSpeed = 5f; // �������� ����������� ��������
    public GameObject bulletPrefab; // ������ ��� ������
    public Transform firePoint; // �����, �� ������� ����� ������� �����
    public float bulletForce = 20f; // ����, � ������� ����� ����� �������

    public float detectionRadius = 10f; // ������ ����������� ������
    public float shootCooldown = 0.5f; // ����� �������� ����� ����������

    public string enemyTag = "Enemy"; // ��� ��� ����������� ������

    public LineRenderer aimLineRenderer; // ������ �� ��������� LineRenderer ��� ����������� ���� ������������

    private bool isMoving = true; // ����, ����������� �� ��, ��� ������� ��������
    private bool isAiming = false; // ����, ����������� �� ��, ��� ����� �������������
    private bool canShoot = false; // ����, ����������� �� ����������� ��������
    private float lastShootTime = 0f; // ����� ���������� ��������

    private Animator playerAnimator; // ������ �� ��������� Animator (���������� ��������)

    private int bulletCount = 400; // ���������� �������� � ������

    public CinemachineFreeLook virtualCamera;
    public float rotationSpeed = 10f;


    public int maxHealth = 5; // ������������ �������� �����
    private int health; // ������� �������� �����
    private int killedPlayer = 0;

    void Start()
    {
        virtualCamera = FindObjectOfType<CinemachineFreeLook>();
        // �������� ��������� Animator �� �������� �������
        playerAnimator = GetComponent<Animator>();

        // �������������� LineRenderer, ���� �� ����
        if (aimLineRenderer != null)
        {
            aimLineRenderer.enabled = false;
        }
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            // ��������� �������
            Touch touch = Input.GetTouch(0);
            RotateHeadWithTouch(touch);
        }

        // ��������� �������� ������� ����
        if (Input.GetMouseButton(0))
        {
            RotateHeadWithMouse();
        }

        if (virtualCamera != null)
        {
            // ������������� ������� � ���������� Cinemachine � ������������ � ����������.
            virtualCamera.Follow = transform;
            virtualCamera.LookAt = transform;
        }

        if (isAiming && aimLineRenderer != null)
        {
            RaycastHit hit;
            if (Physics.Raycast(firePoint.position, firePoint.forward, out hit, detectionRadius, LayerMask.GetMask("Enemies")))
            {
                // ���� ������� ������������� ������ �� �����, ��������� ������ ��������
                canShoot = true;
            }
            else
            {
                // ���� ������� ������������� �� ������ �� �����, ��������� ��������
                canShoot = false;
            }
        }

        if (isMoving)
        {
            playerAnimator.SetBool("Walking", true);
        }
        else
        {
            // ���� ������ � ������� ����������� ���, ���������� ������������ � ����������� ��������
            isAiming = false;
            canShoot = false;
            playerAnimator.SetBool("Shooting", false);
            playerAnimator.SetBool("Shoot", false);
        }
    }

    private void RotateHeadWithMouse()
    {
        // �������� �������� �������� ���� �� ��� X
        float mouseX = Input.GetAxis("Mouse X");

        // �������� ������� ������� ������
        Vector3 currentRotation = head.transform.rotation.eulerAngles;

        // ��������� ����� ���� �������� ������ �� ��� Y
        float newYRotation = currentRotation.y + mouseX * rotationSpeed;

        // ������������ ���� �������� �� �30 ��������
        newYRotation = Mathf.Clamp(newYRotation, -30f, 30f);

        // ��������� ����� ���� �������� ������
        head.transform.rotation = Quaternion.Euler(currentRotation.x, newYRotation, currentRotation.z);
    }

    private void RotateHeadWithTouch(Touch touch)
    {
        // �������� �������� ������� �� ��� X
        float touchDeltaX = touch.deltaPosition.x;

        // �������� ������� ������� ������
        Vector3 currentRotation = head.transform.rotation.eulerAngles;

        // ��������� ����� ���� �������� ������ �� ��� Y
        float newYRotation = currentRotation.y + touchDeltaX * rotationSpeed;

        // ������������ ���� �������� �� �30 ��������
        newYRotation = Mathf.Clamp(newYRotation, -30f, 30f);

        // ��������� ����� ���� �������� ������
        head.transform.rotation = Quaternion.Euler(currentRotation.x, newYRotation, currentRotation.z);
    }

    // ����� ��� ��������� ����������� �������� ���������
    public void SetMovingDirection(Vector3 direction)
    {
        // ���� ����������� �������� ����� ��������� �����, ������������ ������ � ���� �����������
        if (direction.magnitude > 0f)
        {
            // ���������� Atan2, ����� �������� ���� �������� ������ �� ��� Y
            float targetRotationY = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

            // ������������ ���� �������� �� �30 ��������
            targetRotationY = Mathf.Clamp(targetRotationY, -30f, 30f);

            // ��������� ����� ���� �������� ������
            head.transform.rotation = Quaternion.Euler(head.transform.rotation.eulerAngles.x, targetRotationY, head.transform.rotation.eulerAngles.z);
        }
    }
    public void TakeDamage()
    {
        health--;

        if (health <= 0)
        {
            killedPlayer++; // ����������� ������� ������ ������
            FindObjectOfType<TouchAimController>()?.IncreaseScore(); // �������� ����� IncreaseScore � ������� TouchAimController
            Destroy(gameObject);
        }
    }
}
