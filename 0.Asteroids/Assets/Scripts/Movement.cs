using UnityEngine;
using UnityEngine.Assertions;

public class Movement : MonoBehaviour
{
    private readonly float forwardForce = 2f;
    private readonly float rotationVelocity = -300f;
    private Rigidbody2D body;
    private bool forwardInput;
    private Vector2 moveDirection = new(0, 1);
    private float rotationInput;


    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        Assert.IsNotNull(body);
    }

    private void Update()
    {
        rotationInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetButton("Vertical");
    }

    private void FixedUpdate()
    {
        body.velocity = ForwardVelocity() + RightVelocity();
        if (forwardInput) body.AddForce(moveDirection * forwardForce, ForceMode2D.Impulse);

        // TODO: calc max velocity magnitude 
        if (body.velocity.magnitude > 10f) body.velocity = body.velocity.normalized * 10f;


        body.angularVelocity = rotationInput * rotationVelocity;

        moveDirection.x = Mathf.Cos(body.rotation * Mathf.Deg2Rad);
        moveDirection.y = Mathf.Sin(body.rotation * Mathf.Deg2Rad);
    }

    private void OnDrawGizmos()
    {
        Vector2 from = transform.position;
        Vector3 to = from + moveDirection;
        Gizmos.DrawLine(from, to);
    }


    private Vector2 ForwardVelocity()
    {
        var up = transform.up;
        return up * Vector2.Dot(body.velocity, up);
    }

    private Vector2 RightVelocity()
    {
        var right = transform.right;
        return right * Vector2.Dot(body.velocity, right);
    }
}