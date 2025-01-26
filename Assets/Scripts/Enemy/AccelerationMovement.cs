using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcccelerationMovement : MonoBehaviour
{
    [SerializeField] float maxAcc;
    [SerializeField] float pComponent;
    [SerializeField] float vComponent;
    [SerializeField] float deceleration;

    Rigidbody2D rb;
    EnemyProperties ep;
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
            Vector2 deltaP = rb.position - erb.position;
            Vector2 deltaV = rb.velocity - erb.velocity;

            Vector2 targetAcc = -pComponent * deltaP - vComponent * deltaV + deceleration * rb.velocity;
            //Vector3 boundedAcc = Vector3.ClampMagnitude(targetAcc, maxAcc);
            Vector2 boundedAcc = targetAcc.normalized * maxAcc;

            rb.velocity += Time.deltaTime * (boundedAcc - deceleration * rb.velocity);

            transform.LookAt(transform.position + Vector3.forward, Vector3.Cross(rb.velocity, Vector3.forward));
        }
    }
}
