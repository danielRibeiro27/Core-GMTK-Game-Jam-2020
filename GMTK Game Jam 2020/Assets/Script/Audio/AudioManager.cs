using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

/// <summary>
/// Gerenciador de audios que adiciona os audio sources e armazena os audios disponíveis
/// </summary>
public class AudioManager : MonoBehaviour
{
    //singleton
    public static AudioManager instance;


    [SerializeField] private string[] PlayOnAwake;
    [SerializeField] private AudioMixerGroup mainMixer;
    public Sound[] sounds;
    private Transform player;

    private void Awake()
    {
        #region Instance

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        #endregion

        //adiciona o audio source para tocare o som
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = mainMixer;
            s.source.spatialBlend = 0f;
            s.source.minDistance = 400f;
            s.source.maxDistance = 400f;
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {

    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

    }

    private void FixedUpdate()
    {

    }

    #region Class Methods

    public void PlayByName(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if(s == null){
            Debug.LogError("Som não encontrado");
            return;
        }

        s.source.Play();
    }

    public void StopByName(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogError("Som não encontrado");
            return;
        }

        s.source.Stop();
    }

    public void StopAllAudios()
    {
        foreach(Sound s in sounds)
        {
            s.source.Stop();
        }
    }

    #endregion
}
