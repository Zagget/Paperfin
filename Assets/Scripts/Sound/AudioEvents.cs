using UnityEditor;
using UnityEngine;

public class AudioEvents : MonoBehaviour, IObserver
{
    [SerializeField] Subject playerSubject;
    [SerializeField] Subject gameManager;
    [SerializeField] SoundData DeathSounds;
    [SerializeField] SoundData HidingSounds;
    [SerializeField] SoundData DashSounds;
    [SerializeField] SoundData EatingSounds;


    public void OnNotify(Action action)
    {
        if (action == Action.Die)
        {
            SoundManager.Instance.PlayRandomSound(DeathSounds);
            Debug.Log("play death sound");
        }

        if (action == Action.Eat)
        {
            SoundManager.Instance.PlayRandomSound(EatingSounds);
            Debug.Log("play eating sound");
        }

        if (action == Action.Hide)
        {
            SoundManager.Instance.PlayRandomSound(HidingSounds);
            Debug.Log("play hide sound");
        }

        if (action == Action.Dashing)
        {
            SoundManager.Instance.PlayRandomSound(DashSounds);
        }
    }

    private void OnEnable()
    {
        playerSubject.AddObserver(this);
        gameManager.AddObserver(this);
    }

    private void OnDisable()
    {
        playerSubject.RemoveObserver(this);
        gameManager.RemoveObserver(this);
    }
}