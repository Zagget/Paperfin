
using UnityEngine;

public class CameraFollow : MonoBehaviour, IObserver
{
    [SerializeField] float smoothTime = 0.25f;
    [SerializeField] Transform target;
    [SerializeField] Subject playerSubject;

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

    public void OnNotify(PlayerAction action)
    {
        if (action == PlayerAction.Die)
        {
            followplayer = false;
        }

        if (action == PlayerAction.Eat)
        {
            //ToDO If we want camera shake etc.
        }

        if (action == PlayerAction.Hide)
        {
            //ToDO some camera effect when hiding.
        }
    }

    public void OnEnable()
    {
        playerSubject.AddObserver(this);
    }

    public void OnDisable()
    {
        playerSubject.RemoveObserver(this);
    }
}