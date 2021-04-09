using Com.LuisPedroFonseca.ProCamera2D;
using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCutsceneManager : MonoBehaviour
{
    ProCamera2DCinematics Cinematics;
    public Flowchart flow;
    public GameObject enemy;
    public List<GameObject> door;
    // Start is called before the first frame update
    void Start()
    {
        Cinematics = gameObject.GetComponent<ProCamera2DCinematics>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playCinematics()
    {
        Cinematics.Play();
    }

    public void playAnimation()
    {
        enemy.GetComponent<Animator>().Play("Enemy_Walk");
    }

    public void endCinematics()
    {
        Cinematics.Stop();
    }

    public void deleteThings()
    {
        enemy.SetActive(false);
        door[0].SetActive(false);
        door[1].SetActive(false);
    }
}
