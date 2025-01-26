using UnityEngine;

public class FishCollision : Subject
{
    Rigidbody2D rb;
    Growth grow;
    GameManager manager;

    float fishEvo;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        grow = GetComponent<Growth>();

        fishEvo = grow.GetCurrentEvo();
        manager = FindFirstObjectByType<GameManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Fish"))
        {
            Debug.Log("Collided with another fish");

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

            fishEvo = grow.GetCurrentEvo();
            float collisionEvo = otherGrowth.GetCurrentEvo();

            if (isPlayer)
            {
                if (fishEvo >= collisionEvo)
                {
                    Debug.Log("The player died");
                    manager.PlayerDied();
                    Destroy(collision.gameObject);
                    return;
                }
                Debug.Log("The player ate");
                otherGrowth.Grow();
                manager.PlayerAte();
                Destroy(this.gameObject);
                return;
            }
            if (fishEvo > collisionEvo)
            {
                grow.Grow();
                Destroy(collision.gameObject);
            }
            else
            {
                otherGrowth.Grow();
                manager.EnemyAte(this.gameObject.transform.position);
                Destroy(this.gameObject);
            }
        }
    }
}