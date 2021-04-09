using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerAttack : MonoBehaviour
{

    private float timerToAttack = 0.5f;
    float time = 1f;
    private bool canAttack = false;
    public static bool playerTouched = false;
    public GameObject player;

    Enemy enemy;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemy = GetComponent<Enemy>();
    }
    // Update is called once per frame
    void Update()
    {
        TimerToAttack();
    }

    void TimerToAttack()
    {
        if(player)
        {
            if (playerTouched)
            {
                if (timerToAttack > 0)
                {

                    canAttack = false;
                    timerToAttack -= time * Time.deltaTime;

                }
                else
                {
                    canAttack = true;
                    player.GetComponent<Player>().TakeDamage(enemy.damage);
                    timerToAttack = 0.4f;
                }
            }
            playerTouched = false;
        }
        
    }
}
