using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcccelerationMovement : MonoBehaviour
{
    [SerializeField] float maxAcc;
    [SerializeField] float pComponent;
    [SerializeField] float vComponent;
    [SerializeField] float deceleration;

    Rigidbody rb;
    EnemyProperties ep;
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
            Vector3 deltaP = rb.position - erb.position;
            Vector3 deltaV = rb.velocity - erb.velocity;

            Vector3 targetAcc = -pComponent * deltaP - vComponent * deltaV + deceleration * rb.velocity;
            //Vector3 boundedAcc = Vector3.ClampMagnitude(targetAcc, maxAcc);
            Vector3 boundedAcc = targetAcc.normalized * maxAcc;

            rb.velocity += Time.deltaTime * (boundedAcc - deceleration * rb.velocity);
        }
    }
}
