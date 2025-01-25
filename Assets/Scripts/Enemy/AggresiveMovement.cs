using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aggresiveMovement : MonoBehaviour
{
    [SerializeField] float maxAcc;
    [SerializeField] float deceleration;
    private float prevT = 1f;

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

            float gamma = deceleration;
            float t = prevT;
            Vector3 acc = Vector3.zero;
            for (int ind = 0; ind < 10; ind++)
            {
                Vector3 relAccFactor = (-gamma * deltaP - (1 - Mathf.Exp(-gamma * t)) * deltaV) / (gamma * t - (1 - Mathf.Exp(-gamma * t)));
                acc = gamma * (relAccFactor + erb.velocity);
                if (acc.magnitude < maxAcc)
                {
                    t -= Time.deltaTime;
                }
                else
                {
                    t += Time.deltaTime;
                }
            }
            rb.velocity += Time.deltaTime * (acc - deceleration * rb.velocity);
        }
    }
}
