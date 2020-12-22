using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowmanBehaviour : MonoBehaviour
{
    public Vector2 speed = new Vector2(10, 10);
    private Vector2 movement;

    void Update()
    {
        var inputX = Input.GetAxis("Horizontal");
        var inputY = Input.GetAxis("Vertical");
        movement = new Vector2(speed.x * inputX, speed.y * inputY);
    }

    private void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().velocity = movement;
    }
}

