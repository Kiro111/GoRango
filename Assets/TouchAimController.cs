using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class TouchAimController : MonoBehaviour
{
    [SerializeField] private GameObject weapon;
    [SerializeField] private GameObject revalver;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject playerHat;



    public float aimSpeedCanvas = 10f;
    public float minX = -200f;
    public float minY = -200f;
    public float maxX = 200f;
    public float maxY = 410f;
    public Color defaultAimColor = Color.white;
    public Color enemyAimColor = Color.red;
    public LineRenderer aimLineRenderer;

    private Transform targetTransform;
    private SpriteRenderer targetSpriteRenderer;
    private bool isInputActive;

    private bool isPlayingRightAnimation;
    private bool isPlayingLeftAnimation;
    private bool canShoot = true;
    public List<GameObject> enemies = new List<GameObject>();
    private Vector3 enemyPosition;

    public List<Transform> enemyTransforms = new List<Transform>();
    public Transform aim;
    private bool isAimingAtEnemy;


    private int score = 0;
    [SerializeField] private TextMeshProUGUI killedEnemiesText;
    public float baseEnemySpeed = 5f;
  
    void Start()
    {
        

        targetTransform = GetComponent<Transform>();
        targetSpriteRenderer = GetComponent<SpriteRenderer>();

        targetSpriteRenderer.color = defaultAimColor;
        isInputActive = false;



        


        // Найти всех врагов на сцене и добавить их в список
        GameObject[] enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");
        enemies.AddRange(enemyObjects);
        foreach (var enemyObject in enemyObjects)
        {
            enemyTransforms.Add(enemyObject.transform);
        }
    }

    void Update()
    {



        if (enemyTransforms.Count > 0)
        {
            Vector3 playerPosition = playerHat.transform.position;

            foreach (var enemyTransform in enemyTransforms)
            {
                if (enemyTransform == null)
                {
                    continue; // Пропустить уничтоженного врага
                }

                Vector3 enemyPosition = enemyTransform.position;

                Vector3 directionToPlayer = (playerPosition - enemyPosition).normalized;

                //// Изменяем позицию врага, двигая его в сторону игрока
                //float enemySpeed = Mathf.Lerp(baseEnemySpeed, baseEnemySpeed * 10f, Time.timeSinceLevelLoad / 10f); // Увеличение скорости врагов с течением времени
                //enemyTransform.position += directionToPlayer * enemySpeed * Time.deltaTime;
            }
        }

        Animator playerAnimator = player.GetComponent<Animator>();

        if (enemyTransforms==null)
        {
            playerAnimator.SetBool("IsWalking", true);
        }



        if (aim != null && enemyTransforms.Count > 0)
        {
            // Получаем компонент камеры
            Camera mainCamera = Camera.main;

            Vector3 worldPositionAim = aim.position;

            foreach (var enemyTransform in enemyTransforms)
            {
                if (enemyTransform == null)
                {
                    continue; // Пропустить уничтоженного врага
                }

                Vector3 worldPositionEnemy = enemyTransform.position;

                Vector2 screenPositionAim = mainCamera.WorldToScreenPoint(worldPositionAim);
                Vector2 screenPositionEnemy = mainCamera.WorldToScreenPoint(worldPositionEnemy);

                float distanceToEnemy = Vector2.Distance(screenPositionAim, screenPositionEnemy);
                float shootDistanceThreshold = 50f;

                if (distanceToEnemy < shootDistanceThreshold)
                {
                    enemyPosition = worldPositionEnemy;
                    isAimingAtEnemy = true;
                    ShootProjectile(enemyPosition);
                }
                else
                {
                    isAimingAtEnemy = false;
                }
            }
        }

        isInputActive = Input.touchCount > 0 || Input.GetMouseButton(0);

        if (isInputActive)
        {
            Vector3 anchoredPosition = targetTransform.position;
            anchoredPosition.x = Mathf.Clamp(anchoredPosition.x, minX, maxX);
            anchoredPosition.y = Mathf.Clamp(anchoredPosition.y, minY, maxY);
            float step = aimSpeedCanvas * Time.deltaTime;

            Vector2 currentVelocity = Vector2.zero;
            float interpolationFactor = 1f - Mathf.Exp(-aimSpeedCanvas * Time.deltaTime);
            targetTransform.position = Vector2.Lerp(targetTransform.position, anchoredPosition, interpolationFactor);

            bool isMovingRight = anchoredPosition.x > 0;
            bool isMovingLeft = anchoredPosition.x < 0;

            if (playerAnimator != null)
            {
                if (isPlayingLeftAnimation && anchoredPosition.x > -18f)
                {
                    playerAnimator.SetBool("Left", false);
                    isPlayingLeftAnimation = false;
                }

                if (isPlayingRightAnimation && anchoredPosition.x < -18f)
                {
                    playerAnimator.SetBool("Right", false);
                    isPlayingRightAnimation = false;
                }

                if (!isPlayingRightAnimation && anchoredPosition.x > -18f)
                {
                    playerAnimator.SetBool("Right", true);
                    isPlayingRightAnimation = true;
                }

                if (!isPlayingLeftAnimation && anchoredPosition.x <-18f)
                {
                    playerAnimator.SetBool("Left", true);
                    isPlayingLeftAnimation = true;
                }
            }
        }
        else
        {
           
            if (playerAnimator != null)
            {
                playerAnimator.SetBool("Right", false);
                playerAnimator.SetBool("Left", false);
                isPlayingRightAnimation = false;
                isPlayingLeftAnimation = false;
            }
        }
    }

    public void ShootProjectile(Vector3 targetPosition)
    {
        if (bullet == null)
        {
            Debug.LogError("Projectile prefab is not set!");
            return;
        }

        if (canShoot && isAimingAtEnemy)
        {
            if (isPlayingRightAnimation == true)
            {
                GameObject newProjectile = Instantiate(bullet, weapon.transform.position, Quaternion.identity);

                Vector3 direction = (targetPosition - weapon.transform.position).normalized;

                Rigidbody projectileRb = newProjectile.GetComponent<Rigidbody>();
                float projectileSpeed = 200f;
                projectileRb.velocity = direction * projectileSpeed;
            }
           if (isPlayingLeftAnimation == true)
            {
                GameObject newProjectile = Instantiate(bullet, revalver.transform.position, Quaternion.identity);
                Vector3 direction = (targetPosition - weapon.transform.position).normalized;

                Rigidbody projectileRb = newProjectile.GetComponent<Rigidbody>();
                float projectileSpeed = 200f;
                projectileRb.velocity = direction * projectileSpeed;


            }
           

           

            canShoot = false;
            float shootDelay = 0.3f;
            StartCoroutine(ShootDelayCoroutine(shootDelay));

            if (enemies.Count == 0)
            {
                canShoot = false;
            }
        }
    }
   

    private IEnumerator ShootDelayCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        canShoot = true;
    }


    //private IEnumerator AddEnemiesCoroutine()
    //{
    //    yield return new WaitForSeconds(100f); // Ждать 7 секунд перед первой итерацией

    //    while (true)
    //    {
    //        // Создать нового врага из префаба и добавить его в список enemyTransforms
    //        GameObject newEnemy = Instantiate(enemyPrefab, new Vector3(Random.Range(-40f, 40f), Random.Range(60f, 80f), Random.Range(200f, 250f)), Quaternion.identity);
    //        enemyTransforms.Add(newEnemy.transform);

    //        yield return new WaitForSeconds(2f); // Ждать 2 секунды перед следующей итерацией
    //    }
    //}
    public void IncreaseScore()
    {
        score++;
        // Обновите отображение счета на UI
        if (killedEnemiesText != null)
        {
            killedEnemiesText.text = score.ToString();
        }
    }
}
