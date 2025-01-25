using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Growth : MonoBehaviour
{
    [Header("Grow")]
    [SerializeField] float growFactor = 0.3f;
    [SerializeField] float currentGrowth = 1;
    [SerializeField] float startingGrowth = 1;
    [SerializeField] float growthTime = 0.5f;

    private IEnumerator growthRoutine;

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
        //transform.localScale += new Vector3(growFactor, growFactor, growFactor);
        //currentGrowth = transform.localScale.x;

        growthRoutine = growSlowly(growFactor);
        StartCoroutine(growthRoutine);
    }

    private IEnumerator growSlowly(float growthAmount)
    {
        for (float t = 0f; t <= growthTime; t += Time.deltaTime)
        {
            currentGrowth += Time.deltaTime * growthAmount / growthTime;
            transform.localScale = new Vector3(Mathf.Sqrt(currentGrowth), Mathf.Sqrt(currentGrowth), Mathf.Sqrt(currentGrowth));
            yield return null;
        }
    }
}