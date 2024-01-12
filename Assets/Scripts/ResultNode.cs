using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResultNode : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI percentageText;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI rankText;
    [SerializeField] private Image myImage;
    [SerializeField] private Image background;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void constructBar(Node theNode, int rank)
    {
        //Decide color
        if (rank % 2 == 0)
        {
            //Unity uses RGB 0.0 - 1.0
            background.color = new Color(0.8773f, 0.6117f, 0.4359f);
        }

        rankText.text = "#" + rank;
        nameText.text = theNode.myName;
        percentageText.text = theNode.percentageChosen() + "%";
        myImage.sprite = theNode.myImage;


    }
}
