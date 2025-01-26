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
    private float timer = 1f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ep = GetComponent<EnemyProperties>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (!(ep.target == null))
        {
            Rigidbody2D erb = ep.target.GetComponent<Rigidbody2D>();
            Vector3 predictedTargetPosition = erb.position + (erb.position - rb.position).magnitude * erb.velocity / speed;
            Vector3 rightVector = Vector3.Cross(Vector3.forward, -transform.right);
            float angularVelocity = 360f / (2f * Mathf.PI) * turnSpeed;

            bool isInFront = Vector3.Dot(-transform.right, predictedTargetPosition) > 0;

            if (ep.behaviour == Behaviour.FOLLOWING)
            {
                float projVal = Vector3.Dot(rightVector, predictedTargetPosition - (Vector3)rb.position);
                //if (Vector3.Dot(rightVector, erb.position - rb.position) > ep.mouthWidth/2)

                if (projVal > ep.mouthWidth / 2)
                {
                    transform.Rotate(Vector3.forward, Time.deltaTime * angularVelocity);
                }
                //else if (Vector3.Dot(rightVector, erb.position - transform.position) < -ep.mouthWidth/2)
                else if (projVal < -ep.mouthWidth / 2)
                {
                    transform.Rotate(Vector3.forward, -Time.deltaTime * angularVelocity);
                }
            }
            else if (ep.behaviour == Behaviour.FLEEING)
            {
                float projVal = Vector3.Dot(rightVector, (Vector3) erb.position - (Vector3)rb.position);
                if (projVal > ep.mouthWidth / 2 || (projVal > 0 && isInFront))
                {
                    transform.Rotate(Vector3.forward, -Time.deltaTime * angularVelocity);
                }
                //else if (Vector3.Dot(rightVector, erb.position - transform.position) < -ep.mouthWidth/2)
                else if (projVal < -ep.mouthWidth / 2 || (projVal < 0 && isInFront))
                {
                    transform.Rotate(Vector3.forward, Time.deltaTime * angularVelocity);
                }

            }

            if (timer <= 0)
            {
                timer = strokeTime;

                rb.velocity = speed * (-transform.right);
            }
            timer -= Time.deltaTime;
        }
        rb.velocity -= Time.deltaTime * rb.velocity * deceleration;
    }
}
