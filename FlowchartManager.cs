using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowchartManager : MonoBehaviour
{
    public Flowchart flow;
    bool didItOnce = false;
    // Start is called before the first frame update
    void Start()
    {
        if(!didItOnce)
        {
            DontDestroyOnLoad(flow);
            didItOnce = true;
        }
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
