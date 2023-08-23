using UnityEngine;
using System.Collections;
using Cinemachine;
using System;

[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private GameObject head;
    [SerializeField] private Rigidbody _rigidbody;

    public float moveSpeed = 5f; // Скорость перемещения стикмена
    public GameObject bulletPrefab; // Префаб для шарика
    public Transform firePoint; // Точка, из которой будет выпущен шарик
    public float bulletForce = 20f; // Сила, с которой шарик будет выпущен

    public float detectionRadius = 10f; // Радиус обнаружения врагов
    public float shootCooldown = 0.5f; // Время задержки между выстрелами

    public string enemyTag = "Enemy"; // Тег для обнаружения врагов

    public LineRenderer aimLineRenderer; // Ссылка на компонент LineRenderer для отображения луча прицеливания

    private bool isMoving = true; // Флаг, указывающий на то, что стикмен движется
    private bool isAiming = false; // Флаг, указывающий на то, что игрок прицеливается
    private bool canShoot = false; // Флаг, указывающий на возможность стрельбы
    private float lastShootTime = 0f; // Время последнего выстрела

    private Animator playerAnimator; // Ссылка на компонент Animator (необходимо добавить)

    private int bulletCount = 400; // Количество патронов у игрока

    public CinemachineFreeLook virtualCamera;
    public float rotationSpeed = 10f;


    public int maxHealth = 5; // Максимальное здоровье врага
    private int health; // Текущее здоровье врага
    private int killedPlayer = 0;

    void Start()
    {
        virtualCamera = FindObjectOfType<CinemachineFreeLook>();
        // Получаем компонент Animator из текущего объекта
        playerAnimator = GetComponent<Animator>();

        // Инициализируем LineRenderer, если он есть
        if (aimLineRenderer != null)
        {
            aimLineRenderer.enabled = false;
        }
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            // Обработка касания
            Touch touch = Input.GetTouch(0);
            RotateHeadWithTouch(touch);
        }

        // Обработка движения курсора мыши
        if (Input.GetMouseButton(0))
        {
            RotateHeadWithMouse();
        }

        if (virtualCamera != null)
        {
            // Устанавливаем позицию и ориентацию Cinemachine в соответствии с персонажем.
            virtualCamera.Follow = transform;
            virtualCamera.LookAt = transform;
        }

        if (isAiming && aimLineRenderer != null)
        {
            RaycastHit hit;
            if (Physics.Raycast(firePoint.position, firePoint.forward, out hit, detectionRadius, LayerMask.GetMask("Enemies")))
            {
                // Если лучевое трассирование попало на врага, разрешаем игроку стрелять
                canShoot = true;
            }
            else
            {
                // Если лучевое трассирование не попало на врага, запрещаем стрельбу
                canShoot = false;
            }
        }

        if (isMoving)
        {
            playerAnimator.SetBool("Walking", true);
        }
        else
        {
            // Если врагов в радиусе обнаружения нет, сбрасываем прицеливание и возможность стрельбы
            isAiming = false;
            canShoot = false;
            playerAnimator.SetBool("Shooting", false);
            playerAnimator.SetBool("Shoot", false);
        }
    }

    private void RotateHeadWithMouse()
    {
        // Получаем значение смещения мыши по оси X
        float mouseX = Input.GetAxis("Mouse X");

        // Получаем текущий поворот головы
        Vector3 currentRotation = head.transform.rotation.eulerAngles;

        // Вычисляем новый угол поворота головы по оси Y
        float newYRotation = currentRotation.y + mouseX * rotationSpeed;

        // Ограничиваем угол поворота до ±30 градусов
        newYRotation = Mathf.Clamp(newYRotation, -30f, 30f);

        // Применяем новый угол поворота головы
        head.transform.rotation = Quaternion.Euler(currentRotation.x, newYRotation, currentRotation.z);
    }

    private void RotateHeadWithTouch(Touch touch)
    {
        // Получаем смещение касания по оси X
        float touchDeltaX = touch.deltaPosition.x;

        // Получаем текущий поворот головы
        Vector3 currentRotation = head.transform.rotation.eulerAngles;

        // Вычисляем новый угол поворота головы по оси Y
        float newYRotation = currentRotation.y + touchDeltaX * rotationSpeed;

        // Ограничиваем угол поворота до ±30 градусов
        newYRotation = Mathf.Clamp(newYRotation, -30f, 30f);

        // Применяем новый угол поворота головы
        head.transform.rotation = Quaternion.Euler(currentRotation.x, newYRotation, currentRotation.z);
    }

    // Метод для установки направления движения персонажа
    public void SetMovingDirection(Vector3 direction)
    {
        // Если направление движения имеет ненулевую длину, поворачиваем голову в этом направлении
        if (direction.magnitude > 0f)
        {
            // Используем Atan2, чтобы получить угол поворота головы по оси Y
            float targetRotationY = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

            // Ограничиваем угол поворота до ±30 градусов
            targetRotationY = Mathf.Clamp(targetRotationY, -30f, 30f);

            // Применяем новый угол поворота головы
            head.transform.rotation = Quaternion.Euler(head.transform.rotation.eulerAngles.x, targetRotationY, head.transform.rotation.eulerAngles.z);
        }
    }
    public void TakeDamage()
    {
        health--;

        if (health <= 0)
        {
            killedPlayer++; // Увеличиваем счетчик убитых врагов
            FindObjectOfType<TouchAimController>()?.IncreaseScore(); // Вызываем метод IncreaseScore у объекта TouchAimController
            Destroy(gameObject);
        }
    }
}
