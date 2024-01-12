using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using Random = UnityEngine.Random;

public class NodeMaster : MonoBehaviour
{
    public List<Node> nodeList = new List<Node>();
    public bool has2Nodes = false;
    public bool isRandom = false;
    [SerializeField] private Node[] nodeArray = null;
    private Node[][] nodeMatrix = null;

    private Node node1;
    private Node node2;
    public TextMeshProUGUI thing1;
    public TextMeshProUGUI thing2;
    public Image image1;
    public Image image2;
    public ScreenSwitch resultSwitch;
    private int currentNode;
    private int currentOpponent = 0;

    public ResultGenerator barMaker;
    private bool newResults = true;
    public TMP_InputField resultExportScreen;
    public GameObject loadingScreen;

    public TMP_InputField nodeExportField;
    public TMP_InputField nodeInputField;
    public ApplyImageButton confirmButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void removeAllNodes()
    {
        has2Nodes = false;
        nodeArray = null; //This needs to be before the list clearing?
        nodeMatrix = null;
        isRandom = false;
        nodeList.Clear();
        //Destroy all children!
        int kids = transform.childCount; //To keep things constant
        for (int i = kids-1; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
            //Destroys children in reverse order to account for their
            //delayed destruction and changing indices
        }
        //BigO(n)
    }

    public int numberOfNodes()
    {
        return nodeList.Count;
    }

    public void addNode(string name, Sprite image, string address, bool import)
    {
        
        GameObject temp = new GameObject();
        //Make node objects children of the Node Master
        temp.transform.parent = gameObject.transform;
        Node newNode = temp.AddComponent<Node>();
        newNode.construct(name, image, address);
        nodeList.Add(newNode);
        nodeArray = null;
        nodeMatrix = null;
        if(numberOfNodes() > 1)
        {
            has2Nodes = true;
        }
        newResults = true;
        
    }

    public void startQuestioning()
    {
        //Precondition: There are two or more nodes
        //Check if data is new
        if(nodeArray == null)
        {
            //Create the array
            nodeArray = nodeList.ToArray();
            barMaker.destroyResults();
        }
        //Good to go

        isRandom = true;
        node1 = nodeArray[Random.Range(0, nodeArray.Length)];
        node2 = node1;
        while (node2.Equals(node1))
        {
            //Guarantees node2 is not node1
            node2 = nodeArray[Random.Range(0, nodeArray.Length)];
        }

        thing1.text = node1.myName;
        thing2.text = node2.myName;
        image1.sprite = node1.myImage;
        image2.sprite = node2.myImage;


    }

    public void option1Chosen()
    {
        node1.timesSeen += 1;
        node2.timesSeen += 1;
        node1.timesChosen += 1;
        newResults = true;
        if(isRandom == true)
        {
            startQuestioning();
            return;
        }
        //else
        //Set up next question
        currentOpponent++;
        if (currentOpponent >= nodeMatrix[currentNode].Length)
        {
            currentOpponent = 0;
            currentNode++;
        }
        preciseQuestion();
       
    }
    public void option2Chosen()
    {
        node1.timesSeen += 1;
        node2.timesSeen += 1;
        node2.timesChosen += 1;
        newResults = true;
        if(isRandom == true)
        {
            startQuestioning();
            return;
        }
        //else
        //Set up next question
        currentOpponent++;
        if (currentOpponent >= nodeMatrix[currentNode].Length)
        {
            currentOpponent = 0;
            currentNode++;
        }
        preciseQuestion();
    }
    public void calculateResults()
    {
        //Start loading
        //loadingScreen.SetActive(true);

        if(newResults == true)
        {
            barMaker.destroyResults();
            mergeSort(nodeArray);
            for (int i = 0; i < nodeArray.Length; i++)
            {
                barMaker.addBarToRank(nodeArray[i], i);
            }
            newResults = false;
        }

        //End loading
        //loadingScreen.SetActive(false);
    }

    public void exportResults()
    {
        if(resultExportScreen != null)
        {
           string resultText = "This or That Ranking:\n";
           for (int i = 0; i < nodeArray.Length; i++)
           {
               string temp = i + 1 + ") " + nodeArray[i].myName + " " +
                 nodeArray[i].percentageChosen() + "%\n";
               resultText += temp;
           }
           resultExportScreen.text = resultText;
        }
            
    }

