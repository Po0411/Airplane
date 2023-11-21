using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateInput();
        UpdateMouse();
    }

    void UpdateInput()
    {
        Vector3 moveDirection = Vector3.zero;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            moveDirection.y = 1;
            //Debug.Log("위");
        }

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            moveDirection.y = -1;
            //Debug.Log("아래");
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            moveDirection.x = -1;
            //Debug.Log("왼쪽");
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            moveDirection.x = 1;
            //Debug.Log("오른쪽");
        }

        SystemManager.Instance.Hero.ProcessInput(moveDirection);
    }

    void UpdateMouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SystemManager.Instance.Hero.Fire();
        }
    }
}
