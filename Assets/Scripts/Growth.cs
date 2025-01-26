using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class Growth : MonoBehaviour
{
    [Header("Evolution")]
    [SerializeField] public int currentEvo = 1;
    [SerializeField] public int currentArt = 1;

    [Header("Grow")]
    [SerializeField] public float currentGrowth = 1;
    [SerializeField] float growFactor = 0.3f;
    [SerializeField] float startingGrowth = 1;
    [SerializeField] float growthTime = 0.5f;

    private IEnumerator growthRoutine;
    //[SerializeField] float growFactor = 0.1f;
    bool evolvedTo2 = false;
    bool evolvedTo3 = false;

    void Start()
    {

        if (currentEvo == 3)
        {
            AnimationController.Instance.PlayC01(this.gameObject);
            currentGrowth = 2.5F;
        }
        if (currentEvo == 2)
        {
            AnimationController.Instance.PlayB01(this.gameObject);
            currentGrowth = 1.5F;
        }
        if (currentEvo == 1)
        {
            AnimationController.Instance.PlayA01(this.gameObject);
        }
        if (currentEvo == 0)
        {
            currentGrowth = 0.5f;
            int randomFeed = Random.Range(0, 2);
            if (randomFeed == 1)
            {
                AnimationController.Instance.PlayFeedA(this.gameObject);

            }
            else
            {
                AnimationController.Instance.PlayFeedB(this.gameObject);
            }
        }
        transform.localScale = new Vector3(currentGrowth, currentGrowth, currentGrowth);
    }

    public float GetCurrentEvo()
    {
        return currentEvo;
    }

    public float GetCurrentArt()
    {
        return currentArt;
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
        //UpdateBoxCollider();
        CheckEvo();
        CheckArt();
    }

    private void CheckEvo()
    {
        // Evo2 growth = 2. Evo3 growth 5
        if (currentGrowth >= 2.5 && !evolvedTo3)
        {
            currentEvo = 3;
            currentArt = 1;
            evolvedTo3 = true;
            AnimationController.Instance.PlayC01(this.gameObject);
        }
        if (currentGrowth >= 1.5 && !evolvedTo2)
        {
            currentEvo = 2;
            currentArt = 1;
            evolvedTo2 = true;
            AnimationController.Instance.PlayB01(this.gameObject);
        }
    }

    private void CheckArt()
    {
        if (currentEvo == 3)
        {
            if (currentGrowth >= 3.5)
            {
                currentArt = 3;
                AnimationController.Instance.PlayC03(this.gameObject);
            }
            if (currentGrowth >= 2.8)
            {
                currentArt = 2;
                AnimationController.Instance.PlayC02(this.gameObject);
            }
        }

        if (currentEvo == 2)
        {
            if (currentGrowth >= 3.8)
            {
                currentArt = 3;
                AnimationController.Instance.PlayB03(this.gameObject);
            }

            if (currentGrowth >= 3.2)
            {
                currentArt = 2;
                AnimationController.Instance.PlayB02(this.gameObject);
            }
        }

        if (currentEvo == 1)
        {
            if (currentGrowth >= 1.4)
            {
                currentArt = 3;
                AnimationController.Instance.PlayA03(this.gameObject);
            }

            if (currentGrowth >= 1.2)
            {
                currentArt = 2;
                AnimationController.Instance.PlayA02(this.gameObject);
            }
        }
    }
}