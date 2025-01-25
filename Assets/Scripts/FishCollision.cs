using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishCollision : Subject
{
    Rigidbody rb;
    Growth grow;

    float currentGrow;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        grow = GetComponent<Growth>();

        currentGrow = grow.GetCurrentGrowth();
    }

    private void OnCollisionEnter(Collision collision)
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

            currentGrow = grow.GetCurrentGrowth();
            float otherGrow = otherGrowth.GetCurrentGrowth();

            if (currentGrow > otherGrow)
            {
                grow.Grow();

                if (isPlayer)
                {
                    Debug.Log("The player died");
                    NotifyObservers(PlayerAction.Die);
                }

                Destroy(collision.gameObject);

            }
            else
            {
                if (isPlayer)
                {
                    Debug.Log("The player ate");
                    NotifyObservers(PlayerAction.Eat);
                }
                otherGrowth.Grow();
                Destroy(this.gameObject);
            }
        }
    }
}