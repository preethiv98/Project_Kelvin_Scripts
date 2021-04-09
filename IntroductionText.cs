using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class IntroductionText : MonoBehaviour
{
    public LevelChange change;
    public GameObject text;
    int count = 0;
    public List<string> narrative = new List<string>
    {
        "The year was 20XX.",
        "Following a series of failed governments, Leader XXXX XXX XXXXXX of the Aster Party was elected into office.",
        "Their first order of business was to instill many government regulations for the good of the people.",
        "Such included special programs that allowed citizens to voice their opinions in safe spaces, encouraging the establishment of a trust link between the people and the Aster Party and to ensure to citizens that their voices were being heard.",
        "However, a secret group of hackers and government dissenters, known as the Belvederes, rose up in opposition.",
        "Through their hijacking of top-secret government information and prison breaks of criminals, they spread mass panic among the population.",
        "A war broke out among the Belvederes and the Aster Party, resulting in the government's close victory over the rebellion. This event would later become known as the Citizens' War.",
        "The year is now 2XXX.",
        "With ever-rising dissent among the citizens, the Aster Party decided that precautions were needed in order to ensure the safety of the populace.",
        "Enter the XXXXX Foundation, a biotech company specializing in human VR.",
        "A partnership was created between the XXXXX Foundation and the Aster Party to study human and computer relations.",
        "As a result, Project Kelvin was created.",
        "Through specially-selected volunteers, Project Kelvin hopes to collect necessary data needed to revolutionize current AI systems into helpers for the future members of society.",
         "Through specially-selected volunteers, Project Kelvin hopes to collect necessary data needed to revolutionize current AI systems into helpers for the future members of society."
    };
    // Start is called before the first frame update
    void Start()
    {
        //for(int i = 0; i < count; i++)
        //{
        //    gameObject.GetComponent<Animator>().Play("Fade In");
          
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            change.FadeToNextLevel();
            //SceneManager.LoadScene("Tutorial Cutscene");
        }
    }

    public void LaunchText()
    {
        
        //TMP_InputField tmp = text.GetComponent<TMP_InputField>();
        gameObject.GetComponent<TextMeshProUGUI>().text = narrative[count];
        count++;
    }

    public void Check()
    {
        if (count == narrative.Count)
        {
            SceneManager.LoadScene("Tutorial Cutscene");
        }
    }

    public void Skip()
    {
       
    }
}
