using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    Animator anim;
    public Transform player;
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = this.GetComponent<Rigidbody2D>();
        StartCoroutine(Motion());
    }


    IEnumerator Motion()
    {
        while(true)
        {
          
            anim.SetBool("GoLeft", true);
            yield return new WaitForSeconds(2.0f);
            anim.SetBool("GoLeft", false);
            anim.SetBool("BottomLeft", true);
            GetComponent<FireBullets>().Fire();
            yield return new WaitForSeconds(2.0f);
            anim.SetBool("BottomLeft", false);

            anim.SetBool("GoUp", true);
            yield return new WaitForSeconds(2.0f);
            anim.SetBool("GoUp", false);
            anim.SetBool("UpperLeft", true);
            GetComponent<FireBullets>().Fire();
            yield return new WaitForSeconds(2.0f);
            anim.SetBool("UpperLeft", false);

            anim.SetBool("GoRight", true);
            yield return new WaitForSeconds(2.0f);
            anim.SetBool("GoRight", false);
            anim.SetBool("UpperRight", true);
            GetComponent<FireBullets>().Fire();
            yield return new WaitForSeconds(2.0f);
            anim.SetBool("UpperRight", false);

            anim.SetBool("GoDown", true);
            yield return new WaitForSeconds(2.0f);
            anim.SetBool("GoDown", false);

            anim.SetBool("BottomRight", true);
            GetComponent<FireBullets>().Fire();
            yield return new WaitForSeconds(2.0f);
            anim.SetBool("BottomRight", false);


        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
