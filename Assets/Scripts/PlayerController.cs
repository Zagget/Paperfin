using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Subject
{
    [Header("Movement")]
    public float maxSpeed = 8;
    public float acceleration = 5;
    public float deceleration = 20;

    Rigidbody rb;
    Vector2 velocity;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Movement();
    }


    private void Movement()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (input.magnitude > 0)
        {
            velocity += input.normalized * acceleration * Time.fixedDeltaTime;
        }
        else
        {
            // Apply deceleration if there's no input
            float decelerationAmount = deceleration * Time.fixedDeltaTime;
            if (velocity.magnitude > decelerationAmount)
            {
                velocity = velocity.normalized * (velocity.magnitude - decelerationAmount);
            }
            else
            {
                velocity = Vector2.zero;
            }
        }

        velocity += input * acceleration * Time.fixedDeltaTime;

        velocity = Vector2.ClampMagnitude(velocity, maxSpeed);

        rb.velocity = new Vector2(velocity.x, velocity.y);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SeaWeed"))
        {
            Debug.Log("Player is hiding");
            NotifyObservers(PlayerAction.Hide);
        }

        // if (other.CompareTag("enemy"))
        // {
        //     NotifyObservers(PlayerAction.Eat);
        // }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("SeaWeed"))
        {
            Debug.Log("Player is no longer hiding");
            NotifyObservers(PlayerAction.Normal);
        }
    }
}