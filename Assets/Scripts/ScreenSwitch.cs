using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSwitch : MonoBehaviour
{
    public GameObject[] screens;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void disableScreen(int screenNumber)
    {
        screens[screenNumber].SetActive(false);
    }

    public void enableScreen(int screenNumber)
    {
        screens[screenNumber].SetActive(true);
    }

    public void zeroToOne()
    {
        enableScreen(1);
        disableScreen(0);
    }
}
