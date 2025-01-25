
using UnityEngine;

public class CameraFollow : MonoBehaviour, IObserver
{
    [SerializeField] float smoothTime = 0.25f;
    [SerializeField] Transform target;
    [SerializeField] Subject ManagerSubject;

    Vector3 offset = new Vector3(0f, 0f, -10f);
    Vector3 velocity = Vector3.zero;

    bool followplayer = true;

    void FixedUpdate()
    {
        if (followplayer)
        {
            Vector3 targetPosition = target.position + offset;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
    }

    public void OnNotify(Action action)
    {
        if (action == Action.Die)
        {
            Debug.Log("Player died,");
            followplayer = false;
        }

        if (action == Action.Eat)
        {
            //ToDO If we want camera shake etc.
        }

        if (action == Action.Hide)
        {
            //ToDO some camera effect when hiding.
        }
    }

    public void OnEnable()
    {
        ManagerSubject.AddObserver(this);
    }

    public void OnDisable()
    {
        ManagerSubject.RemoveObserver(this);
    }
}