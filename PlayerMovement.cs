using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private Animator anim;

    [SerializeField]
    private GameObject cam;
    private float fraction = 0;
    
    public float moveSpeed = 5f;

    [SerializeField]
    private Rigidbody2D rb;

    public BoxCollider2D col;

    [SerializeField]
    private float thrust = 0.5f;

    WeaponSwitcher switcher;

    private MusicManager music;

    //bool try = false;
    //[SerializeField]
    //private Animator animator;

    Vector2 movement;
    bool froze = false;

    bool trigger;
    bool check;
    bool left, right, top, down;

    bool attackCheck = false;

    public bool dealingSpecial = false;

   
    public GameObject[] attackColliders;
    // Start is called before the first frame update
    void Start()
    {

            music = GameObject.FindGameObjectWithTag("Music").GetComponent<MusicManager>();

       
        switcher = gameObject.GetComponent<WeaponSwitcher>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (froze)
        {

        }
        else
        {
            StopCoroutine(Waiting());
            //if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
            //{
               
            //}
                if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
                {
                music.GetComponent<AudioSource>().PlayOneShot(music.music.Find(x => x.name.Contains("Footsteps")).clip, 0.3f);
                anim.SetLayerWeight(2, 1);
                    anim.SetLayerWeight(1, 0);
                    if (Input.GetMouseButtonDown(0))
                    {
                    if (switcher.primaryEquipped && SceneManager.GetActiveScene().name != "Game Cutscene")
                    {
                        // Debug.Log("drip");
                        //music.GetComponent<AudioSource>().PlayOneShot(music.music.Find(x => x.name.Contains("Attack")).clip, 0.5f);
                        if (anim.GetFloat("Horizontal") == -1)
                        {
                            if (gameObject.GetComponent<WeaponSwitcher>().weaponHolding == gameObject.GetComponent<WeaponSwitcher>().weaponList[2])
                            {
                                anim.Play("Attack_Left", 2);
                            }
                            if (gameObject.GetComponent<WeaponSwitcher>().weaponHolding == gameObject.GetComponent<WeaponSwitcher>().weaponList[0])
                            {
                                anim.Play("Attack_Left_Regular", 2);
                            }
                            if (gameObject.GetComponent<WeaponSwitcher>().weaponHolding == gameObject.GetComponent<WeaponSwitcher>().weaponList[3])
                            {
                                anim.Play("Attack_Left_Plasma", 2);
                            }
                            if (gameObject.GetComponent<WeaponSwitcher>().weaponHolding == gameObject.GetComponent<WeaponSwitcher>().weaponList[1])
                            {
                                anim.Play("Attack_Left_Electric", 2);
                            }
                        }
                        else if (anim.GetFloat("Vertical") == -1)
                        {
                            if (gameObject.GetComponent<WeaponSwitcher>().weaponHolding == gameObject.GetComponent<WeaponSwitcher>().weaponList[2])
                            {
                                anim.Play("Attack_Down", 2);
                            }
                            if (gameObject.GetComponent<WeaponSwitcher>().weaponHolding == gameObject.GetComponent<WeaponSwitcher>().weaponList[0])
                            {
                                anim.Play("Attack_Down_Regular", 2);
                            }
                            if (gameObject.GetComponent<WeaponSwitcher>().weaponHolding == gameObject.GetComponent<WeaponSwitcher>().weaponList[3])
                            {
                                anim.Play("Attack_Down_Plasma", 2);
                            }
                            if (gameObject.GetComponent<WeaponSwitcher>().weaponHolding == gameObject.GetComponent<WeaponSwitcher>().weaponList[1])
                            {
                                anim.Play("Attack_Down_Electric", 2);
                            }
                        }
                        else if (anim.GetFloat("Horizontal") == 1)
                        {
                            if (gameObject.GetComponent<WeaponSwitcher>().weaponHolding == gameObject.GetComponent<WeaponSwitcher>().weaponList[2])
                            {
                                anim.Play("Attack_Right", 2);
                            }
                            if (gameObject.GetComponent<WeaponSwitcher>().weaponHolding == gameObject.GetComponent<WeaponSwitcher>().weaponList[0])
                            {
                                anim.Play("Attack_Right_Regular", 2);
                            }
                            if (gameObject.GetComponent<WeaponSwitcher>().weaponHolding == gameObject.GetComponent<WeaponSwitcher>().weaponList[3])
                            {
                                anim.Play("Attack_Right_Plasma", 2);
                            }
                            if (gameObject.GetComponent<WeaponSwitcher>().weaponHolding == gameObject.GetComponent<WeaponSwitcher>().weaponList[1])
                            {
                                anim.Play("Attack_Right_Electric", 2);
                            }
                        }
                        else if (anim.GetFloat("Vertical") == 1)
                        {
                            if (gameObject.GetComponent<WeaponSwitcher>().weaponHolding == gameObject.GetComponent<WeaponSwitcher>().weaponList[2])
                            {
                                anim.Play("Attack_Up", 2);
                            }
                            if (gameObject.GetComponent<WeaponSwitcher>().weaponHolding == gameObject.GetComponent<WeaponSwitcher>().weaponList[0])
                            {
                                anim.Play("Attack_Up_Regular", 2);
                            }
                            if (gameObject.GetComponent<WeaponSwitcher>().weaponHolding == gameObject.GetComponent<WeaponSwitcher>().weaponList[3])
                            {
                                anim.Play("Attack_Up_Plasma", 2);
                            }
                            if (gameObject.GetComponent<WeaponSwitcher>().weaponHolding == gameObject.GetComponent<WeaponSwitcher>().weaponList[1])
                            {
                                anim.Play("Attack_Up_Electric", 2);
                            }
                        }
                    }
                }
                if (Input.GetMouseButtonDown(1))
                    {
                    if (!gameObject.GetComponent<Player>().outOfSpecial)
                    {
                        if (switcher.primaryEquipped && SceneManager.GetActiveScene().name != "Game Cutscene")
                        {
                            music.GetComponent<AudioSource>().PlayOneShot(music.music.Find(x => x.name.Contains("Attack")).clip, 0.5f);
                            dealingSpecial = true;
                            gameObject.GetComponent<Player>().ExecuteSpecial(gameObject.GetComponent<Player>().specialCost);
                            // Debug.Log("drip");
                            if (anim.GetFloat("Horizontal") == -1)
                            {
                                if (gameObject.GetComponent<WeaponSwitcher>().weaponHolding == gameObject.GetComponent<WeaponSwitcher>().weaponList[2])
                                {
                                    anim.Play("Attack_Left", 2);
                                }
                                if (gameObject.GetComponent<WeaponSwitcher>().weaponHolding == gameObject.GetComponent<WeaponSwitcher>().weaponList[0])
                                {
                                    anim.Play("Attack_Left_Regular", 2);
                                }
                                if (gameObject.GetComponent<WeaponSwitcher>().weaponHolding == gameObject.GetComponent<WeaponSwitcher>().weaponList[3])
                                {
                                    anim.Play("Attack_Left_Plasma", 2);
                                }
                                if (gameObject.GetComponent<WeaponSwitcher>().weaponHolding == gameObject.GetComponent<WeaponSwitcher>().weaponList[1])
                                {
                                    anim.Play("Attack_Left_Electric", 2);
                                }
                            }
                            else if (anim.GetFloat("Vertical") == -1)
                            {
                                if (gameObject.GetComponent<WeaponSwitcher>().weaponHolding == gameObject.GetComponent<WeaponSwitcher>().weaponList[2])
                                {
                                    anim.Play("Attack_Down", 2);
                                }
                                if (gameObject.GetComponent<WeaponSwitcher>().weaponHolding == gameObject.GetComponent<WeaponSwitcher>().weaponList[0])
                                {
                                    anim.Play("Attack_Down_Regular", 2);
                                }
                                if (gameObject.GetComponent<WeaponSwitcher>().weaponHolding == gameObject.GetComponent<WeaponSwitcher>().weaponList[3])
                                {
                                    anim.Play("Attack_Down_Plasma", 2);
                                }
                                if (gameObject.GetComponent<WeaponSwitcher>().weaponHolding == gameObject.GetComponent<WeaponSwitcher>().weaponList[1])
                                {
                                    anim.Play("Attack_Left_Electric", 2);
                                }
                            }
                            else if (anim.GetFloat("Horizontal") == 1)
                            {
                                if (gameObject.GetComponent<WeaponSwitcher>().weaponHolding == gameObject.GetComponent<WeaponSwitcher>().weaponList[2])
                                {
                                    anim.Play("Attack_Right", 2);
                                }
                                if (gameObject.GetComponent<WeaponSwitcher>().weaponHolding == gameObject.GetComponent<WeaponSwitcher>().weaponList[0])
                                {
                                    anim.Play("Attack_Right_Regular", 2);
                                }
                                if (gameObject.GetComponent<WeaponSwitcher>().weaponHolding == gameObject.GetComponent<WeaponSwitcher>().weaponList[3])
                                {
                                    anim.Play("Attack_Right_Plasma", 2);
                                }
                                if (gameObject.GetComponent<WeaponSwitcher>().weaponHolding == gameObject.GetComponent<WeaponSwitcher>().weaponList[1])
                                {
                                    anim.Play("Attack_Right_Electric", 2);
                                }
                            }
                            else if (anim.GetFloat("Vertical") == 1)
                            {
                                if (gameObject.GetComponent<WeaponSwitcher>().weaponHolding == gameObject.GetComponent<WeaponSwitcher>().weaponList[2])
                                {
                                    anim.Play("Attack_Up", 2);
                                }
                                if (gameObject.GetComponent<WeaponSwitcher>().weaponHolding == gameObject.GetComponent<WeaponSwitcher>().weaponList[0])
                                {
                                    anim.Play("Attack_Up_Regular", 2);
                                }
                                if (gameObject.GetComponent<WeaponSwitcher>().weaponHolding == gameObject.GetComponent<WeaponSwitcher>().weaponList[3])
                                {
                                    anim.Play("Attack_Up_Plasma", 2);
                                }
                                if (gameObject.GetComponent<WeaponSwitcher>().weaponHolding == gameObject.GetComponent<WeaponSwitcher>().weaponList[1])
                                {
                                    anim.Play("Attack_Up_Electric", 2);
                                }
                            }
                        }
                    }

                    }
                



            }
            else
            {
                anim.SetLayerWeight(2, 0);
                anim.SetLayerWeight(1, 1);
               
                    if (Input.GetMouseButtonDown(0))
                    {
                    if (switcher.primaryEquipped && SceneManager.GetActiveScene().name != "Game Cutscene")
                    {
                        // Debug.Log("drip");
                           music.GetComponent<AudioSource>().PlayOneShot(music.music.Find(x => x.name.Contains("Attack")).clip, 0.5f);
                        if (anim.GetCurrentAnimatorStateInfo(1).IsName("Idle_Left"))
                        {
                            if (gameObject.GetComponent<WeaponSwitcher>().weaponHolding == gameObject.GetComponent<WeaponSwitcher>().weaponList[2])
                            {
                                anim.Play("Attack_Left", 1);
                            }
                            if (gameObject.GetComponent<WeaponSwitcher>().weaponHolding == gameObject.GetComponent<WeaponSwitcher>().weaponList[0])
                            {
                                anim.Play("Attack_Left_Regular", 1);
                            }
                            if (gameObject.GetComponent<WeaponSwitcher>().weaponHolding == gameObject.GetComponent<WeaponSwitcher>().weaponList[3])
                            {
                                anim.Play("Attack_Left_Plasma", 1);
                            }
                            if (gameObject.GetComponent<WeaponSwitcher>().weaponHolding == gameObject.GetComponent<WeaponSwitcher>().weaponList[1])
                            {
                                anim.Play("Attack_Left_Electric", 1);
                            }
                        }
                        if (anim.GetCurrentAnimatorStateInfo(1).IsName("Idle"))
                        {
                            if (gameObject.GetComponent<WeaponSwitcher>().weaponHolding == gameObject.GetComponent<WeaponSwitcher>().weaponList[2])
                            {
                                anim.Play("Attack_Down", 1);
                            }
                            if (gameObject.GetComponent<WeaponSwitcher>().weaponHolding == gameObject.GetComponent<WeaponSwitcher>().weaponList[0])
                            {
                                anim.Play("Attack_Down_Regular", 1);
                            }
                            if (gameObject.GetComponent<WeaponSwitcher>().weaponHolding == gameObject.GetComponent<WeaponSwitcher>().weaponList[3])
                            {
                                anim.Play("Attack_Down_Plasma", 1);
                            }
                            if (gameObject.GetComponent<WeaponSwitcher>().weaponHolding == gameObject.GetComponent<WeaponSwitcher>().weaponList[1])
                            {
                                anim.Play("Attack_Down_Electric", 1);
                            }
                        }
                        if (anim.GetCurrentAnimatorStateInfo(1).IsName("Idle_Right"))
                        {
                            if (gameObject.GetComponent<WeaponSwitcher>().weaponHolding == gameObject.GetComponent<WeaponSwitcher>().weaponList[2])
                            {
                                anim.Play("Attack_Right", 1);
                            }
                            if (gameObject.GetComponent<WeaponSwitcher>().weaponHolding == gameObject.GetComponent<WeaponSwitcher>().weaponList[0])
                            {
                                anim.Play("Attack_Right_Regular", 1);
                            }
                            if (gameObject.GetComponent<WeaponSwitcher>().weaponHolding == gameObject.GetComponent<WeaponSwitcher>().weaponList[3])
                            {
                                anim.Play("Attack_Right_Plasma", 1);
                            }
                            if (gameObject.GetComponent<WeaponSwitcher>().weaponHolding == gameObject.GetComponent<WeaponSwitcher>().weaponList[1])
                            {
                                anim.Play("Attack_Right_Electric", 1);
                            }
                        }
                        if (anim.GetCurrentAnimatorStateInfo(1).IsName("Idle_Up"))
                        {
                            if (gameObject.GetComponent<WeaponSwitcher>().weaponHolding == gameObject.GetComponent<WeaponSwitcher>().weaponList[2])
                            {
                                anim.Play("Attack_Up", 1);
                            }
                            if (gameObject.GetComponent<WeaponSwitcher>().weaponHolding == gameObject.GetComponent<WeaponSwitcher>().weaponList[0])
                            {
                                anim.Play("Attack_Up_Regular", 1);
                            }
                            if (gameObject.GetComponent<WeaponSwitcher>().weaponHolding == gameObject.GetComponent<WeaponSwitcher>().weaponList[3])
                            {
                                anim.Play("Attack_Up_Plasma", 1);
                            }
                            if (gameObject.GetComponent<WeaponSwitcher>().weaponHolding == gameObject.GetComponent<WeaponSwitcher>().weaponList[1])
                            {
                                anim.Play("Attack_Up_Electric", 1);
                            }
                        }
                    }
                }

                    if (Input.GetMouseButtonDown(1))
                    {
                    if (switcher.primaryEquipped && SceneManager.GetActiveScene().name != "Game Cutscene")
                    {
                        if (!gameObject.GetComponent<Player>().outOfSpecial)
                        {
                            music.GetComponent<AudioSource>().PlayOneShot(music.music.Find(x => x.name.Contains("Attack")).clip, 0.5f);
                            dealingSpecial = true;
                            GetComponent<Player>().ExecuteSpecial(GetComponent<Player>().specialCost);
                            // Debug.Log("drip");

                            if (anim.GetCurrentAnimatorStateInfo(1).IsName("Idle_Left"))
                            {
                                if (gameObject.GetComponent<WeaponSwitcher>().weaponHolding == gameObject.GetComponent<WeaponSwitcher>().weaponList[2])
                                {
                                    anim.Play("Attack_Left", 1);
                                }
                                if (gameObject.GetComponent<WeaponSwitcher>().weaponHolding == gameObject.GetComponent<WeaponSwitcher>().weaponList[0])
                                {
                                    anim.Play("Attack_Left_Regular", 1);
                                }
                                if (gameObject.GetComponent<WeaponSwitcher>().weaponHolding == gameObject.GetComponent<WeaponSwitcher>().weaponList[3])
                                {
                                    anim.Play("Attack_Left_Plasma", 1);
                                }
                                if (gameObject.GetComponent<WeaponSwitcher>().weaponHolding == gameObject.GetComponent<WeaponSwitcher>().weaponList[1])
                                {
                                    anim.Play("Attack_Left_Electric", 1);
                                }
                            }
                            if (anim.GetCurrentAnimatorStateInfo(1).IsName("Idle"))
                            {
                                if (gameObject.GetComponent<WeaponSwitcher>().weaponHolding == gameObject.GetComponent<WeaponSwitcher>().weaponList[2])
                                {
                                    anim.Play("Attack_Down", 1);
                                }
                                if (gameObject.GetComponent<WeaponSwitcher>().weaponHolding == gameObject.GetComponent<WeaponSwitcher>().weaponList[0])
                                {
                                    anim.Play("Attack_Down_Regular", 1);
                                }
                                if (gameObject.GetComponent<WeaponSwitcher>().weaponHolding == gameObject.GetComponent<WeaponSwitcher>().weaponList[3])
                                {
                                    anim.Play("Attack_Down_Plasma", 1);
                                }
                                if (gameObject.GetComponent<WeaponSwitcher>().weaponHolding == gameObject.GetComponent<WeaponSwitcher>().weaponList[1])
                                {
                                    anim.Play("Attack_Down_Electric", 1);
                                }
                            }
                            if (anim.GetCurrentAnimatorStateInfo(1).IsName("Idle_Right"))
                            {
                                if (gameObject.GetComponent<WeaponSwitcher>().weaponHolding == gameObject.GetComponent<WeaponSwitcher>().weaponList[2])
                                {
                                    anim.Play("Attack_Right", 1);
                                }
                                if (gameObject.GetComponent<WeaponSwitcher>().weaponHolding == gameObject.GetComponent<WeaponSwitcher>().weaponList[0])
                                {
                                    anim.Play("Attack_Right_Regular", 1);
                                }
                                if (gameObject.GetComponent<WeaponSwitcher>().weaponHolding == gameObject.GetComponent<WeaponSwitcher>().weaponList[3])
                                {
                                    anim.Play("Attack_Right_Plasma", 1);
                                }
                                if (gameObject.GetComponent<WeaponSwitcher>().weaponHolding == gameObject.GetComponent<WeaponSwitcher>().weaponList[1])
                                {
                                    anim.Play("Attack_Right_Electric", 1);
                                }
                            }
                            if (anim.GetCurrentAnimatorStateInfo(1).IsName("Idle_Up"))
                            {
                                if (gameObject.GetComponent<WeaponSwitcher>().weaponHolding == gameObject.GetComponent<WeaponSwitcher>().weaponList[2])
                                {
                                    anim.Play("Attack_Up", 1);
                                }
                                if (gameObject.GetComponent<WeaponSwitcher>().weaponHolding == gameObject.GetComponent<WeaponSwitcher>().weaponList[0])
                                {
                                    anim.Play("Attack_Up_Regular", 1);
                                }
                                if (gameObject.GetComponent<WeaponSwitcher>().weaponHolding == gameObject.GetComponent<WeaponSwitcher>().weaponList[3])
                                {
                                    anim.Play("Attack_Up_Plasma", 1);
                                }
                                if (gameObject.GetComponent<WeaponSwitcher>().weaponHolding == gameObject.GetComponent<WeaponSwitcher>().weaponList[1])
                                {
                                    anim.Play("Attack_Up_Electric", 1);
                                }
                            }
                        }
                    }

                    }
                
            }
      
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            anim.SetFloat("Horizontal", movement.x);
            anim.SetFloat("Vertical", movement.y);
            anim.SetFloat("Speed", movement.sqrMagnitude);

        }


    }

    public void SpecialDone()
    {
        dealingSpecial = false;
    }

    private void FixedUpdate()
    {
        if (froze)
        {

        }
        else
        {
            StopCoroutine(Waiting());
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        

        if (!trigger)
        {
            if (collision.tag == "Right Door")
            {
                right = true;
                // cam.transform.position = Vector3.Lerp(cam.transform.position, new Vector3(cam.transform.position.x + 30, 0, 0), 3f * Time.deltaTime);
                //Teleport(cam.transform.position, new Vector3(cam.transform.position.x + 30, 0, 0), 3f);
                //trigger = false;
                gameObject.transform.position = new Vector3(gameObject.transform.position.x + 27.5f, gameObject.transform.position.y, 0);
                if (!trigger)
                {
                    Teleport();
                }
                rb.velocity = Vector3.zero;
                cam.transform.position = new Vector3(cam.transform.position.x + 32.5f, cam.transform.position.y, 0);
                //  cam.GetComponent<Animator>().Play("Camera_Move_Right");
                //Teleport();

                //Teleport();
            }
            if (collision.tag == "Left Door")
            {
                left = true;
                //cam.transform.position = Vector3.Lerp(cam.transform.position, new Vector3(cam.transform.position.x - 30, 0, 0), 3f * Time.deltaTime);
                //Teleport(cam.transform.position, new Vector3(cam.transform.position.x - 30, 0, 0), 3f);
                gameObject.transform.position = new Vector3(gameObject.transform.position.x - 27.5f, gameObject.transform.position.y, 0);
                if (!trigger)
                {
                    Teleport();
                }
                rb.velocity = Vector3.zero;
                cam.transform.position = new Vector3(cam.transform.position.x - 32.5f, cam.transform.position.y, 0);
                // cam.GetComponent<Animator>().Play("Camera_Move_Left");
                //Teleport();

                //Teleport();
            }
            if (collision.tag == "Top Door")
            {
                top = true;
                //Debug.Log("yay!");
                // cam.transform.position = Vector3.Lerp(cam.transform.position, new Vector3(0, cam.transform.position.y + 30, 0), 3f * Time.deltaTime);
                //Teleport(cam.transform.position, new Vector3(0, cam.transform.position.y + 30, 0), 3f);
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 26.5f, 0);
                if (!trigger)
                {
                    Teleport();
                }
                rb.velocity = Vector3.zero;
                cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y + 30.5f, 0);
                //  cam.GetComponent<Animator>().Play("Camera_Move_Up");
                // Teleport();

                //Teleport();
            }
            if (collision.tag == "Bottom Door")
            {
                down = true;
                //cam.transform.position = Vector3.Lerp(cam.transform.position, new Vector3(cam.transform.position.x, cam.transform.position.y - 30, 0), 3f * Time.deltaTime);
                //Teleport(cam.transform.position, new Vector3(0, cam.transform.position.y - 30, 0), 3f);
                froze = true;
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 26.5f, 0);
                if (!trigger)
                {
                    Teleport();
                }
                rb.velocity = Vector3.zero;
                cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y - 30.5f, 0);
                //Vector3 vec = new Vector3(cam.transform.position.x, -30.5f, 0);
                //cam.transform.position = Vector3.Lerp(new Vector3(0, 0, -10.97f), new Vector3(cam.transform.position.x, -30.5f, 0), 0.2f * Time.deltaTime); ;
                // cam.GetComponent<Animator>().Play("Camera_Move_Down");


                //Teleport();
            }
            trigger = true;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        trigger = false;
    }
    void Teleport()
    {
        //if
        StartCoroutine(Waiting());
    }

    IEnumerator CheckAttack()
    {
        attackCheck = true;
        yield return new WaitForSeconds(0.5f);
        attackCheck = false;

    }
    private IEnumerator KnockCoroutine(Rigidbody2D enemy)
    {
        Vector2 forceDirection = enemy.transform.position - transform.position;
        Vector2 force = forceDirection.normalized * thrust;

        enemy.velocity = force;
        yield return new WaitForSeconds(.3f);

        enemy.velocity = new Vector2();
    }

IEnumerator Waiting()
    {
        froze = true;
        yield return new WaitForSeconds(0.5f);
        froze = false;

    }
    void OnEnable()
    {
        //Debug.Log("OnEnable called");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // called second
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //Debug.Log("OnSceneLoaded: " + scene.name);
        //Debug.Log(mode);
        if (GameObject.FindGameObjectWithTag("Cam"))
        {
            cam = GameObject.FindGameObjectWithTag("Cam");
        }


    }

}
