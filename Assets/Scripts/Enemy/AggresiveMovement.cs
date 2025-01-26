using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aggresiveMovement : MonoBehaviour
{
    [SerializeField] float maxAcc;
    [SerializeField] float deceleration;
    private float prevT = 1f;

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

            float gamma = deceleration;
            float t = prevT;
            Vector2 acc = Vector2.zero;
            for (int ind = 0; ind < 10; ind++)
            {
                Vector2 relAccFactor = (-gamma * deltaP - (1 - Mathf.Exp(-gamma * t)) * deltaV) / (gamma * t - (1 - Mathf.Exp(-gamma * t)));
                acc = gamma * (relAccFactor + erb.velocity);
                if (acc.magnitude < maxAcc)
                {
                    t -= Time.deltaTime;
                }
                else
                {
                    t += Time.deltaTime;
                }
                Debug.Log("Acc: " + acc.magnitude);
                prevT = t;
            }
            rb.velocity += Time.deltaTime * (acc.normalized * maxAcc - deceleration * rb.velocity);

            transform.LookAt(transform.position + Vector3.forward, Vector3.Cross(rb.velocity, Vector3.forward));
        }
    }
}
