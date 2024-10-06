using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DivingMovement : MonoBehaviour
{
    public float moveSpeed;
    public float rotationSpeed;
    public float maxRotationZ;
    public Transform container;
    private float currentRotationZ = 0f;
    private int direction = 1;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        if(horizontalInput < 0)
        {
            direction = -1;
        }
        if (horizontalInput > 0)
        {
            direction = 1;
        }

        Vector3 _dir = (direction == 1) ? Vector3.right : Vector3.left;

        container.transform.Translate(_dir * moveSpeed * Time.deltaTime);
        currentRotationZ = Mathf.Clamp(currentRotationZ + direction * verticalInput * rotationSpeed * Time.deltaTime, -maxRotationZ, maxRotationZ);
        container.transform.localRotation = Quaternion.Euler(0, 0, currentRotationZ);
        container.transform.localScale = (direction == 1) ?Vector3.one : new Vector3(-1, 1, 1);

    }

}
