using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlow : MonoBehaviour
{
    public Flowchart flow;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Computer")
        {
            flow.ExecuteBlock("Exploring Computers in Room.");
        }
        if (collision.tag == "Files")
        {
            flow.ExecuteBlock("Look Through Files.");
        }
        if (collision.tag == "Stairs")
        {
            flow.ExecuteBlock("Found Stairs");
        }
        if(collision.tag == "taperecorder")
        {
            flow.ExecuteBlock("Tape Recorder");
        }
        if (collision.tag == "box")
        {
            flow.ExecuteBlock("Box");
        }
        if (collision.tag == "fameis")
        {
            flow.ExecuteBlock("Fameis");
        }
    }

    private void OnCollisionEnter2D(UnityEngine.Collision2D collision)
    {
        if(collision.gameObject.tag == "Sword")
        {
            gameObject.GetComponent<WeaponSwitcher>().weaponHolding = gameObject.GetComponent<WeaponSwitcher>().weaponList[0];
            flow.ExecuteBlock("Regular Sword");
            Destroy(collision.gameObject);
        }
       
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "StairsPrompt")
        {
            flow.StopAllBlocks();
            flow.ExecuteBlock("Go To Stairs");
        }
        if (other.tag == "Computer")
        {
            flow.StopAllBlocks();
            flow.StopBlock("Menu Options");
        }
        if (other.tag == "Files")
        {
            flow.StopAllBlocks();
            flow.StopBlock("Menu Options");
        }
        if (other.tag == "Stairs")
        {
            flow.StopAllBlocks();
            flow.StopBlock("Menu Options");
        }
    }
}
