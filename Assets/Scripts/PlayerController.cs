using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Subject
{
    [Header("Movement")]
    [SerializeField] float maxSpeed = 8;
    [SerializeField] float acceleration = 5;
    [SerializeField] float deceleration = 20;

    [Header("Dash")]
    [SerializeField] float dashCooldown = 0.5f;
    [SerializeField] float dashDuration = 1.25f;
    [SerializeField] float dashSpeed = 8f;

    float dashTime;

    float lastDashTime = 0;
    bool isDashing = false;

    Rigidbody rb;
    Vector2 velocity;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Movement();
        Dash();
    }

    private void Movement()
    {
        if (isDashing) return;

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

    private void Dash()
    {
        // Check if dash is available and the player presses the dash button
        if (Time.time - lastDashTime >= dashCooldown && Input.GetKeyDown(KeyCode.Space) && !isDashing)
        {
            NotifyObservers(Action.Dashing);
            StartDash();
        }

        if (isDashing)
        {
            dashTime += Time.deltaTime;

            if (dashTime >= dashDuration)
            {
                StopDash();
            }
        }
    }

    void StartDash()
    {
        isDashing = true;
        dashTime = 0f;

        lastDashTime = Time.time;

        Vector2 dashDirection = velocity.normalized;
        if (dashDirection.magnitude == 0) dashDirection = transform.right;

        // Apply dash speed immediately
        rb.velocity = dashDirection * dashSpeed;
    }

    void StopDash()
    {
        // Stop the dash and return to normal movement
        isDashing = false;
        velocity = rb.velocity.normalized * maxSpeed;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SeaWeed"))
        {
            Debug.Log("Player is hiding");
            NotifyObservers(Action.Hide);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Feed"))
        {
            GameManager.Instance.PlayerAte();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("SeaWeed"))
        {
            Debug.Log("Player is no longer hiding");
            NotifyObservers(Action.Normal);
        }
    }
}