using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private static AnimationController instance;
    public static AnimationController Instance { get { return instance; } }


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void PlayEvo1(GameObject fish)
    {
        Animator animator = fish.GetComponent<Animator>();
        animator.Play("a_01_a");
    }
    public void PlayEvo2(GameObject fish)
    {
        Animator animator = fish.GetComponent<Animator>();
        animator.Play("b_01_a");
    }
    public void PlayEvo3(GameObject fish)
    {
        Animator animator = fish.GetComponent<Animator>();
        animator.Play("c_01_a");
    }

    public void PlayFeedA(GameObject fish)
    {
        Animator animator = fish.GetComponent<Animator>();
        animator.Play("feed_a");
    }
    public void PlayFeedB(GameObject fish)
    {
        Animator animator = fish.GetComponent<Animator>();
        animator.Play("feed_b");
    }
}