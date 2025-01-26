
using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour, IObserver
{
    [Header("Following Player")]
    [SerializeField] float smoothTime = 0.25f;
    [SerializeField] Transform target;
    [SerializeField] Subject ManagerSubject;

    [Header("Increase FOV when eating")]
    [SerializeField] float fovIncreaseFactor = 1.2f;
    [SerializeField] float fovSmoothTime = 0.5f;
    [SerializeField] float targetFOV = 60;

    float minFOV = 60;
    float maxFOV = 110;

    float currentFOVVelocity = 0;
    Vector3 offset = new Vector3(0f, 0f, -10f);
    Vector3 velocity = Vector3.zero;
    Camera cam;

    bool followplayer = true;

    void Awake()
    {
        cam = GetComponent<Camera>();
        if (cam == null)
        {
            Debug.LogError("CameraFollow script must be attached to a GameObject with a Camera component.");
        }
    }

    void FixedUpdate()
    {
        if (followplayer)
        {
            Vector3 targetPosition = target.position + offset;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }

        cam.fieldOfView = Mathf.SmoothDamp(cam.fieldOfView, targetFOV, ref currentFOVVelocity, fovSmoothTime);
    }

    public void IncreaseFOV(float playerScale)
    {
        targetFOV = 60 * (1 + (playerScale - 1) * fovIncreaseFactor);
        targetFOV = Mathf.Clamp(targetFOV, minFOV, maxFOV);
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
            float playerScale = target.localScale.x; // Assuming uniform scaling.
            IncreaseFOV(playerScale);
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