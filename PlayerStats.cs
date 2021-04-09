using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    //public int health = 500;

    public static int numOfLives = 3;

    bool isTakingDamage = false;

    public GameObject deathEffect;

    private GameObject instantiatedDeath;


    [SerializeField]
    private Stat health;


    [SerializeField]
    private Image[] lives;

    [SerializeField]
    private Sprite heart;


    void Awake()
    {
        health.Initialize();
    }


    public void TakeDamage(int damage)
    {
        isTakingDamage = true;

        health.CurrentVal -= damage;

        isTakingDamage = false;

        if (health.CurrentVal <= 0)
        {

            Die();
        }
    }

    public void AddHealth()
    {
        health.CurrentVal += 50;
    }

    public void AddLife()
    {
        numOfLives++;
    }


    void Die()
    {
        //Instantiate(deathEffect, transform.position, Quaternion.identity);
        numOfLives--;
        health.CurrentVal = health.MaxVal;
        if (numOfLives == -1)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
            //    GameOver();
        }
    }

    void Update()
    {
        for (int i = 0; i < lives.Length; i++)
        {
            if (i < numOfLives)
            {
                lives[i].enabled = true;
            }
            else
            {
                lives[i].enabled = false;
            }
        }
    }

    //void GameOver()
    //{
    //    SceneManager.LoadScene(1);
    //}

}