    public void exportNodes()
    {
        if(nodeArray == null)
        {
            nodeArray = nodeList.ToArray();
        }
        string finalText = "*$*";
        for (int i = 0; i < nodeArray.Length; i++)
        {
            if (nodeArray[i].myName == "")
            {
                nodeArray[i].myName = " ";
            }
            string temp = nodeArray[i].myName;

            if (nodeArray[i].myAddress == "")
            {
                nodeArray[i].myAddress = " ";
            }
            temp += "$?" + nodeArray[i].myAddress + "$|";
            finalText += temp;
        }

        //All done
        nodeExportField.text = finalText;
    }

    public void importNodes()
    {
        string theText = nodeInputField.text;
        //Check if the import is acceptable
        if (theText.IndexOf("*$*") != 0)
        {
            nodeInputField.text = "Not a valid import";
            return;
        }
        theText = theText.Substring(3); //Remove the starting bit
        string[] individuals = theText.Split("$|");
        //Set loading screen
        confirmButton.load(true, individuals.Length -1);

        for (int i = 0; i < individuals.Length-1; i++)
        {
            string[] nameAndNumber = individuals[i].Split("$?");
            //Start making the node
            GameObject temp = new GameObject();
            temp.transform.SetParent(transform);
            Node newNode = temp.AddComponent<Node>();
            //Debug.Log("<" + nameAndNumber[1] + ">");
            newNode.construct(nameAndNumber[0], null, nameAndNumber[1]);
            //Set the image
            confirmButton.setImage(newNode);
            nodeList.Add(newNode);
        }

        //All done
        newResults = true;
        nodeArray = null;
        if(nodeList.Count > 1)
        {
            has2Nodes = true;
        }
        nodeInputField.text = "Success!";
    }

    public void precisionSetUp()
    {

        //nodeArray must be reset to preserve order
        nodeArray = nodeList.ToArray();
        if(nodeMatrix == null)
        {
            nodeMatrix = new Node[nodeArray.Length][];
            for(int i = 0; i < nodeArray.Length; i++)
            {
                Node[] temp = new Node[nodeArray.Length - (i + 1)];
                for (int j = 0; j < nodeArray.Length - (i+1); j++)
                {
                    temp[j] = nodeArray[j + i + 1];
                }
                nodeMatrix[i] = temp;
            }

            currentNode = 0;
            currentOpponent = 0;
        }
        //Done with that

        preciseQuestion();
    }

    private void preciseQuestion()
    {
        //Precondition
        if(currentNode >= nodeArray.Length - 1)
        {   
            //Stop sorting
            resultSwitch.zeroToOne();
            calculateResults();
            return;
        }

        isRandom = false;
        node1 = nodeArray[currentNode];
        node2 = nodeMatrix[currentNode][currentOpponent];

        


        thing1.text = node1.myName;
        thing2.text = node2.myName;
        image1.sprite = node1.myImage;
        image2.sprite = node2.myImage;

    }
    public void clearAllNodeInfo()
    {
        if(nodeArray == null)
        {
            nodeArray = nodeList.ToArray();
        }
        for(int i = 0; i < nodeArray.Length; i++)
        {
            nodeArray[i].clearData();
        }
        isRandom = false;
        currentNode = 0;
        nodeArray = nodeList.ToArray(); //Set back to original order
    }

    //Sort the array in O(n * log(n) ) operations
    public Node[] mergeSort(Node[] array)
    {
        //Base Step
        if (array.Length < 2)
        {
            return array;
        }
        //Reduction Step
        int n = array.Length;
        int firstHalf = n / 2;
        int secondHalf = n/2;
        if(n%2 == 1)
        {
            secondHalf += 1;
        }

        //Split the array in half
        Node[] a0 = new Node[firstHalf];
        for (int i = 0; i < firstHalf; i++)
        {
            a0[i] = array[i];
        }
        Node[] a1 = new Node[secondHalf];
        for (int i = 0; i < secondHalf; i++)
        {
            a1[i] = array[i + firstHalf];
        }

        //Recursion
        a0 = mergeSort(a0);
        a1 = mergeSort(a1);

        //Sort
        merge(array, a0, a1);
        return array;

    }

    //Sorts greatest to least
    private void merge(Node[] array, Node[] a0, Node[] a1)
    {
        int i0 = 0;
        int i1 = 0;
        int n = array.Length;

        for(int i = 0; i < n; i++)
        {
            if(i0 == a0.Length)
            {
                array[i] = a1[i1];
                i1++;
            }
            else if (i1 == a1.Length)
            {
                array[i] = a0[i0];
                i0++;
            }
            else if (a0[i0].percentageChosen() >= a1[i1].percentageChosen())
            {
                array[i] = a0[i0];
                i0++;
            }
            else
            {
                array[i] = a1[i1];
                i1++;
            }
        }
    }
}
