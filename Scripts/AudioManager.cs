using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    public Sound[] sounds;

    void Awake()
    {
        foreach (Sound s in sounds) {
            s.audioSource = gameObject.AddComponent<AudioSource>();
            s.audioSource.clip = s.clip;
            s.audioSource.volume = s.volume;
            s.audioSource.pitch = s.pitch;

        }
    }

    public void Play(string name) { 
        Sound sound = Array.Find(sounds, s => s.name == name);
        sound.audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
