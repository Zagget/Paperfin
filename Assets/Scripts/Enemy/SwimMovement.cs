using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwimMovement : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float turnSpeed;
    [SerializeField] float strokeTime;
    [SerializeField] float deceleration;

    Rigidbody2D rb;
    EnemyProperties ep;
    private float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ep = GetComponent<EnemyProperties>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!(ep.target == null) && ep.target.GetComponent<EnvironmentEffects>().isVisible)
        {
            Rigidbody2D erb = ep.target.GetComponent<Rigidbody2D>();
            Vector3 predictedTargetPosition = erb.position + (erb.position - rb.position).magnitude * erb.velocity / speed;
            Vector3 rightVector = Vector3.Cross(Vector3.forward, transform.right);

            float angularVelocity = 360f / (2f * Mathf.PI) * turnSpeed;
            //if (Vector3.Dot(rightVector, erb.position - rb.position) > ep.mouthWidth/2)
            if (Vector3.Dot(rightVector, predictedTargetPosition - (Vector3) rb.position) > ep.mouthWidth / 2)
            {
                transform.Rotate(Vector3.forward, Time.deltaTime * angularVelocity);
            }
            //else if (Vector3.Dot(rightVector, erb.position - transform.position) < -ep.mouthWidth/2)
            else if (Vector3.Dot(rightVector, predictedTargetPosition - (Vector3) rb.position) < -ep.mouthWidth / 2)
            {
                transform.Rotate(Vector3.forward, -Time.deltaTime * angularVelocity);
            }


            if (timer <= 0)
            {
                timer = strokeTime;

                rb.velocity = speed * transform.right;
            }
            timer -= Time.deltaTime;
            rb.velocity -= Time.deltaTime * rb.velocity * deceleration;
        }
    }
}
