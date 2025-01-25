using UnityEditor;
using UnityEngine;

public class PlayerAudio : MonoBehaviour, IObserver
{
    [SerializeField] Subject playerSubject;
    [SerializeField] SoundData DeathSounds;
    [SerializeField] SoundData HidingSounds;

    [SerializeField] SoundData DashShounds;


    public void OnNotify(PlayerAction action)
    {
        if (action == PlayerAction.Die)
        {
            SoundManager.Instance.PlayRandomSound(DeathSounds);
            Debug.Log("play death sound");
        }

        if (action == PlayerAction.Hide)
        {
            SoundManager.Instance.PlayRandomSound(HidingSounds);
            Debug.Log("play hide sound");
        }

        if (action == PlayerAction.Dashing)
        {
            SoundManager.Instance.PlayRandomSound(DashShounds);
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