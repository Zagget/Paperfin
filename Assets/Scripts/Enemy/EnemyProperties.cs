using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProperties : MonoBehaviour
{
    [SerializeField] public GameObject target;
    [SerializeField] public float mouthWidth;
    [SerializeField] float sightScale;
    public Behaviour behaviour;
    private Growth gr;

    // Start is called before the first frame update
    void Start()
    {
        gr = GetComponent<Growth>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int ind = 0; ind < 10; ind++)
        {
            float sightAngle = Random.Range(-180, 180);
            Vector2 rayCastDirection = new Vector2(transform.right.x * Mathf.Cos(sightAngle) - transform.right.y * Mathf.Sin(sightAngle), transform.right.x * Mathf.Sin(sightAngle) + transform.right.y * Mathf.Cos(sightAngle));
            RaycastHit2D raycastHit = Physics2D.Raycast(transform.position, rayCastDirection, sightScale * gr.currentGrowth);

            if (raycastHit)
            {
                string colliderTag = raycastHit.collider.tag;
                Debug.Log(raycastHit.collider.gameObject.name);
                if ((colliderTag == "Fish" || colliderTag == "Feed") && raycastHit.rigidbody.GetComponent<EnvironmentEffects>().isVisible)
                {
                    Growth hitGrowth = raycastHit.rigidbody.GetComponent<Growth>();
                    if (hitGrowth.currentEvo == gr.currentEvo - 1 || hitGrowth.currentEvo > gr.currentEvo)
                    {
                        if (target == null)
                        {
                            target = raycastHit.rigidbody.gameObject;
                        }
                        //else if (raycastHit.rigidbody.GetComponent<Growth>().currentGrowth > target.GetComponent<Growth>().currentGrowth)
                        else if (hitGrowth.currentEvo > target.GetComponent<Growth>().currentEvo || (hitGrowth.currentEvo == target.GetComponent<Growth>().currentEvo && (raycastHit.transform.position - transform.position).magnitude < (target.transform.position - transform.position).magnitude))
                        {
                            target = raycastHit.rigidbody.gameObject;
                        }
                    }
                }

                if (colliderTag == "Obstacle")
                {
                    //Nothing right now
                }
            }
        }

        if(target == null || !target.GetComponent<EnvironmentEffects>().isVisible || (target.transform.position - transform.position).magnitude > sightScale * gr.currentGrowth)
        {
            target = null;
            behaviour = Behaviour.NEUTRAL;
        }
        else if(target.GetComponent<Growth>().currentEvo == gr.currentEvo - 1)
        {
            behaviour = Behaviour.FOLLOWING;
        }
        else if (target.GetComponent<Growth>().currentEvo > gr.currentEvo)
        {
            behaviour = Behaviour.FLEEING;
        }
        else
        {
            behaviour = Behaviour.NEUTRAL;
        }
    }
}

public enum Behaviour
{
    NEUTRAL,
    FOLLOWING,
    FLEEING
}
