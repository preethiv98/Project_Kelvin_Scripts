using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Fungus;

public class GameManager : MonoBehaviour
{
    public GameObject diedPanel;

    public GameObject levelChanger;
    public GameObject grid;
    public GameObject player;

    public Flowchart flow;

    public bool diedFirst = false;

    public int numberOfLevels;
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        if(flow)
        {
            DontDestroyOnLoad(flow);
        }

        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    public void Restart()
    {
        player.SetActive(true);
        diedPanel.SetActive(false);
        grid.GetComponent<LevelGeneration>().player = player;
      
        grid.GetComponent<LevelGeneration>().Reset();
        grid.GetComponent<LevelGeneration>().StartGeneration();
        //GameObject.FindGameObjectWithTag("Grid").GetComponent<LevelGeneration>().player = player;
        //player.GetComponent<WeaponSwitcher>().weaponHolding = gameObject.GetComponent<WeaponSwitcher>().weaponList[0];
        //player.GetComponent<WeaponSwitcher>().primaryObject.GetComponent<Image>().sprite = gameObject.GetComponent<WeaponSwitcher>().weaponList[0].weaponSprite;
        //player.GetComponent<WeaponSwitcher>().secondaryObject.GetComponent<Image>().sprite = null;
        //player.GetComponent<WeaponSwitcher>().secondaryEquipped = false;
        //GameObject.FindGameObjectWithTag("Grid").GetComponent<LevelGeneration>().numberOfLevels = numberOfLevels;
        ////if (numOfLives == -1)
        ////{
        ////if (deathEffect)
        ////{
        ////    Instantiate(deathEffect, transform.position, Quaternion.identity);

        ////}
        //levelChanger.GetComponent<LevelChanger>().playerrb = gameObject.GetComponent<Rigidbody2D>().constraints;
    }



    //if(SceneManager.GetActiveScene().buildIndex >= 5)
    //{
    //    diedPanel.SetActive(false);
    //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    //}
    //else
    //{
    //    diedPanel.SetActive(false);
    //    SceneManager.LoadScene(1);

    //}



    public void Quit()
    {
        Application.Quit();
    }

    public void BackToMainMenu()
    {
        diedPanel.SetActive(false);
        SceneManager.LoadScene(0);
    }

    void OnEnable()
    {
        //Debug.Log("OnEnable called");
        //SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // called second
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
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