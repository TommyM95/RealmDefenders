using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    public enum Sound
    {
        Hit,
        Explo,
        Shoot
    }

    private AudioSource audioSource;
    private Dictionary<Sound, AudioClip> audioDictinary;

    private void Awake()
    {
        Instance = this;

        audioSource = GetComponent<AudioSource>();
        audioDictinary = new Dictionary<Sound, AudioClip>();

        foreach (Sound sound in System.Enum.GetValues(typeof(Sound)))
        {
            audioDictinary[sound] = Resources.Load<AudioClip>(sound.ToString());
        }
    }

    public void PlaySound(Sound sound)
    {
        audioSource.PlayOneShot(audioDictinary[sound]);
    }
}
