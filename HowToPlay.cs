using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToPlay : MonoBehaviour
{
    public List<GameObject> pages;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TurnRight()
    {
        for(int i = 0; i < pages.Count; i++)
        {
            if(pages[i].activeSelf && i!= pages.Count-1)
            {
                pages[i].SetActive(false);
                pages[i + 1].SetActive(true);
                break;
            }
            else if(pages[i].activeSelf && i == pages.Count-1)
            {
                pages[i].SetActive(false);
                pages[0].SetActive(true);
                break;
            }
        }
        //Debug.Log("click");
        //menu.SetActive(true);
    }

    public void TurnLeft()
    {
        for (int i = 0; i < pages.Count; i++)
        {
            if (pages[i].activeSelf && i != 0)
            {
                pages[i].SetActive(false);
                pages[i - 1].SetActive(true);
                break;
            }
            else if(pages[i].activeSelf && i == 0)
            {
                pages[i].SetActive(false);
                pages[pages.Count - 1].SetActive(true);
                break;
            }
        }
        //Debug.Log("Going Left");
        //menu.SetActive(false);
    }
}
