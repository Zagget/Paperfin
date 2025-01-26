using UnityEngine;

public class EnvironmentEffects : MonoBehaviour, IObserver
{
    [SerializeField] Subject playerSubject;

    public bool isVisible = true;


    public void OnNotify(Action action)
    {
        if (action == Action.Hide)
        {
            isVisible = false;
        }
        if (action == Action.Normal)
        {
            isVisible = true;
        }
    }

    private void OnEnable()
    {
        playerSubject.AddObserver(this);
    }

    private void OnDisable()
    {
        playerSubject.RemoveObserver(this);
    }
}
