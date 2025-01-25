using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Growth : MonoBehaviour
{
    [Header("Grow")]
    [SerializeField] float growFactor = 0.3f;
    [SerializeField] float currentGrowth = 1;
    [SerializeField] float startingGrowth = 1;

    void Start()
    {
        transform.localScale = Vector3.one * startingGrowth;
    }

    public float GetCurrentGrowth()
    {
        return currentGrowth;
    }

    public void Grow()
    {
        transform.localScale += new Vector3(growFactor, growFactor, growFactor);
        currentGrowth = transform.localScale.x;
    }
}