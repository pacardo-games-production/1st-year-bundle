using System;
using UnityEngine;
using UnityEngine.Assertions;

public class Movement : MonoBehaviour
{
    [SerializeField] private float maxSpeed = 10;
    [SerializeField] private float forceCoefficient = 1.3f;
    [SerializeField] private float maxSpeedCoefficient = 1.3f;
    [SerializeField] private float revSpeed = 3;
    private Rigidbody2D rb2d;
    private Vector2 moveDirection = new Vector2(0, 1);

    private float thrustInput = 0;
    private float rotationInput;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        Assert.IsNotNull(rb2d);
    }

    private void Update()
    {
        rotationInput = Input.GetAxis("Horizontal");
        thrustInput = Input.GetAxis("Vertical");
    }

    private void OnDrawGizmos()
    {
        Vector3 from = transform.position;
        Vector3 to = (Vector2)from + moveDirection;
        Gizmos.DrawLine(from, to);
    }

    private void FixedUpdate()
    {
        MovementHandling();
    }

    private void MovementHandling()
    {
        if (rotationInput > 0 || rotationInput < 0)
        {
            rb2d.MoveRotation(rb2d.rotation + (-revSpeed) * rotationInput * Time.fixedDeltaTime);
            moveDirection.x = Mathf.Cos(rb2d.rotation * Mathf.Deg2Rad);
            moveDirection.y = Mathf.Sin(rb2d.rotation * Mathf.Deg2Rad);
        }


        if (thrustInput > 0 || thrustInput < 0)
        {
            rb2d.AddForce(moveDirection * forceCoefficient * thrustInput, ForceMode2D.Force);
        }

        // max velocity limitation
        var exceedingSpeedLimit = rb2d.velocity.magnitude - maxSpeed;
        // Debug.Log(rb2d.velocity.magnitude.ToString());
        if (exceedingSpeedLimit > 0)
        {
            rb2d.AddForce(-rb2d.velocity * exceedingSpeedLimit * maxSpeedCoefficient, ForceMode2D.Force);
        }
    }
}