using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;
    public static SoundManager Instance { get { return instance; } }

    [Header("Volume Control")]
    [SerializeField] AudioMixer mixer;
    [SerializeField]
    [Range(0.0001f, 1f)]
    public float player = 1.0f;

    [SerializeField]
    [Range(0.0001f, 1f)]
    public float background = 1.0f;

    [SerializeField]
    [Range(0.0001f, 1f)]
    public float misc = 1.0f;

    [Header("Ambient")]
    [SerializeField] SoundData ambienceSound;

    AudioSource ambienceSource
    ;
    // Audiosources so no sound gets cut off.
    private int currentAudioSourceIndex = 0;
    private List<AudioSource> audioSources;

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

        ambienceSource = gameObject.AddComponent<AudioSource>();
    }

    void Start()
    {
        audioSources = new List<AudioSource>();
        for (int i = 0; i < 5; i++)
        {
            AudioSource source = gameObject.AddComponent<AudioSource>();
            audioSources.Add(source);
        }

        SetAudioMixerVolume();
        PlayLoop(ambienceSound, "Ambience", ambienceSource);
    }

    private void SetAudioMixerVolume()
    {
        mixer.SetFloat("playerVolume", Mathf.Log10(player) * 20);
        mixer.SetFloat("backgroundVolume", Mathf.Log10(background) * 20);
        mixer.SetFloat("miscVolume", Mathf.Log10(misc) * 20);
    }


    public void PlayRandomSound(SoundData soundData)
    {
        bool empty = CheckIfSoundDataEmpty(soundData);
        if (empty)
        {
            return;
        }

        // Pick a random sound entry
        SoundData.SoundEntry randomEntry = soundData.sounds[Random.Range(0, soundData.sounds.Length)];

        // Play the random sound
        PlayClip(randomEntry.clip, randomEntry.loop, randomEntry.mixer);
    }

    public void PlayRandomSoundAtLocation(SoundData soundData, AudioSource source)
    {
        bool empty = CheckIfSoundDataEmpty(soundData);
        if (empty)
        {
            return;
        }

        // Pick a random sound entry
        SoundData.SoundEntry randomEntry = soundData.sounds[Random.Range(0, soundData.sounds.Length)];

        source.clip = randomEntry.clip;
        source.outputAudioMixerGroup = randomEntry.mixer;
        source.Play();
    }

    private void PlayClip(AudioClip clip, bool loop, AudioMixerGroup mixer)
    {
        AudioSource currentSource = audioSources[currentAudioSourceIndex];

        // Set audio clip and settings
        currentSource.clip = clip;
        currentSource.loop = loop;
        currentSource.outputAudioMixerGroup = mixer;

        // Play the sound
        currentSource.Play();

        // Move to the next audio source in the list
        currentAudioSourceIndex = (currentAudioSourceIndex + 1) % audioSources.Count;
        Debug.Log($"SOUND: Played {clip}");
    }

    public void PlayLoop(SoundData soundData, string soundName, AudioSource targetSource)
    {
        bool empty = CheckIfSoundDataEmpty(soundData);
        if (empty)
        {
            return;
        }

        SoundData.SoundEntry entry = System.Array.Find(soundData.sounds, s => s.name == soundName);
        bool exist = CheckIfSoundExist(entry, soundName);
        if (!exist)
        {
            return;
        }

        // Set the target audio source settings
        targetSource.clip = entry.clip;
        targetSource.loop = entry.loop;
        targetSource.outputAudioMixerGroup = entry.mixer;
        targetSource.Play();
    }

    private bool CheckIfSoundDataEmpty(SoundData soundData)
    {
        if (soundData == null || soundData.sounds.Length == 0)
        {
            Debug.LogWarning("SoundData is null or contains no sounds.");
            return true;
        }
        return false;
    }

    private bool CheckIfSoundExist(SoundData.SoundEntry entry, string soundName)
    {
        if (entry == null || entry.clip == null)
        {
            Debug.LogWarning($"Sound '{soundName}' not found in SoundData.");
            return false;
        }
        return true;
    }

    private void OnValidate()
    {
        SetAudioMixerVolume();
    }
}