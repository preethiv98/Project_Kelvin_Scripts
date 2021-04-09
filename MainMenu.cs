using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public GameObject pressAnyButton;
    public GameObject mainMenuPanel;
    public GameObject musicManager;
    bool mainMenuAccessed;
    // Start is called before the first frame update
    void Start()
    {
        if(!GameObject.FindGameObjectWithTag("Music"))
        {
            Instantiate(musicManager);
            musicManager = GameObject.FindGameObjectWithTag("Music");
        }
        else
        {
            musicManager = GameObject.FindGameObjectWithTag("Music");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!mainMenuAccessed)
        {
            Check();
        }

    }

    public void Check()
    {
        if (Input.anyKeyDown)
        {

            mainMenuPanel.SetActive(true);
            pressAnyButton.SetActive(false);
            mainMenuAccessed = true;
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void ConceptArt()
    {
        SceneManager.LoadScene("Concept Art");
    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }
}


