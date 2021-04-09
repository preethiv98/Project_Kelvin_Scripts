using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Click_Load_Scene : MonoBehaviour
{
    public static bool ending = false;
    
    public string levelToLoad;

    public void Clicky()
    {
            StartCoroutine(LoadScenes());
        
    }

    IEnumerator LoadScenes()
    {

        ending=true;
    
        yield return new WaitForSeconds(1.0f);
        ending=false;
        SceneManager.LoadScene(levelToLoad);
    }


    
}
