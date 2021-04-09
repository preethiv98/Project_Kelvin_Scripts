using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;
using Fungus;

public class Weapon : MonoBehaviour
{
    bool collectedLongSword;
    public GameObject text;

    private MusicManager music;

    private void Awake()
    {
            music = GameObject.FindGameObjectWithTag("Music").GetComponent<MusicManager>();      
    }

    private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        if(collision.tag == "Long Sword")
        {
          
            if (!gameObject.GetComponent<WeaponSwitcher>().secondaryEquipped && gameObject.GetComponent<WeaponSwitcher>().primaryEquipped)
            {
                music.GetComponent<AudioSource>().PlayOneShot(music.music.Find(x => x.name.Contains("ItemPickup")).clip, 0.5f);
                gameObject.GetComponent<WeaponSwitcher>().secondaryObject.GetComponent<Image>().sprite = gameObject.GetComponent<WeaponSwitcher>().weaponList[2].weaponSprite;
                Image image = gameObject.GetComponent<WeaponSwitcher>().secondaryObject.GetComponent<Image>();
                Color c = image.color;
                c.a = 1f;
                image.color = c;
                gameObject.GetComponent<WeaponSwitcher>().secondaryEquipped = true;
                Destroy(collision.gameObject);
            }
            else if (!gameObject.GetComponent<WeaponSwitcher>().secondaryEquipped)
            {
                music.GetComponent<AudioSource>().PlayOneShot(music.music.Find(x => x.name.Contains("ItemPickup")).clip, 0.5f);
                gameObject.GetComponent<WeaponSwitcher>().primaryObject.GetComponent<Image>().sprite = gameObject.GetComponent<WeaponSwitcher>().weaponList[2].weaponSprite;
                gameObject.GetComponent<WeaponSwitcher>().weaponHolding = gameObject.GetComponent<WeaponSwitcher>().weaponList[2];
                Image image = gameObject.GetComponent<WeaponSwitcher>().primaryObject.GetComponent<Image>();
                Color c = image.color;
                c.a = 1f;
                image.color = c;
                //gameObject.GetComponent<WeaponSwitcher>().secondaryEquipped = true;
                //gameObject.GetComponent<WeaponSwitcher>().
                Destroy(collision.gameObject);
            }
          else
            {
                if(text)
                {
                    text.SetActive(true);
                }
               
            }

            //gameObject.GetComponent<WeaponSwitcher>().

        }
        if (collision.tag == "Sword")
        {
            
            if (!gameObject.GetComponent<WeaponSwitcher>().secondaryEquipped && gameObject.GetComponent<WeaponSwitcher>().primaryEquipped && SceneManager.GetActiveScene().name != "Game Cutscene")
            {
                music.GetComponent<AudioSource>().PlayOneShot(music.music.Find(x => x.name.Contains("ItemPickup")).clip, 0.5f);
                gameObject.GetComponent<WeaponSwitcher>().secondaryObject.GetComponent<Image>().sprite = gameObject.GetComponent<WeaponSwitcher>().weaponList[0].weaponSprite;
                Image image = gameObject.GetComponent<WeaponSwitcher>().secondaryObject.GetComponent<Image>();
                Color c = image.color;
                c.a = 1f;
                image.color = c;
                gameObject.GetComponent<WeaponSwitcher>().secondaryEquipped = true;
                Destroy(collision.gameObject);
                //gameObject.GetComponent<WeaponSwitcher>().

            }
            else if(!gameObject.GetComponent<WeaponSwitcher>().primaryEquipped && SceneManager.GetActiveScene().name != "Game Cutscene")
            {

                music.GetComponent<AudioSource>().PlayOneShot(music.music.Find(x => x.name.Contains("ItemPickup")).clip, 0.5f);
                gameObject.GetComponent<WeaponSwitcher>().primaryObject.GetComponent<Image>().sprite = gameObject.GetComponent<WeaponSwitcher>().weaponList[0].weaponSprite;
                gameObject.GetComponent<WeaponSwitcher>().weaponHolding = gameObject.GetComponent<WeaponSwitcher>().weaponList[0];
                Image image = gameObject.GetComponent<WeaponSwitcher>().primaryObject.GetComponent<Image>();
                Color c = image.color;
                c.a = 1f;
                image.color = c;
                gameObject.GetComponent<WeaponSwitcher>().primaryEquipped = true;
                Destroy(collision.gameObject);
                //gameObject.GetComponent<WeaponSwitcher>().secondaryEquipped = true;
                //gameObject.GetComponent<WeaponSwitcher>().
                Destroy(collision.gameObject);
            }
            else
            {
                if (text)
                {
                    text.SetActive(true);
                }
            }

        }
        if (collision.tag == "Plasma Sword")
        {
            
            if (!gameObject.GetComponent<WeaponSwitcher>().secondaryEquipped && gameObject.GetComponent<WeaponSwitcher>().primaryEquipped)
            {
                music.GetComponent<AudioSource>().PlayOneShot(music.music.Find(x => x.name.Contains("ItemPickup")).clip, 0.5f);
                gameObject.GetComponent<WeaponSwitcher>().secondaryObject.GetComponent<Image>().sprite = gameObject.GetComponent<WeaponSwitcher>().weaponList[3].weaponSprite;
                Image image = gameObject.GetComponent<WeaponSwitcher>().secondaryObject.GetComponent<Image>();
                Color c = image.color;
                c.a = 1f;
                image.color = c;
                Destroy(collision.gameObject);
                gameObject.GetComponent<WeaponSwitcher>().secondaryEquipped = true;
            }
            else if (!gameObject.GetComponent<WeaponSwitcher>().secondaryEquipped)
            {
                music.GetComponent<AudioSource>().PlayOneShot(music.music.Find(x => x.name.Contains("ItemPickup")).clip, 0.5f);
                gameObject.GetComponent<WeaponSwitcher>().primaryObject.GetComponent<Image>().sprite = gameObject.GetComponent<WeaponSwitcher>().weaponList[3].weaponSprite;
                gameObject.GetComponent<WeaponSwitcher>().weaponHolding = gameObject.GetComponent<WeaponSwitcher>().weaponList[3];
                Image image = gameObject.GetComponent<WeaponSwitcher>().primaryObject.GetComponent<Image>();
                Color c = image.color;
                c.a = 1f;
                image.color = c;
                gameObject.GetComponent<WeaponSwitcher>().primaryEquipped = true;
                //gameObject.GetComponent<WeaponSwitcher>().secondaryEquipped = true;
                //gameObject.GetComponent<WeaponSwitcher>().
                Destroy(collision.gameObject);
            }
            else
            {
                if (text)
                {
                    text.SetActive(true);
                }
            }
            //gameObject.GetComponent<WeaponSwitcher>().

        }
        if (collision.tag == "Electric Sword")
        {
            
            if (!gameObject.GetComponent<WeaponSwitcher>().secondaryEquipped && gameObject.GetComponent<WeaponSwitcher>().primaryEquipped)
            {
                music.GetComponent<AudioSource>().PlayOneShot(music.music.Find(x => x.name.Contains("ItemPickup")).clip, 0.5f);
                gameObject.GetComponent<WeaponSwitcher>().secondaryObject.GetComponent<Image>().sprite = gameObject.GetComponent<WeaponSwitcher>().weaponList[1].weaponSprite;
                Image image = gameObject.GetComponent<WeaponSwitcher>().secondaryObject.GetComponent<Image>();
                Color c = image.color;
                c.a = 1f;
                image.color = c;

                //gameObject.GetComponent<WeaponSwitcher>().
                Destroy(collision.gameObject);
                gameObject.GetComponent<WeaponSwitcher>().secondaryEquipped = true;
            }
            else if (!gameObject.GetComponent<WeaponSwitcher>().secondaryEquipped)
            {
                music.GetComponent<AudioSource>().PlayOneShot(music.music.Find(x => x.name.Contains("ItemPickup")).clip, 0.5f);
                gameObject.GetComponent<WeaponSwitcher>().primaryObject.GetComponent<Image>().sprite = gameObject.GetComponent<WeaponSwitcher>().weaponList[1].weaponSprite;
                gameObject.GetComponent<WeaponSwitcher>().weaponHolding = gameObject.GetComponent<WeaponSwitcher>().weaponList[1];
                Image image = gameObject.GetComponent<WeaponSwitcher>().primaryObject.GetComponent<Image>();
                Color c = image.color;
                c.a = 1f;
                image.color = c;
                gameObject.GetComponent<WeaponSwitcher>().primaryEquipped = true;
                //gameObject.GetComponent<WeaponSwitcher>().secondaryEquipped = true;
                //gameObject.GetComponent<WeaponSwitcher>().
                Destroy(collision.gameObject);
            }
            else
            {
                if (text)
                {
                    text.SetActive(true);
                }
            }
        }
    }

    private void OnCollisionEnter2D(UnityEngine.Collision2D collision)
    {
        if(collision.gameObject.tag == "Junk")
        {
            if(gameObject.GetComponent<WeaponSwitcher>().primaryObject.GetComponent<Image>().sprite == gameObject.GetComponent<WeaponSwitcher>().weaponList[2].weaponSprite)
            {
                Destroy(collision.gameObject);
            }
              
            
        }
    }

    private void OnCollisionExit2D(UnityEngine.Collision2D collision)
    {
        if (text)
        {
            text.SetActive(false);
        }

    }

}
