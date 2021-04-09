using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public Transform player;
    public GameObject exclaimation;
    private Rigidbody2D rb;
    bool follow = false;
    private Vector2 movement;
    public float moveSpeed = 5f;
    public CircleCollider2D range;
    // Start is called before the first frame update
    void Start()
    {
        if (exclaimation)
        {
            exclaimation.SetActive(false);
        }
       
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player && follow)
        {
            Vector3 direction = player.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x);
            rb.rotation = angle;
            direction.Normalize();
            movement = direction;
        }
       

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
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
