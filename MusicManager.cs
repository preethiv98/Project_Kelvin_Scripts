using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


[System.Serializable]
public class Music
{
    public string name;
    public AudioClip clip;
}

public class MusicManager : MonoBehaviour
{
    public List<Music> music;
    private static MusicManager _instance;
    AudioSource source;

    public static MusicManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<MusicManager>();
            }

            return _instance;
        }
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        source = GetComponent<AudioSource>();
        source.PlayOneShot(music[0].clip, 0.5f);
    }

    void OnEnable()
    {
        //Debug.Log("OnEnable called");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // called second
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //Debug.Log("OnSceneLoaded: " + scene.name);
        //Debug.Log(mode);
        if(scene.name == "Tutorial" || scene.name == "Level One")
        {
            source.Stop();
            source.clip = music[2].clip;
            source.Play();
        }
        if (scene.name == "Game Cutscene")
        {
            source.Stop();
            source.clip = music[3].clip;
            source.Play();
        }
        if (scene.name == "Tutorial Cutscene" || scene.name == "Main Menu")
        {
            source.Stop();
            source.clip = music[0].clip;
            source.Play();
        }
        

    }
}
