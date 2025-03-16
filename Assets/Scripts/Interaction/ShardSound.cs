using System.Collections.Generic;
using UnityEngine;

public class ShardSound : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private List<AudioClip> clickSounds;
    public void PlayClickSound(float delayedTime = 0)
    {
        audioSource.clip = clickSounds[Random.Range(0, clickSounds.Count)];
        audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.PlayDelayed(delayedTime);
    }
}
