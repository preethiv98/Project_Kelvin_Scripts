using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class WeaponSwitcher : MonoBehaviour
{
   // [SerializeField]
    public List<WeaponStats> weaponList;

    //[SerializeField]
    public GameObject primaryObject;

    //[SerializeField]
    public GameObject secondaryObject;

    public bool primaryEquipped, secondaryEquipped;
   // [SerializeField]
    public WeaponStats weaponHolding;

    private void Start()
    {
        if(SceneManager.GetActiveScene().name != "Tutorial")
        {
            weaponHolding = weaponList[0];
            primaryEquipped = true;
        }
        else
        {
            weaponHolding = null;
            //primaryObject.SetActive(false);
            primaryEquipped = false;
        }
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            if(secondaryEquipped)
            {
                Sprite temp;
                temp = primaryObject.GetComponent<Image>().sprite;
                primaryObject.GetComponent<Image>().sprite = secondaryObject.GetComponent<Image>().sprite;
                secondaryObject.GetComponent<Image>().sprite = temp;
                Equip();
            }
          
        }
        if(Input.GetKeyDown(KeyCode.C))
        {
            Drop();
        }
        if (weaponHolding == weaponList[2])
        {
            gameObject.GetComponent<PlayerMovement>().moveSpeed = 4f;
        }
        else if(weaponHolding == weaponList[1])
        {
            gameObject.GetComponent<PlayerMovement>().moveSpeed = 6f;
        }
        else
        {
            gameObject.GetComponent<PlayerMovement>().moveSpeed = 5f;
        }
    }

    void Equip()
    {
        for(int i = 0; i < weaponList.Count; i++)
        {
            if(weaponList[i].weaponSprite == primaryObject.GetComponent<Image>().sprite)
            {
                weaponHolding = weaponList[i];
            }
        }
        
    }

    void Drop()
    {
        if (primaryEquipped && secondaryEquipped)
        {

            for (int i = 0; i < weaponList.Count; i++)
            {
                if (secondaryObject.GetComponent<Image>().sprite == weaponList[i].weaponSprite)
                {
                    Instantiate(weaponList[i].weaponPrefab, new Vector2(gameObject.transform.position.x+1.5f, gameObject.transform.position.y+1.5f), Quaternion.identity).transform.parent = GameObject.Find("Item").transform;
                }
            }
            Image image = secondaryObject.GetComponent<Image>();
            Color c = image.color;
            c.a = 0f;
            image.color = c;
            secondaryEquipped = false;

        }
     
    }

}
