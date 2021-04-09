using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//[System.Serializable]
//public class Tutorial
//{
//    public GameObject tutorialPanel;
//    public GameObject tutorialCollider;
//}

public class Tutorials : MonoBehaviour
{
    public Flowchart flowChart;
    public List<GameObject> enemies;
    private MusicManager music;

    //public List<WeaponStats> stats;


    // Start is called before the first frame update
    void Awake()
    {
        music = gameObject.GetComponent<Player>().music.GetComponent<MusicManager>();
    }

    private void Start()
    {
        //switcher = gameObject.GetComponent<WeaponSwitcher>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        if (collision.gameObject.tag == "Sword")
        {
            Debug.Log("Tag the sword!");
            if (SceneManager.GetActiveScene().name == "Tutorial" && !flowChart.GetBooleanVariable("regswordpickup"))
            {
                music.GetComponent<AudioSource>().PlayOneShot(music.music.Find(x => x.name.Contains("ItemPickup")).clip, 0.5f);
                flowChart.ExecuteBlock("Pick up the Sword?");
                flowChart.SetBooleanVariable("regswordpickup", true);
            }

        }
        if (collision.gameObject.name == "Flavor1")
        {
            flowChart.StopBlock("Pick up the Sword?");
            flowChart.ExecuteBlock("Oh????");
        }
        if (collision.gameObject.name == "Flavor2")
        {
            flowChart.StopBlock("Pick up the Sword?");
            flowChart.ExecuteBlock("Will");
        }
        if(collision.gameObject.name == "Flavor3")
        {
            flowChart.StopBlock("Pick up the Sword?");
            flowChart.ExecuteBlock("What the");
        }
        if (collision.gameObject.name == "Flavor4")
        {
            flowChart.StopBlock("Pick up the Sword?");
            flowChart.ExecuteBlock("I wonder how");
        }
        if (collision.gameObject.name == "Flavor5")
        {
            flowChart.ExecuteBlock("The orbs are a trap");
        }
        if (collision.gameObject.name == "Flavor6")
        {
            flowChart.ExecuteBlock("IsThisTheEnd?");
        }
        if (collision.gameObject.name == "Flavor7")
        {
            flowChart.ExecuteBlock("And So It Begins");
        }

        //if (collision.gameObject.name == "Flavor4")
        //{
        //    flowChart.StopBlock("I wonder how");
        //    flowChart.ExecuteBlock("I wonder how");
        //}
        if (collision.gameObject.name == "Enemy" && flowChart.GetBooleanVariable("firstenemyTrigger"))
        {
            //flowChart.StopBlock("Pick up the Sword?");
            flowChart.ExecuteBlock("An escapee");
        }
        if(collision.gameObject.tag == "Health")
        {
            music.GetComponent<AudioSource>().PlayOneShot(music.music.Find(x => x.name.Contains("ItemPickup")).clip, 0.5f);
            //flowChart.StopBlock("I wonder how");
            flowChart.ExecuteBlock("Weird");
        }
        if(collision.gameObject.tag == "Stairs")
        {
            flowChart.ExecuteBlock("And So It Begins");
        }
    }

    private void OnCollisionEnter2D(UnityEngine.Collision2D collision)
    {
     
        if (collision.gameObject.tag == "Long Sword")
        {
            Debug.Log("Tag the sword!");
            if (SceneManager.GetActiveScene().name == "Tutorial" && flowChart.GetBooleanVariable("regswordpickup"))
            {
                music.GetComponent<AudioSource>().PlayOneShot(music.music.Find(x => x.name.Contains("ItemPickup")).clip, 0.5f);
                flowChart.ExecuteBlock("Pick up the Sword?");
                flowChart.SetBooleanVariable("regswordpickup", true);
            }


        }

    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Flavor1")
        {
            flowChart.StopBlock("Oh????");
        }
        if (collision.gameObject.name == "Flavor2")
        {
            flowChart.StopBlock("Will");
        }
        if (collision.gameObject.name == "Flavor3")
        {
            flowChart.StopBlock("What the");
        }
        if (collision.gameObject.name == "Flavor4")
        {
            flowChart.StopBlock("I wonder how");
        }
        if (collision.gameObject.name == "Flavor5")
        {
            flowChart.StopBlock("The orbs are a trap");
        }
        if (collision.gameObject.name == "Flavor6")
        {
            flowChart.StopBlock("IsThisTheEnd?");
        }
        if (collision.gameObject.name == "Flavor7")
        {
            flowChart.StopBlock("And So It Begins");
        }



        if (collision.gameObject.name == "Enemy")
        {
            flowChart.StopBlock("An escapee");
        }
    }
}