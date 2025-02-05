using Unity.VisualScripting;
using UnityEngine;

public class FishCollision : Subject
{
    Rigidbody2D rb;
    Growth grow;
    GameManager manager;

    float fishEvo;
    float fishArt;
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
            fishArt = grow.GetCurrentArt();
            float collisionEvo = otherGrowth.GetCurrentEvo();
            float collisionArt = otherGrowth.GetCurrentArt();

            if (isPlayer)
            {
                if (fishEvo > collisionEvo)
                {
                    Debug.Log("The player died");
                    AnimationController.Instance.Die(collision.gameObject);
                    manager.PlayerDied();
                    return;
                }

                if (fishEvo == collisionEvo && fishArt > collisionArt)
                {
                    Debug.Log("The player died");
                    AnimationController.Instance.Die(collision.gameObject);
                    manager.PlayerDied();
                    return;
                }

                Debug.Log("The player ate");
                otherGrowth.Grow();
                manager.PlayerAte();
                AnimationController.Instance.Die(this.gameObject);
                return;
            }


            if (fishEvo == collisionEvo && fishArt == collisionArt)
            {
                return;
            }

            if (fishEvo > collisionEvo)
            {
                grow.Grow();
                AnimationController.Instance.Die(collision.gameObject);
                return;
            }

            if (fishEvo >= collisionEvo && fishArt > collisionArt)
            {
                grow.Grow();
                AnimationController.Instance.Die(collision.gameObject);
                return;
            }

            otherGrowth.Grow();
            manager.EnemyAte(this.gameObject.transform.position);
            AnimationController.Instance.Die(this.gameObject);
        }
    }
}