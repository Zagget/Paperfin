using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float maxSpeed = 8;
    public float acceleration = 5;
    public float deceleration = 20;

    Rigidbody rb;

    Vector2 velocity;
    bool isGrounded = false;


    void Start()
    {
        rb = GetComponent<Rigidbody>();

        Physics2D.queriesStartInColliders = false;

    }

    void Update()
    {
        AdjustGravity();
        Movement();
    }

    private void AdjustGravity()
    {
        //ToDO Downward force
    }

    private void Movement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        velocity.x += x * acceleration * Time.fixedDeltaTime;
        velocity.y += y * acceleration * Time.fixedDeltaTime;

        velocity.x = Mathf.Clamp(velocity.x, -maxSpeed, maxSpeed);
        velocity.y = Mathf.Clamp(velocity.y, -maxSpeed, maxSpeed);

        // if (x == 0 || (x < 0 == xVelocity > 0))
        // {
        //     xVelocity *= 1 - (deceleration * Time.fixedDeltaTime);
        // }

        rb.velocity = new Vector2(velocity.x, velocity.y);
    }
}