using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{

    public AudioClip clip;
    public string name;

    [Range(0,1)]
    public float volume;

    [Range(0, 1)]
    public float pitch;

    //I don't why Brackeys hide this in the inspector! "Whatever..." -Atreus
    [HideInInspector]
    public AudioSource source;

    public bool loop;
}
