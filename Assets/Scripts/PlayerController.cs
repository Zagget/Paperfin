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
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        velocity += input * acceleration * Time.fixedDeltaTime;


        // ToDo player comes to halt when velocity is close to 0.
        // if (x == 0 || (x < 0 == xVelocity > 0))
        // {
        //     xVelocity *= 1 - (deceleration * Time.fixedDeltaTime);
        // }

        velocity = Vector2.ClampMagnitude(velocity, maxSpeed);

        rb.velocity = new Vector2(velocity.x, velocity.y);
    }
}