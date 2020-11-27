using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Sound[] sounds;
    public static AudioManager instance;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume =s.volume*VolumeSetter.Volume;
            Debug.Log(VolumeSetter.Volume);
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.playOnAwake = s.playonawake;
            s.source.mute = s.mute;
           
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Sound s in sounds)
        {
            s.source.volume = s.volume * VolumeSetter.Volume;
        }
    }

    

     public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.Name == name);

        
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + "Not found");
            return;
        }
        s.source.Play();
    }
    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.Name == name);


        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + "Not found");
            return;
        }
        s.source.Stop();
    }
    public void Mute(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.Name == name);


        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + "Not found");
            return;
        }
        s.source.mute = true;
    }
    public void Unmute(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.Name == name);


        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + "Not found");
            return;
        }
        s.source.mute = false;
    }

}
