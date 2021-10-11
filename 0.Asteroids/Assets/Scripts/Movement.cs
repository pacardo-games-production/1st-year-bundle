using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Movement : MonoBehaviour
{
    [SerializeField] private Vector2 maxVelocity = new Vector2(5, 5);
    private Rigidbody2D rb2d;
    private float moveX = 0;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        Assert.IsNotNull(rb2d);
    }

    void Update()
    {
        moveX = Input.GetAxis("Horizontal");
    }

    private void FixedUpdate()
    {
        if (moveX > 0)
        {
            rb2d.AddForce(new Vector2(1f, 0), ForceMode2D.Impulse);
        }
        else if (moveX < 0)
        {
            rb2d.AddForce(new Vector2(-1f, 0), ForceMode2D.Impulse);
        }


        var objectVelocity = rb2d.velocity;

        if (objectVelocity.x > maxVelocity.x)
        {
            objectVelocity.x = maxVelocity.x;
        }

        if (objectVelocity.x < -maxVelocity.x)
        {
            objectVelocity.x = -maxVelocity.x;
        }

        rb2d.velocity = objectVelocity;
    }
}