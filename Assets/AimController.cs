using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimController : MonoBehaviour
{
    public float speedMove;
    private Vector3 moveVector;
    private CharacterController characterController;
    public float moveInputDeadZona;
    int rightFingerID;
    float halfScreenWidth;
    public float cameraSensitivity;

    private Vector2 lookInput;
    private float cameraPitch;
    public Transform Aim;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        rightFingerID = -1;
        halfScreenWidth = Screen.width / 2;
        moveInputDeadZona = Mathf.Pow(Screen.height / moveInputDeadZona, 2);
        
    }

    // Update is called once per frame
    void Update()
    {
        GetTochInput();
    }

    private void GetTochInput()
    {
        for(int i = 0; i < Input.touchCount ; i++)
        { 
            Touch t = Input.GetTouch(i);
            switch (t.phase) 
            {
                case TouchPhase.Began:
                    if(t.position.x > halfScreenWidth && rightFingerID == -1)
                    {
                        rightFingerID = t.fingerId;
                    }
                    break;

                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    if(t.fingerId == rightFingerID)
                    {
                        rightFingerID = -1;
                    }
                    break;
                case TouchPhase.Moved:
                    if(t.fingerId == rightFingerID)
                    {
                        lookInput = t.position * cameraSensitivity * Time.deltaTime;
                    }
                    break;
                case TouchPhase.Stationary:
                    if(t.fingerId == rightFingerID)
                    {
                        lookInput = Vector2.zero;

                    }
                    break;

            }
        }
    }
}
