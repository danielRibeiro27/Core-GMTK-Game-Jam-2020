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
            s.source.spatialBlend = 1f;
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {

    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject p = GameObject.Find("Player");
        if (p != null)
        {
            player = p.transform;
        }
    }

    private void FixedUpdate()
    {
        if(player != null)
        {
            transform.position = player.position;
        }
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

    //da play no audio e aumenta o volume de 0 a 1
    public void FadeInAudio(string name, float wait_time)
    {
        //busca o audio no array
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogError("Som não encontrado");
            return;
        }

        //seta o volume como 0, porque o audio tem que começar mutado e ir ganhando volume
        s.source.volume = 0f;
        s.source.Play();

        //da play na coroutine que irá aumentar o volume gradualmente
        StartCoroutine(LerpInFloat(s.source, 0f, 1f, wait_time));
    }
    IEnumerator LerpInFloat(AudioSource src, float start_value, float end_value, float wait_time)
    {
        float elapsedTime = 0;
        while(elapsedTime < wait_time)
        {
            src.volume = Mathf.Lerp(start_value, end_value, (elapsedTime / wait_time));
            elapsedTime += Time.deltaTime;

            yield return null;
        }
    }
    
    //diminui o volume do audio de 1 a 0
    public void FadeOutAudio(string name, float wait_time,Level01Manager manager)
    {
        //busca o audio no array
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogError("Som não encontrado");
            return;
        }

        //da play na coroutine que irá diminuir o volume gradualmente
        StartCoroutine(LerpOutFloat(s.source, s.source.volume, 0f, wait_time, manager));
    }

    IEnumerator LerpOutFloat(AudioSource src, float start_value, float end_value, float wait_time, Level01Manager manager)
    {
        float elapsedTime = 0;
        while (elapsedTime < wait_time)
        {
            src.volume = Mathf.Lerp(start_value, end_value, (elapsedTime / wait_time));
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        //depois de diminuir o volume, para o audio e notifica que pode dar o FadeIn
        src.Stop();
        manager.OnMusicFinished();
    }

    #endregion
}
