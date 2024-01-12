using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultGenerator : MonoBehaviour
{
    public GameObject template;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addBarToRank(Node theNode, int index)
    {
        GameObject theBar = Instantiate(template);
        theBar.transform.SetParent(transform); //This game object will be the parent
        theBar.GetComponent<ResultNode>().constructBar(theNode, index + 1);
    }

    public void destroyResults()
    {
        int kids = transform.childCount;
        for (int i = kids-1; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

    }
}
