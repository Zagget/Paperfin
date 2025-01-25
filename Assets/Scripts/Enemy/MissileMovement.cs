using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileMovement : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float turnRadius;

    Rigidbody rb;
    EnemyProperties ep;
    private bool isTurning = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ep = GetComponent<EnemyProperties>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!(ep.target == null) && ep.target.GetComponent<EnvironmentEffects>().isVisible)
        {
            Rigidbody erb = ep.target.GetComponent<Rigidbody>();
            Vector3 predictedTargetPosition = erb.position + (erb.position - rb.position).magnitude * erb.velocity / speed;
            Vector3 rightVector = Vector3.Cross(Vector3.forward, transform.right);

            float angularVelocity = 360f / (2f * Mathf.PI) * speed / turnRadius;
            //if (Vector3.Dot(rightVector, erb.position - rb.position) > ep.mouthWidth/2)
            if (!isTurning)
            {
                isTurning = ((erb.position - rb.position - turnRadius * rightVector).magnitude > turnRadius && (erb.position - rb.position + turnRadius * rightVector).magnitude > turnRadius);
            }
            else
            {
                isTurning = ((erb.position - rb.position - turnRadius * rightVector).magnitude > (turnRadius - ep.mouthWidth/2) && (erb.position - rb.position + turnRadius * rightVector).magnitude > (turnRadius - ep.mouthWidth/2));
                if (Vector3.Dot(rightVector, predictedTargetPosition - rb.position) > ep.mouthWidth / 2)
                {
                    transform.Rotate(Vector3.forward, Time.deltaTime * angularVelocity);
                }
                //else if (Vector3.Dot(rightVector, erb.position - transform.position) < -ep.mouthWidth/2)
                else if (Vector3.Dot(rightVector, predictedTargetPosition - rb.position) < -ep.mouthWidth / 2)
                {
                    transform.Rotate(Vector3.forward, -Time.deltaTime * angularVelocity);
                }
            }
        }
        rb.velocity = speed * transform.right;
    }
}
