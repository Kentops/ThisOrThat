using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearNodesButton : MonoBehaviour
{
    public NodeMaster controller;
    public NodeCounter counter;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void clearThemNodes()
    {
        controller.removeAllNodes();
        counter.trackNodes();
    }
}
