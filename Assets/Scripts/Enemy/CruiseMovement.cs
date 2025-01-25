using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CruiseMovement : MonoBehaviour
{
    [SerializeField] float maxAcc;
    [SerializeField] float accVariation;
    [SerializeField] float deceleration;
    private Vector3 prevAcc = Vector3.zero;

    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        prevAcc += Vector3.ClampMagnitude((Vector3)(Mathf.Sqrt(Time.deltaTime) * Random.insideUnitCircle) * accVariation, maxAcc);
        rb.velocity += Time.deltaTime * prevAcc - Time.deltaTime * rb.velocity * deceleration;
    }
}
