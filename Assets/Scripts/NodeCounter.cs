using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NodeCounter : MonoBehaviour
{
    private TextMeshProUGUI counter;
    public NodeMaster controller;

    // Start is called before the first frame update
    void Start()
    {
        counter = gameObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void trackNodes()
    {
        counter.text = "Nodes: " + controller.numberOfNodes();
    }
}
