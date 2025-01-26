using UnityEngine;
using System.Collections;

public class Growth : MonoBehaviour
{
    [Header("Evolution")]
    [SerializeField] float currentEvo = 1;

    [Header("Grow")]
    [SerializeField] float currentGrowth = 1;
    [SerializeField] float growFactor = 0.3f;

    void Start()
    {
        float startSize = 1f;
        if (currentEvo == 3)
        {
            AnimationController.Instance.PlayEvo3(this.gameObject);
            startSize = 3;
        }
        if (currentEvo == 2)
        {
            AnimationController.Instance.PlayEvo2(this.gameObject);
            startSize = 2;
        }
        if (currentEvo == 1)
        {
            AnimationController.Instance.PlayEvo1(this.gameObject);
        }
        if (currentEvo == 0)
        {
            startSize = 0.5f;
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
        transform.localScale = new Vector3(startSize, startSize, startSize);
    }

    public float GetCurrentEvo()
    {
        return currentEvo;
    }

    public void Grow()
    {
        transform.localScale += new Vector3(growFactor, growFactor, growFactor);
        currentGrowth = transform.localScale.x;
        //UpdateBoxCollider();
        CheckEvo();
    }

    private void CheckEvo()
    {
        // Evo2 growth = 2. Evo3 growth 5
        if (currentGrowth >= 3)
        {
            currentEvo = 3;
            AnimationController.Instance.PlayEvo3(this.gameObject);
        }
        if (currentGrowth >= 2)
        {
            currentEvo = 2;
            AnimationController.Instance.PlayEvo2(this.gameObject);
        }
    }
}