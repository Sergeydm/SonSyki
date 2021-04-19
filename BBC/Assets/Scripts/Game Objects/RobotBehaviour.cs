using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotBehaviour : MonoBehaviour
{
    public float currentMoveSpeed;
    public float currentRotateSpeed;
    public float moveSpeed = 3f;
    public float rotateSpeed = 3f;
    public float freezeSpeed = 0f;

    void FixedUpdate()
    {
        var deltaX = Input.GetAxis("Horizontal");
        var deltaZ = Input.GetAxis("Vertical");
        transform.Rotate(0f, deltaX * currentRotateSpeed, 0f);
        if (Input.GetKey(KeyCode.LeftShift))
            transform.Translate(new Vector3(0f, 0f, -deltaZ * currentMoveSpeed * 2 * Time.deltaTime));
        else transform.Translate(new Vector3(0f, 0f, -deltaZ * currentMoveSpeed * Time.deltaTime));   
    }

    private void Start()
    {
        currentMoveSpeed = moveSpeed;
        currentRotateSpeed = rotateSpeed;
    }
}
