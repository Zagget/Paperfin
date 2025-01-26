using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CruiseMovement : MonoBehaviour
{
    [SerializeField] float maxAcc;
    [SerializeField] float accVariation;
    [SerializeField] float deceleration;
    private Vector2 prevAcc = Vector2.zero;

    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        prevAcc += Vector2.ClampMagnitude((Mathf.Sqrt(Time.deltaTime) * Random.insideUnitCircle) * accVariation, maxAcc);
        rb.velocity += Time.deltaTime * prevAcc - Time.deltaTime * rb.velocity * deceleration;
    }
}
