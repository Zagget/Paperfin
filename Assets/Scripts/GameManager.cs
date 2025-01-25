using UnityEngine;
using System.Collections;

public class GameManager : Subject
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    [SerializeField] int amountFishAte = 0;
    [SerializeField] SoundData enemyEating;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void PlayerAte()
    {
        NotifyObservers(Action.Eat);
        amountFishAte++;
    }

    public void PlayerDied()
    {
        NotifyObservers(Action.Die);
    }

    public void FishEvolve(GameObject fish)
    {
        NotifyObservers(Action.Evolve);
    }

    public void CheckEvolve(GameObject g)
    {

    }

    public void EnemyAte(Vector3 enemyPos)
    {
        GameObject audioObject = new GameObject("AudioSourceEnemyAte");
        audioObject.transform.position = enemyPos;
        AudioSource audioSource = audioObject.AddComponent<AudioSource>();
        audioSource.spatialBlend = 1f;

        SoundManager.Instance.PlayRandomSoundAtLocation(enemyEating, audioSource);
        StartCoroutine(DestroyAudio(audioObject, audioSource));
    }

    private IEnumerator DestroyAudio(GameObject g, AudioSource source)
    {
        while (source.isPlaying)
        {
            yield return null;  // Wait for the next frame
        }
        Destroy(g);
    }
}