using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Networking;

public class ApplyImageButton : MonoBehaviour
{
    public TMP_InputField url;
    public Image imageHolder;
    public Sprite NotFoundDefault;
    public Button escapeButton;
    public int count = 0;
    public int max;
    public GameObject loadingScreen;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void getImage()
    {
        GetComponent<Button>().interactable = false;
        StartCoroutine(downloadImage(url.text, null));
    }

    public void setImage(Node theNode)
    {
        //Used to apply image from url rather than displayed image
        //This will be for mass-imports.
        if(theNode.myAddress == " ")
        {
            theNode.myImage = NotFoundDefault;
            load();
            return;
        }
        //GetComponent<Button>().interactable = false;
        //escapeButton.interactable = false;
        StartCoroutine(downloadAndSet(theNode.myAddress, theNode));
    }

    public void load(bool start = false, int ceiling = 0)
    {
        if (start)
        {
            max = ceiling;
            loadingScreen.SetActive(true);
        }
        else
        {
            count++;
            if(count == max)
            {
                loadingScreen.SetActive(false);
                count = 0;
            }
        }
    }

    IEnumerator downloadImage(string mediaURL, Node theNode)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(mediaURL);
        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.ConnectionError 
            || request.result == UnityWebRequest.Result.ProtocolError)
        {
            //Image could not load
            Debug.Log("Image not found");
            imageHolder.sprite = NotFoundDefault;
            //Maybe play a negative sound?
            if (theNode != null)
            {
                theNode.myImage = NotFoundDefault;
            }
        }
        else
        {
            Debug.Log("Found it!");
            Texture theImage = ((DownloadHandlerTexture)request.downloadHandler).texture;
            //Convert the texture into a sprite
            if(theImage == null)
            {
                imageHolder.sprite = NotFoundDefault;
                if(theNode != null)
                {
                    theNode.myImage = NotFoundDefault;
                }
                Debug.Log("Image Not Found");
            }
            else
            {
                Sprite newSprite = Sprite.Create((Texture2D)theImage,
                    new Rect(0, 0, theImage.width, theImage.height), new Vector2(0.5f, 0.5f));
                imageHolder.sprite = newSprite;
                if (theNode != null)
                {
                    theNode.myImage = newSprite;
                }
            }
           
        }
        GetComponent<Button>().interactable = true;
    }

    IEnumerator downloadAndSet(string mediaURL, Node theNode)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(mediaURL);
        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.ConnectionError
            || request.result == UnityWebRequest.Result.ProtocolError)
        {
            //Image could not load
            theNode.myImage = NotFoundDefault;
        }
        else
        {
            Texture theImage = ((DownloadHandlerTexture)request.downloadHandler).texture;
            //Convert the texture into a sprite
            if (theImage == null)
            {
                theNode.myImage = NotFoundDefault;
            }
            else
            {
                Sprite newSprite = Sprite.Create((Texture2D)theImage,
                    new Rect(0, 0, theImage.width, theImage.height), new Vector2(0.5f, 0.5f));
                theNode.myImage = newSprite;
            }

        }
        GetComponent<Button>().interactable = true;
        escapeButton.interactable = true;
        load();
    }

}
