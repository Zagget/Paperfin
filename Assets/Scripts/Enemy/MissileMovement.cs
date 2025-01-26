using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileMovement : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float turnRadius;

    Rigidbody2D rb;
    EnemyProperties ep;
    private bool isTurning = false;

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
            Vector3 rightVector = Vector3.Cross(Vector3.forward, transform.right);

            float angularVelocity = 360f / (2f * Mathf.PI) * speed / turnRadius;

            if (ep.behaviour == Behaviour.FOLLOWING)
            {
                //if (Vector3.Dot(rightVector, erb.position - rb.position) > ep.mouthWidth/2)
                if (!isTurning)
                {
                    isTurning = (((Vector3)erb.position - (Vector3)rb.position - turnRadius * rightVector).magnitude > turnRadius && ((Vector3)erb.position - (Vector3)rb.position + turnRadius * rightVector).magnitude > turnRadius);
                }
                else
                {
                    isTurning = (((Vector3)erb.position - (Vector3)rb.position - turnRadius * rightVector).magnitude > (turnRadius - ep.mouthWidth / 2) && ((Vector3)erb.position - (Vector3)rb.position + turnRadius * rightVector).magnitude > (turnRadius - ep.mouthWidth / 2));
                    if (Vector3.Dot(rightVector, predictedTargetPosition - (Vector3)rb.position) > ep.mouthWidth / 2)
                    {
                        transform.Rotate(Vector3.forward, Time.deltaTime * angularVelocity);
                    }
                    //else if (Vector3.Dot(rightVector, erb.position - transform.position) < -ep.mouthWidth/2)
                    else if (Vector3.Dot(rightVector, predictedTargetPosition - (Vector3)rb.position) < -ep.mouthWidth / 2)
                    {
                        transform.Rotate(Vector3.forward, -Time.deltaTime * angularVelocity);
                    }
                }
            }
            else if (ep.behaviour == Behaviour.FLEEING)
            {
                float projVal = Vector3.Dot(rightVector, predictedTargetPosition - (Vector3)rb.position);
                bool isInFront = Vector3.Dot(transform.right, predictedTargetPosition) > 0;
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
        }
        rb.velocity = speed * transform.right;
    }
}
