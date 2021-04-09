using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
using TMPro;

public class ReportContact : MonoBehaviour
{
    public Flowchart flow;
    public SpawnRoom generation;
    public TextMeshProUGUI mission;
    int count = 1;
    // Start is called before the first frame update
    void Start()
    {
        mission.text = "Find Report 0.2";
        flow = GameObject.FindGameObjectWithTag("Game Flowchart").GetComponent<Flowchart>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(UnityEngine.Collision2D collision)
    {
        if(collision.gameObject.tag == "Report")
        {
            generation.AddComputerPart();
            
            if(generation.currentLevel == 1)
            {
                mission.text = "Find Computer Part 1";
            }
            if (generation.currentLevel == 2)
            {
                mission.text = "Find Computer Part 2";
            }
            if (generation.currentLevel == 5)
            {
                mission.text = "Find Computer Part 4";
            }
            Destroy(collision.gameObject);
        }
        if(collision.gameObject.tag == "Clothes")
        {
            //flow.ExecuteBlock("PickupCloth");
            Destroy(collision.gameObject);
            GameObject.Find("GameFlowchart").GetComponent<Flowchart>().SetBooleanVariable("pickedUpTheCloth", true);
            mission.text = "";
        }
        if (collision.gameObject.tag == "Computer1")
        {
            //flow.ExecuteBlock("Find Report 0.2");
            flow.SetBooleanVariable("ComputerPart1Found",true);
            Destroy(collision.gameObject);
            mission.text = "";
        }
        if (collision.gameObject.tag == "Computer2")
        {
            //flow.ExecuteBlock("Find Report 0.3");
            flow.SetBooleanVariable("ComputerPart2Found", true);
            Destroy(collision.gameObject);
            mission.text = "";
        }
        if (collision.gameObject.tag == "Computer3")
        {
            //flow.ExecuteBlock("FindComputerPart3");
            flow.SetBooleanVariable("ComputerPart3Found", true);
            Destroy(collision.gameObject);
            mission.text = "";
        }
        if(collision.gameObject.tag == "Computer4")
        {
            flow.SetBooleanVariable("ComputerPart4Found", true);
            //flow.ExecuteBlock("FindComputerPart4");
            Destroy(collision.gameObject);
            mission.text = "";
            if(flow.GetBooleanVariable("ComputerPart1Found") && flow.GetBooleanVariable("ComputerPart2Found") && flow.GetBooleanVariable("ComputerPart3Found") && flow.GetBooleanVariable("ComputerPart4Found"))
            {
                flow.SetBooleanVariable("FoundAllParts", true);
            }


        }
        if(collision.gameObject.tag == "Clothes")
        {
            GameObject.Find("Flowchart").GetComponent<Flowchart>().ExecuteBlock("PickupCloth");
            flow.SetBooleanVariable("ClothesFound", true);
            Destroy(collision.gameObject);
            mission.text = "";
        }
    }
}
