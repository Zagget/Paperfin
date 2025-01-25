using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CruiseMovementMissile : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float maxTurn;
    [SerializeField] float turnVariation;
    [SerializeField] float turnPullBack;
    private float prevTurn = 0;

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
        if (!ep.isFollowing)
        {
            prevTurn += Mathf.Clamp(Mathf.Sqrt(Time.deltaTime) * Random.Range(-turnVariation, turnVariation) - Time.deltaTime * prevTurn * turnPullBack, -maxTurn, maxTurn);
            transform.Rotate(Vector3.forward, -Time.deltaTime * prevTurn * 360 / (2 * Mathf.PI));
            rb.velocity = speed * transform.right;
        }
    }
}
