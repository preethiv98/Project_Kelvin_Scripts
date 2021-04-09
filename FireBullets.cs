using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullets : MonoBehaviour
{
    [SerializeField]
    private int bulletsAmount = 10;

    [SerializeField]
    private bool isDroid;

    [SerializeField]
    private float startAngle = 90f, endAngle = 270f;

    private Vector2 bulletMoveDirection;
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(FireBullet());
        InvokeRepeating("Fire", 0f, 2f);
    }

    //private void FixedUpdate()
    //{
    //    if (gameObject.GetComponent<DroidPatrol>().isRange)
    //    {
    //        StartCoroutine(FireBullet());
    //        isRange = false;
    //    }
    //}

    IEnumerator FireBullet()
    {

        float angleStep = (endAngle - startAngle) / bulletsAmount;
        float angle = startAngle;

        if(isDroid)
        {
            if (gameObject.GetComponent<DroidPatrol>().isRange)
            {

                for (int i = 0; i < bulletsAmount + 1; i++)
                {
                    float bulDirX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
                    float bulDirY = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);

                    Vector3 bulMoveVector = new Vector3(bulDirX, bulDirY, 0f);
                    Vector2 bulDir = (bulMoveVector - transform.position).normalized;

                    if (BulletPool.bulletPoolInstance)
                    {
                        GameObject bul = BulletPool.bulletPoolInstance.GetBullet();
                        bul.transform.position = transform.position;
                        bul.transform.rotation = transform.rotation;
                        bul.transform.rotation = Quaternion.LookRotation(new Vector3(transform.position.x, transform.rotation.y, angleStep));
                        bul.SetActive(true);
                        bul.GetComponent<Bullet>().SetMoveDirection(bulDir);

                        angle += angleStep;
                    }

                }
            }
            yield return new WaitForSeconds(2f);
        }
        else
        {
           
                for (int i = 0; i < bulletsAmount + 1; i++)
                {
                    float bulDirX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
                    float bulDirY = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);

                    Vector3 bulMoveVector = new Vector3(bulDirX, bulDirY, 0f);
                    Vector2 bulDir = (bulMoveVector - transform.position).normalized;

                    if (BulletPool.bulletPoolInstance)
                    {
                        GameObject bul = BulletPool.bulletPoolInstance.GetBullet();
                        bul.transform.position = transform.position;
                        bul.transform.rotation = transform.rotation;
                        bul.transform.rotation = Quaternion.LookRotation(new Vector3(transform.position.x, transform.rotation.y, angleStep));
                        bul.SetActive(true);
                        bul.GetComponent<Bullet>().SetMoveDirection(bulDir);

                        angle += angleStep;
                    }

                }
        }
    
    }
    public void Fire()
    {

        float angleStep = (endAngle - startAngle) / bulletsAmount;
        float angle = startAngle;

        if(isDroid)
        {
            if (gameObject.GetComponent<DroidPatrol>().isRange)
            {

                for (int i = 0; i < bulletsAmount + 1; i++)
                {
                    float bulDirX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
                    float bulDirY = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);

                    Vector3 bulMoveVector = new Vector3(bulDirX, bulDirY, 0f);
                    Vector2 bulDir = (bulMoveVector - transform.position).normalized;


                    GameObject bul = BulletPool.bulletPoolInstance.GetBullet();
                    bul.transform.position = transform.position;
                    bul.transform.rotation = transform.rotation;
                    bul.SetActive(true);
                    bul.GetComponent<Bullet>().SetMoveDirection(bulDir);

                    angle += angleStep;
                }
            }

        }

        else
        {
            for (int i = 0; i < bulletsAmount + 1; i++)
            {
                float bulDirX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
                float bulDirY = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);

                Vector3 bulMoveVector = new Vector3(bulDirX, bulDirY, 0f);
                Vector2 bulDir = (bulMoveVector - transform.position).normalized;


                GameObject bul = BulletPool.bulletPoolInstance.GetBullet();
                bul.transform.position = transform.position;
                bul.transform.rotation = transform.rotation;
                bul.SetActive(true);
                bul.GetComponent<Bullet>().SetMoveDirection(bulDir);

                angle += angleStep;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
