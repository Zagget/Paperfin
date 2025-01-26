using UnityEngine;
using System.Collections;

public class Growth : MonoBehaviour
{
    [Header("Evolution")]
    [SerializeField] public int currentEvo = 1;
    [SerializeField] public int currentArt = 1;

    [Header("Grow")]
    [SerializeField] public float currentGrowth = 1;
    [SerializeField] float growFactor = 0.1f;
    [SerializeField] float growthTime = 0.5f;

    private IEnumerator growthRoutine;
    public bool evolvedTo2 = false;
    public bool evolvedTo3 = false;

    void Start()
    {

        if (currentEvo == 3)
        {
            AnimationController.Instance.PlayC01(this.gameObject);
            currentGrowth = 4.7f;
        }
        if (currentEvo == 2)
        {
            AnimationController.Instance.PlayB01(this.gameObject);
            currentGrowth = 2.7f;
        }
        if (currentEvo == 1)
        {
            currentGrowth = 1f;
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
        Grow();
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

        CheckEvo();
        CheckArt();
    }

    private void CheckEvo()
    {
        if (currentGrowth >= 4.7 && !evolvedTo3)
        {
            currentEvo = 3;
            currentArt = 1;
            evolvedTo3 = true;
            AnimationController.Instance.PlayC01(this.gameObject);
        }
        if (currentGrowth >= 2.7 && !evolvedTo2)
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
            if (currentGrowth >= 6f)
            {
                currentArt = 3;
                AnimationController.Instance.PlayC03(this.gameObject);
                return;
            }
            if (currentGrowth >= 5f)
            {
                currentArt = 2;
                AnimationController.Instance.PlayC02(this.gameObject);
            }
        }

        else if (currentEvo == 2)
        {
            if (currentGrowth >= 4f)
            {
                currentArt = 3;
                AnimationController.Instance.PlayB03(this.gameObject);
                return;
            }

            if (currentGrowth >= 3.2f)
            {
                currentArt = 2;
                AnimationController.Instance.PlayB02(this.gameObject);
            }
        }

        else if (currentEvo == 1)
        {
            if (currentGrowth >= 2f)
            {
                currentArt = 3;
                AnimationController.Instance.PlayA03(this.gameObject);
                return;
            }

            if (currentGrowth >= 1.4f)
            {
                currentArt = 2;
                AnimationController.Instance.PlayA02(this.gameObject);
            }
        }
    }
}