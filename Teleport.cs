using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Teleporting
{
    public GameObject begin;
    public GameObject end;
}

public class Teleport : MonoBehaviour
{
    public GameObject levelChanger;
    //[SerializeField]
    //private GameObject player;
    public GameObject stairsPanel;

    public GameObject bulletPool;

    public GameObject bossFloor;

    public GameObject startPoint;

    private MusicManager music;
    //[SerializeField]
    //private List<Teleporting> teleport;
    // Start is called before the first frame update
    void Start()
    {
       
            music = GameObject.FindGameObjectWithTag("Music").GetComponent<MusicManager>();       
        //levelChanger.SetActive(false);
        stairsPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Yes()
    {
        GameObject grid = GameObject.FindGameObjectWithTag("Generation");
        if (grid.GetComponent<SpawnRoom>().numberOfLevels == 1)
        {
            grid.GetComponent<SpawnRoom>().DeleteRoom();
            bossFloor.SetActive(true);
            bulletPool.SetActive(false);
            GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>().clip = music.music.Find(x => x.name.Contains("Boss")).clip;
            GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>().Play();
            gameObject.transform.position = startPoint.transform.position;
            stairsPanel.SetActive(false);
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            levelChanger.SetActive(true);
            grid.GetComponent<SpawnRoom>().DeleteRoom();
            grid.GetComponent<SpawnRoom>().numberOfLevels--;
            levelChanger.GetComponent<LevelChanger>().FadeToLevel();
            //levelChanger.SetActive(false);
           
           
            //stairsPanel.SetActive(false);
        }


    }

    public void No()
    {
        stairsPanel.SetActive(false);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Stairs")
        {
            stairsPanel.SetActive(false);
           
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Stairs")
            {

                stairsPanel.SetActive(true);
            }

       

        //if(collision.tag == "Up")
        //{
        //    if(collision.gameObject.name == "Up Stairs")
        //    {
        //        gameObject.transform.position = GameObject.Find("Up Stairs Teleport").transform.position;
        //    }
        //    else
        //    {
        //        gameObject.transform.position = GameObject.Find("Up Stairs").transform.position;
        //    }
           
        //}
        //if (collision.tag == "Down")
        //{
        //    if (collision.gameObject.name == "Down Stairs")
        //    {
        //        gameObject.transform.position = GameObject.Find("Down Stairs Teleport").transform.position;
        //    }
        //    else
        //    {
        //        gameObject.transform.position = GameObject.Find("Down Stairs").transform.position;
        //    }

        //}
        //if (collision.tag == "Right")
        //{
        //    if (collision.gameObject.name == "Right Stairs")
        //    {
        //        gameObject.transform.position = GameObject.Find("Right Stairs Teleport").transform.position;
        //    }
        //    else
        //    {
        //        gameObject.transform.position = GameObject.Find("Right Stairs").transform.position;
        //    }

        //}
        //if (collision.tag == "Left")
        //{
        //    if (collision.gameObject.name == "Left Stairs")
        //    {
        //        gameObject.transform.position = GameObject.Find("Left Stairs Teleport").transform.position;
        //    }
        //    else
        //    {
        //        gameObject.transform.position = GameObject.Find("Left Stairs").transform.position;
        //    }

        //}
    }
}
