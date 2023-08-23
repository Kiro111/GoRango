using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl1 : MonoBehaviour
{
    private float moveSpeed = -10f;
    private bool isMoving = false;
    private Animator animator;

    private bool firstTouch = true; // Флаг для отслеживания первого касания

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.touchCount > 0 && firstTouch || Input.GetMouseButton(0))

        {
            // При первом касании начинаем движение
            isMoving = true;
            moveSpeed = -7f;
          
            firstTouch = false; // Отключаем флаг первого касания
        }

        if (isMoving)
        {
            Vector3 movement = new Vector3(0f, 0f, 1f) * moveSpeed * Time.deltaTime;
            transform.Translate(movement);
        }
    }
}