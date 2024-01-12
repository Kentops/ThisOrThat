using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    // Start is called before the first frame update
    public string myName;
    public Sprite myImage;
    public string myAddress;
    public int timesSeen = 0;
    public int timesChosen = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void construct(string name, Sprite image, string address)
    {
        myName = name;
        myImage = image;
        myAddress = address;
    }
    public int percentageChosen()
    {
        if(timesSeen == 0)
        {
            return 0;
        }
        return (int) ( ((float)timesChosen / timesSeen) * 100) ;
        //Will return two decimal places. It doesn't
    }

    public void clearData()
    {
        timesSeen = 0;
        timesChosen = 0;
    }

    //Constructor
    /*public Node(string name, Sprite image)
    {
        myName = name;
        myImage = image;
    }*/
}
