using UnityEngine.Audio;
using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Random = System.Random;

public class AudioManager : MonoBehaviour
{


    public static AudioManager instance;
    public readonly string masterVolExposed = "MasterVol";
    public readonly string musicVolExposed = "MusicVol";
    public readonly string sfxVolExposed = "SfxVol";

    public AudioMixerGroup musicMixerGroup;
    public AudioMixerGroup sfxMixerGroup; // sound effects
    [HideInInspector]
    public AudioSource musicAudioSource;
    [HideInInspector]
    public AudioSource sfxAudioSource;

    public Sound[] musicGlobalSounds;
    public Sound[] sfxGlobalSounds;

    private Sound[] allGlobalSounds;

    private Random random;


    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        random = new Random();

        musicAudioSource = gameObject.AddComponent<AudioSource>();
        musicAudioSource.outputAudioMixerGroup = musicMixerGroup;
        sfxAudioSource = gameObject.AddComponent<AudioSource>();
        sfxAudioSource.outputAudioMixerGroup = sfxMixerGroup;


        AddSoundToMixerGroup(musicMixerGroup, musicGlobalSounds);
        AddSoundToMixerGroup(sfxMixerGroup, sfxGlobalSounds);

        allGlobalSounds = musicGlobalSounds.Concat<Sound>(sfxGlobalSounds).ToArray();

    }

    private void Start()
    {
        LoadPlayerPrefs();
    }

    private void LoadPlayerPrefs()
    {
        //Set volume if Playerprefs are set, else use mixersettings
        float masterVol = PlayerPrefs.GetFloat(masterVolExposed, -100);
        float musicVol = PlayerPrefs.GetFloat(musicVolExposed, -100);
        float sfxVol = PlayerPrefs.GetFloat(sfxVolExposed, -100);
        if (masterVol >= -80) musicMixerGroup.audioMixer.SetFloat(masterVolExposed, masterVol);
        if (musicVol >= -80) musicMixerGroup.audioMixer.SetFloat(musicVolExposed, musicVol);
        if (sfxVol >= -80) musicMixerGroup.audioMixer.SetFloat(sfxVolExposed, sfxVol);
    }

    private void AddSoundToMixerGroup(AudioMixerGroup audioMixerGroup, Sound[] sounds)
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.loop = s.loop;

            s.source.outputAudioMixerGroup = audioMixerGroup;
        }
    }

    public Sound FindSoundByName(string sound)
    {
        Sound s = Array.Find(allGlobalSounds, item => item.name == sound);
        if (s != null && s.clip == null)
        {
            Debug.LogWarning("Clip on Sound " + sound + " not found!");
        }
        if (s == null)
        {
            Debug.LogWarning("Sound: " + sound + " not found!");
            s = new Sound();
        }
        return s;
    }

    public void PlayRandomSFXSound(List<AudioClip> audioClips)
    {
        if (sfxAudioSource && audioClips.Count > 0)
        {
            sfxAudioSource.PlayOneShot(audioClips[random.Next(0, audioClips.Count)]);
        }
    }

}
