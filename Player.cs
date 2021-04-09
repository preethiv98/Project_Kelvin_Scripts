using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Fungus;


public class Player : MonoBehaviour
{
    [SerializeField]
    private float thrust = 3f;


    public MusicManager music;

    public int currentLevel = 1;
    //public int health = 500;

    //public static int numOfLives = 3;
    public GameObject levelChanger;

    bool isTakingDamage = false;

    public GameObject deathEffect;

    public bool outOfSpecial = false;
    private GameObject instantiatedDeath;

    [SerializeField]
    private Stat health;

    [SerializeField]
    private Stat special;


    public TextMeshProUGUI destructionText;

    public int counter = 0;
    //[SerializeField]
    //private Stat experience;

    [SerializeField]
    private TextMeshProUGUI levelText;


    [SerializeField]
    private string nextLevel;

    [SerializeField]
    private GameObject diedPanel;


    [SerializeField]
    private GameObject canvasPanel;

    //[SerializeField]
    //private Image[] lives;

    public int specialCost;

    //[SerializeField]
    //private Sprite heart;
    public static bool diedBool=false;
    private float blinkTime = 0.1f;
    private float immunedTime;
    public float blink;
    public float immuned;


    [SerializeField]
    private 

    int numberOfLevels = 0;
    void Awake()
    {

            music = GameObject.FindGameObjectWithTag("Music").GetComponent<MusicManager>();
       
        Scene currentScene = SceneManager.GetActiveScene();
       
        string sceneName = currentScene.name;
        if(health != null)
        {
            health.Initialize();
        }
        if(special != null)
        {
            special.Initialize();
        }
        //if(experience != null)
        //{
        //    experience.Initialize();
        //}
       
       
        //if(sceneName == "Tutorial_Floor_One")
        //{

        //DontDestroyOnLoad(gameObject);
        //DontDestroyOnLoad(canvasPanel);
        //DontDestroyOnLoad(diedPanel);
        //}

    }

   
    private void Start()
    {
        //numberOfLevels = GameObject.FindGameObjectWithTag("Grid").GetComponent<SpawnRoom>().numberOfLevels;
    }

    private void Update()
    {
        if (immunedTime > 0)
        {

            immunedTime -= Time.deltaTime;

            blinkTime -= Time.deltaTime;

            if (blinkTime <= 0)
            {
                //Debug.Log("wee");
                //modelRender1.enabled = !modelRender1.enabled;
                //modelRender2.enabled = !modelRender2.enabled;
                gameObject.GetComponent<Animator>().SetLayerWeight(2, 0);
                gameObject.GetComponent<Animator>().SetLayerWeight(1, 0);
                gameObject.GetComponent<Animator>().SetLayerWeight(3, 1);
                //yield return new WaitForSeconds(2f);
              

                blinkTime = blink;
            }
            if (immunedTime <= 0)
            {
                gameObject.GetComponent<Animator>().SetLayerWeight(2, 1);
                gameObject.GetComponent<Animator>().SetLayerWeight(1, 1);
                gameObject.GetComponent<Animator>().SetLayerWeight(3, 0);
                //gameObject.GetComponent<Animator>().SetLayerWeight(2, 1);
                //gameObject.GetComponent<Animator>().SetLayerWeight(1, 0);
                //gameObject.GetComponent<Animator>().SetLayerWeight(3, 0);
                //modelRender1.enabled = true;
                //modelRender2.enabled = true;
            }
        }
        destructionText.text = "" + counter;
    }

