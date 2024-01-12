using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AddNodeButton : MonoBehaviour
{
    public TMP_InputField nameText; //These are the input fields!
    public TMP_InputField imageText;// ^
    public Image displayImage;
    public Sprite notFoundDefault;
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

    public void addNode()
    {
        controller.addNode(nameText.text, displayImage.sprite, imageText.text,false);
        //Reseting the screen
        displayImage.sprite = notFoundDefault;
        nameText.text = "";
        imageText.text = "";
        counter.trackNodes();


    }
}
