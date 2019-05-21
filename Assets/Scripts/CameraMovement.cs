using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    float moveSpeed = 1;

    Vector3 forward, right, movementVector;

    private void Start()
    {
        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;
    }
    private void Update()
    {
        moveCamera();
    }

    private void moveCamera()
    {
        if (Input.mousePosition.y > Screen.height * 0.95f)
            movementVector += forward * moveSpeed * Time.deltaTime;
        else if (Input.mousePosition.y < Screen.height * 0.05f)
            movementVector -= forward * moveSpeed * Time.deltaTime;

        else if (Input.mousePosition.x > Screen.width * 0.95f)
            movementVector += right * moveSpeed * Time.deltaTime ;
        else if (Input.mousePosition.x < Screen.height * 0.05f)
            movementVector -= right * moveSpeed * Time.deltaTime ;

        gameObject.transform.position = movementVector;

    }
}
