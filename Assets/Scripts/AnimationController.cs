using UnityEngine;
using System.Collections;

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
        StartCoroutine(UpdateColliderAfterDelay(fish));
    }
    public void PlayEvo2(GameObject fish)
    {
        Animator animator = fish.GetComponent<Animator>();
        animator.Play("b_01_a");
        StartCoroutine(UpdateColliderAfterDelay(fish));
    }
    public void PlayEvo3(GameObject fish)
    {
        Animator animator = fish.GetComponent<Animator>();
        animator.Play("c_01_a");
        StartCoroutine(UpdateColliderAfterDelay(fish));
    }

    public void PlayFeedA(GameObject fish)
    {
        Animator animator = fish.GetComponent<Animator>();
        animator.Play("feed_a");
        StartCoroutine(UpdateColliderAfterDelay(fish));
    }
    public void PlayFeedB(GameObject fish)
    {
        Animator animator = fish.GetComponent<Animator>();
        animator.Play("feed_b");
        StartCoroutine(UpdateColliderAfterDelay(fish));
    }

    private IEnumerator UpdateColliderAfterDelay(GameObject fish)
    {
        // Wait for one frame to ensure that the animation or sprite has updated.
        yield return new WaitForSeconds(1);

        // Now call UpdateCollider after the delay
        UpdateCollider(fish);
    }

    private void Update()
    {
        GameObject fish = GameObject.Find("Player");

        if (Input.GetKeyDown(KeyCode.B))
        {
            UpdateCollider(fish);
        }
    }

    private void UpdateCollider(GameObject fish)
    {
        SpriteRenderer sr = fish.GetComponent<SpriteRenderer>();
        BoxCollider2D collider = fish.GetComponent<BoxCollider2D>();

        if (sr == null || collider == null)
        {
            Debug.LogError("SpriteRenderer or BoxCollider2D null");
            return;
        }

        Bounds spriteBounds = sr.bounds;

        Vector3 scale = fish.transform.localScale;

        collider.size = new Vector2(spriteBounds.size.x / scale.x, spriteBounds.size.y / scale.y);
        collider.offset = new Vector2(spriteBounds.center.x - fish.transform.position.x, spriteBounds.center.y - fish.transform.position.y);
    }
}