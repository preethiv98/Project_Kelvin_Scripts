using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float thrust;

    [SerializeField]
    private bool immuneToElectric;

    [SerializeField]
    private bool immuneToPlasma;

    private MusicManager music;

    public bool died = false;


    public int health = 100;

    public bool isBoss = false;

    //public GameObject deathEffect;

    private GameObject instantiatedDeath;

    bool findPlayer = false;

    public GameObject player;

    public int damage;

    public int specialDamage = 100;

    Rigidbody2D rb;

    Flowchart flow;

    private float blinkTime = 0.1f;
    private float immunedTime;
    public float blink;
    public float immuned;

    //public AudioSource source;
    //Stuff for the timing between attacks

    //float timerToAttack = 3f;
    //float time = 1f;
    //private bool canAttack = false;
    //private bool playerTouched = false;

    //private void Update()
    //{
    //    TimerToAttack();
    //}

    //void TimerToAttack()
    //{
    //    Debug.Log(playerTouched);
    //    if(playerTouched)
    //    {
    //        if(timerToAttack > 0)
    //        {

    //            canAttack = false;
    //            timerToAttack -= time * Time.deltaTime;
    //            Debug.Log(timerToAttack);
    //        }
    //        else
    //        {
    //            Debug.Log("Attack!");
    //            canAttack = true;
    //            timerToAttack = 3f;
    //        }
    //    }
    //}

    //void Awake()
    //{
    // source = GetComponent<AudioSource>();
    // }

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            player = GameObject.FindGameObjectWithTag("Player");
           
        }
        if(SceneManager.GetActiveScene().name == "Tutorial")
        {
            //if(flow)
            //{
                flow = GameObject.Find("Flowchart").GetComponent<Flowchart>();
           
            //}
           
        }

            music = GameObject.FindGameObjectWithTag("Music").GetComponent<MusicManager>();

      
    }

    private void Update()
    {
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            if(player.GetComponent<WeaponSwitcher>().primaryEquipped)
            {
                damage = player.GetComponent<WeaponSwitcher>().weaponHolding.damage;
            }
           
        }
        if (immunedTime > 0)
        {

            immunedTime -= Time.deltaTime;

            blinkTime -= Time.deltaTime;

            if (blinkTime <= 0)
            {
                //Debug.Log("wee");
                //modelRender1.enabled = !modelRender1.enabled;
                //modelRender2.enabled = !modelRender2.enabled;
                //gameObject.GetComponent<Animator>().SetLayerWeight(2, 0);
                //gameObject.GetComponent<Animator>().SetLayerWeight(1, 0);
                //gameObject.GetComponent<Animator>().SetLayerWeight(3, 1);
                //yield return new WaitForSeconds(2f);

                gameObject.GetComponent<Animator>().SetTrigger("Blinking");
                blinkTime = blink;
            }
            if (immunedTime <= 0)
            {
               
                //gameObject.GetComponent<Animator>().SetLayerWeight(2, 1);
                //gameObject.GetComponent<Animator>().SetLayerWeight(1, 1);
                //gameObject.GetComponent<Animator>().SetLayerWeight(3, 0);
                //gameObject.GetComponent<Animator>().SetLayerWeight(2, 1);
                //gameObject.GetComponent<Animator>().SetLayerWeight(1, 0);
                //gameObject.GetComponent<Animator>().SetLayerWeight(3, 0);
                //modelRender1.enabled = true;
                //modelRender2.enabled = true;
            }
        }
    }
    public void TakeDamage(int damage)
    {
        if (immunedTime <= 0)
        {
            health -= damage;
            if (health <= 0)
            {
                music.GetComponent<AudioSource>().PlayOneShot(music.music.Find(x => x.name.Contains("Explosion")).clip, 0.5f);
                //source.Play();
                Die();
            }
            else
            {
                //gameObject.GetComponent<Animator>().SetLayerWeight(2, 0);
                //gameObject.GetComponent<Animator>().SetLayerWeight(1, 0);
                //gameObject.GetComponent<Animator>().SetLayerWeight(3, 1);
                //gameObject.GetComponent<Animator>().SetLayerWeight(2, 1);
                immunedTime = immuned;
                blinkTime = blink;
            }
            //Debug.Log("damage!  " + damage);
           

        }

    }

    private void OnCollisionEnter2D(UnityEngine.Collision2D collision)
    {
        if(collision.gameObject.tag == "AttackSword")
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.AddForce(new Vector2(gameObject.transform.position.x + 3.5f, gameObject.transform.position.y + 3.5f));
            Debug.Log("sheet");
        }
       
        //Debug.Log("damage!");

        //if (collision.gameObject.tag == "Sword")
        //{
        //    Debug.Log("damage!");
        //    TakeDamage(damage);
        //}
    }



    void Die()
    {
        if (SceneManager.GetActiveScene().name == "Tutorial")
        {
            flow.StopBlock("An escapee");
            flow.ExecuteBlock("Damn");
        }
       
        //    if(!died)
        //    {
        //        if (flow.GetIntegerVariable("EnemyCount") == 0)
        //        {

        //            flow.ExecuteBlock("SecondaryWeapon");
        //            flow.StopBlock("AttackDroid");
        //            player.GetComponent<Tutorials>().doors[1].SetActive(false);
        //            flow.SetIntegerVariable("EnemyCount", 1);
        //            player.GetComponent<Tutorials>().firstTimeSword = false;
        //            died = true;
        //            Destroy(gameObject);
        //        }
        //        else if (flow.GetIntegerVariable("EnemyCount") == 1)
        //        {

        //            flow.ExecuteBlock("ProjectXXXXX");
        //            flow.StopBlock("SecondaryWeapon");
        //            player.GetComponent<Tutorials>().doors[3].SetActive(false);
        //            flow.SetIntegerVariable("EnemyCount", 2);
        //            player.GetComponent<Tutorials>().firstTimeLongSword = false;
        //            died = true;
        //            Destroy(gameObject);

        //        }
        //        else if (flow.GetIntegerVariable("EnemyCount") == 2)
        //        {

        //            flow.ExecuteBlock("End");
        //            flow.StopBlock("DefeatTheDroid");
        //            player.GetComponent<Tutorials>().doors[5].SetActive(false);
        //            flow.SetIntegerVariable("EnemyCount", 3);
        //            died = true;
        //            Destroy(gameObject);
        //        }

        //    }

        //}

        //Instantiate(deathEffect, transform.position, Quaternion.identity);
        if (isBoss)
        {
            StartCoroutine(Wait());
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            //(GameObject.Find("GameController")).GetComponent<GameOver>().BossDeath();
        }
        //player.GetComponent<Player>().AddExperience();
        player.GetComponent<Player>().counter++;
        //instantiatedDeath = (GameObject)Instantiate(deathEffect, transform.position, transform.rotation);
        Destroy(gameObject);

        Destroy(instantiatedDeath);

    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2f);
    }
    //void Give
   
    //{
    //    if (findPlayer)
    //    {
    //        if (GameObject.FindGameObjectWithTag("Player"))
    //        {
    //            GetComponent<Player>().TakeDamage(damage);

    //        }
    //    }
    //}

    private void OnCollisionStay2D(UnityEngine.Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")//or tag
        {
            gameObject.transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            TimerAttack.playerTouched = true;


        }
     

    }

    private void OnCollisionExit2D(UnityEngine.Collision2D collision)
    {
        gameObject.transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
    }

    private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        if (collision.gameObject.tag == "AttackSword")
        {
            //Rigidbody2D enemy = collision.GetComponent<Rigidbody2D>();
            //if (enemy != null)
            //{

            StartCoroutine(KnockCoroutine(rb));
            // }
            if (player.GetComponent<PlayerMovement>().dealingSpecial)
            {
                TakeDamage(specialDamage);
                music.GetComponent<AudioSource>().PlayOneShot(music.music.Find(x => x.name.Contains("Damage")).clip, 0.5f);
                player.GetComponent<PlayerMovement>().dealingSpecial = false;
            }
            else
            {
                if(player.GetComponent<WeaponSwitcher>().weaponHolding == player.GetComponent<WeaponSwitcher>().weaponList[1] && immuneToElectric)
                {
                   
                }
                else
                {
                    //Debug.Log("damage!");
                    rb.bodyType = RigidbodyType2D.Dynamic;
                    rb.AddForce(new Vector2(gameObject.transform.position.x + 1.5f, gameObject.transform.position.y + 1.5f));
                    music.GetComponent<AudioSource>().PlayOneShot(music.music.Find(x => x.name.Contains("Damage")).clip, 0.5f);
                    TakeDamage(damage);
                }
               
            }
         
        }
    }
    private IEnumerator KnockCoroutine(Rigidbody2D enemy)
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        Vector2 forceDirection = enemy.transform.position - transform.position;
        Vector2 force = forceDirection.normalized * thrust;

        enemy.velocity = force;
        yield return new WaitForSeconds(.3f);

        enemy.velocity = new Vector2();
    }
    //private void OnTriggerStay2D(Collider2D collider)
    //{
    //    if(collider.gameObject.name == "Player")
    //    {
    //        Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), collider);
    //        Physic
    //    }
    //}



}