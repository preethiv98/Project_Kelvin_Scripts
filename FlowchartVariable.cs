using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Fungus;

public class FlowchartVariable : MonoBehaviour
{
    public Flowchart flow;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(flow);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnable()
    {
        //Debug.Log("OnEnable called");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // called second
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(SceneManager.GetActiveScene().name == "Tutorial")
        {
            flow.SetBooleanVariable("firstTimePlayer", false);
        }

        if(SceneManager.GetActiveScene().name == "Game Cutscene")
        {
            flow = GameObject.FindGameObjectWithTag("Game Flowchart").GetComponent<Flowchart>();

        }
        //Debug.Log("OnSceneLoaded: " + scene.name);
        //Debug.Log(mode);
        ////if (!diedBool)
        ////{
        //    if (GameObject.Find("Start Point") && player)
        //    {
        //        player.transform.position = GameObject.Find("Start Point").transform.position;
        //    }


        //}
        //else
        //{
        //    diedBool = false;
        //}

    }
}
