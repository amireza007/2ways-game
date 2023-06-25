using UnityEngine.Audio;
using UnityEngine;
using System;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public bool ShouldLowerSound = false;
    void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.pitch = s.pitch;  
            s.source.volume = s.volume;
            s.source.loop = s.loop;
        }
    }
    private void Update()
    {
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }
    
    public IEnumerator LowerVolume(string name, float volumeDecrease)
    {
       
        Sound s = Array.Find(sounds, sound => sound.name == name);
        //s.source.volume = Mathf.Lerp(s.source.volume, volumeDecrease, 0.01f);
        while (!(s.source.volume == volumeDecrease))
        {
            s.source.volume = MathF.Round(Mathf.Lerp(s.source.volume, volumeDecrease, 0.05f), 2);
            yield return new WaitForEndOfFrame();
        }
        
    }
    
}
