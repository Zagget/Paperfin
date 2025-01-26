
using UnityEngine;

public class FeedCollision : Subject
{
    Rigidbody2D rb;
    Growth grow;
    GameManager manager;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        grow = GetComponent<Growth>();

        manager = FindFirstObjectByType<GameManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Fish"))
        {
            Debug.Log("Feed Collided with another fish");

            Growth otherGrowth = collision.gameObject.GetComponent<Growth>();
            if (otherGrowth == null)
            {
                Debug.LogWarning("Collided object has no Growth component!");
                return;
            }

            bool isPlayer = false;
            if (collision.gameObject.GetComponent<PlayerController>() != null)
            {
                isPlayer = true;
            }


            float collisionEvo = otherGrowth.GetCurrentEvo();

            if (isPlayer)
            {
                if (collisionEvo > 1)
                {
                    Debug.Log("Cant eat feed anymore");
                    return;
                }

                if (collisionEvo == 1)
                {
                    Debug.Log("PLayer ate feed");
                    Destroy(this.gameObject);
                    manager.PlayerAte();
                    otherGrowth.Grow();
                }
            }
        }
    }
}