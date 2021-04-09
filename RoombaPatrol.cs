using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoombaPatrol : MonoBehaviour
{
    public Transform player;
    public GameObject exclaimation;
    private Rigidbody2D rb;
    bool follow = false;
    Animator anim;
    private Vector2 movement;
    public float moveSpeed = 5f;
    public CircleCollider2D range;
    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        exclaimation.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player && follow)
        {
            Vector3 direction = player.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x);
            rb.rotation = angle;
            if(angle >= 0 && angle < 90)
            {
                anim.SetBool("walkingLeft", false);
                anim.SetBool("walkingUp", false);
                anim.SetBool("walkingRight", true);
            }
            if(angle >= 90 && angle <= 180)
            {
                anim.SetBool("walkingRight", false);
                anim.SetBool("walkingUp", false);
                anim.SetBool("walkingLeft", true);
            }
            if(angle > 180 && angle <= 270)
            {
                anim.SetBool("walkingRight", false);
                anim.SetBool("walkingLeft", false);
                anim.SetBool("walkingUp", false);
            }
            if(angle > 270 && angle <= 360)
            {
                anim.SetBool("walkingRight", false);
                anim.SetBool("walkingLeft", false);
                anim.SetBool("walkingUp", true);
            }
            direction.Normalize();
            movement = direction;
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            StartCoroutine(Exclaimation());
            follow = true;
        }
    }
    IEnumerator Exclaimation()
    {
        exclaimation.SetActive(true);
        yield return new WaitForSeconds(2f);
        exclaimation.SetActive(false);
    }

    void moveCharacter(Vector2 direction)
    {
        if (rb.bodyType == RigidbodyType2D.Dynamic)
        {
            rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
        }

    }

    void FixedUpdate()
    {
        moveCharacter(movement);
    }
}
