using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private float moveSpeed = 0f;
    private bool isMoving = false;
    private Animator animator;

    private bool firstTouch = true; // ���� ��� ������������ ������� �������

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.touchCount > 0 && firstTouch||Input.GetMouseButton(0))
        {
            // ��� ������ ������� �������� ��������
            isMoving = true;
            moveSpeed = 0f;
            animator.SetBool("IsWalking", true); // ���������� �������� ������
            firstTouch = false; // ��������� ���� ������� �������
        }

        if (isMoving)
        {
            Vector3 movement = new Vector3(0f, 0f, 1f) * moveSpeed * Time.deltaTime;
            transform.Translate(movement);
        }
    }
}