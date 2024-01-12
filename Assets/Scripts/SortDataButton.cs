using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SortDataButton : MonoBehaviour
{
    public NodeMaster controller;
    private Button myButton;
    public bool percise = false;

    // Start is called before the first frame update
    void Start()
    {
        myButton = gameObject.GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        if(percise == false && controller.has2Nodes == true)
        {
            myButton.interactable = true;
        }
        else if(controller.has2Nodes == false && myButton.interactable == true)
        {
            //applies to both
            myButton.interactable = false;
        }
        else if(percise == true && controller.isRandom == true)
        {
            myButton.interactable = false;
        }
        else
        {
            myButton.interactable = true;
        }

        
    }

    //Called on click
    public void enterTheChamber(bool random)
    {
        if(random == false)
        {
            controller.precisionSetUp();
            return;
        }
        controller.startQuestioning();
    }
}