    public void TakeDamage(int damage)
    {
        if (immunedTime <= 0)
        {
            isTakingDamage = true;

            health.CurrentVal -= damage;
            //Invoke("WaitAnim", 0f);
            isTakingDamage = false;

            if (health.CurrentVal <= 0)
            {
                music.GetComponent<AudioSource>().PlayOneShot(music.music.Find(x => x.name.Contains("Explosion")).clip, 0.5f);
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
        }
    }

    //void WaitAnim()
    //{
    //    while(isTakingDamage)
    //    {
            
    //        gameObject.GetComponent<Animator>().SetLayerWeight(2, 0);
    //        gameObject.GetComponent<Animator>().SetLayerWeight(1, 0);
    //        gameObject.GetComponent<Animator>().SetLayerWeight(0, 1);
    //        //yield return new WaitForSeconds(2f);
    //        gameObject.GetComponent<Animator>().SetLayerWeight(2, 1);
    //        gameObject.GetComponent<Animator>().SetLayerWeight(1, 0);
    //        gameObject.GetComponent<Animator>().SetLayerWeight(0, 0);
    //    }
       
       
    //}

    public void ExecuteSpecial(int specialCost)
    {
        if (special.CurrentVal >= specialCost)
        {
            //special.MaxVal = 260;
            special.CurrentVal -= specialCost;
        }
        else
        {
            outOfSpecial = true;
        }
       
    }
   
    public void AddHealth()
    {
        health.CurrentVal += 50;
    }

    //public void AddExperience()
    //{
    //    experience.CurrentVal += 10;
    //    if(experience.MaxVal == experience.CurrentVal)
    //    {
    //        levelText.text = "" + (currentLevel + 1);
    //        experience.MaxVal *= 1.25f;
            
    //    }
    //}

    public void AddSpecial()
    {
        
        if(special.CurrentVal >= specialCost)
        {
            outOfSpecial = false;
           
        }
        special.CurrentVal += 30;
    }

    public void AddLife()
    {
        //numOfLives++;
    }


    void Die()
    {
        //Instantiate(deathEffect, transform.position, Quaternion.identity);
        //numOfLives--;
       if(SceneManager.GetActiveScene().name == "Tutorial")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
       else
        {
            if(GameObject.FindGameObjectWithTag("Game Flowchart"))
            {
                GameObject.FindGameObjectWithTag("Game Flowchart").GetComponent<Flowchart>().SetBooleanVariable("notFirstSceneLoaded", true);
            }
            diedBool = true;
            health.CurrentVal = health.MaxVal;
            special.CurrentVal = special.MaxVal;
            diedPanel.SetActive(true);
            if (GameObject.FindGameObjectWithTag("GameController"))
            {
                GameObject.FindGameObjectWithTag("GameController").GetComponent<FlowchartVariable>().flow.SetBooleanVariable("notFirstSceneLoaded", true);
            }




            //gameObject.GetComponent<WeaponSwitcher>().weaponHolding = gameObject.GetComponent<WeaponSwitcher>().weaponList[0];
            //gameObject.GetComponent<WeaponSwitcher>().primaryObject.GetComponent<Image>().sprite = gameObject.GetComponent<WeaponSwitcher>().weaponList[0].weaponSprite;
            //gameObject.GetComponent<WeaponSwitcher>().secondaryObject.GetComponent<Image>().sprite = null;
            //gameObject.GetComponent<WeaponSwitcher>().secondaryEquipped = false;

            //GameObject.FindGameObjectWithTag("Grid").GetComponent<LevelGeneration>().numberOfLevels = numberOfLevels;
            ////if (numOfLives == -1)
            ////{
            ////if (deathEffect)
            ////{
            ////    Instantiate(deathEffect, transform.position, Quaternion.identity);

            ////}
            levelChanger.GetComponent<LevelChanger>().playerrb = gameObject.GetComponent<Rigidbody2D>().constraints;
            // gameObject.SetActive(false);

            diedPanel.GetComponent<LevelChanger>().FadeToNextCutscene();
            //Destroy(gameObject);
            //GameOver();
        }


    }




    //void Update()
    //{
    //    //for (int i = 0; i < lives.Length; i++)
    //    //{
    //    //    if (i < numOfLives)
    //    //    {
    //    //        lives[i].enabled = true;
    //    //    }
    //    //    else
    //    //    {
    //    //        lives[i].enabled = false;
    //    //    }
    //    //}
    //}
    private void OnCollisionEnter2D(UnityEngine.Collision2D collision)
    {
        //if (collision.gameObject.tag == "Enemy")
        //{
        //    Invoke(KnockCoroutine(gameObject.GetComponent<Rigidbody2D>()), 0.2f);
        //}

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       

        //if(collision.tag == "
        //    ")
        //{

        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        //}
        if(collision.tag == "Bullet")
        {
            TakeDamage(collision.gameObject.GetComponent<Bullet>().damage);
            music.GetComponent<AudioSource>().PlayOneShot(music.music.Find(x => x.name.Contains("Damage")).clip, 0.5f);
            Destroy(collision.gameObject);
        }
        if (collision.tag == "Health")
        {
            AddHealth();
            music.GetComponent<AudioSource>().PlayOneShot(music.music.Find(x => x.name.Contains("HealthPickup")).clip, 0.5f);
            Destroy(collision.gameObject);
        }
        if(collision.tag == "Special")
        {
            AddSpecial();
            music.GetComponent<AudioSource>().PlayOneShot(music.music.Find(x => x.name.Contains("ItemPickup")).clip, 0.5f);
            Destroy(collision.gameObject);
        }
    }

    private string KnockCoroutine(Rigidbody2D enemy)
    {
        //enemy.isKinematic = true;
        Vector2 forceDirection = enemy.transform.position - transform.position;
        Vector2 force = forceDirection.normalized * thrust;

        enemy.velocity = force;
        //yield return new WaitForSeconds(.3f);
       
        enemy.velocity = new Vector2();
        return "KnockCoroutine";
    }
    //void GameOver()
    //{
    //    SceneManager.LoadScene(1);
    //}


}
