using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;


// Script responsible of the display of the cards.
// It uses the data stored in "galery.json"

public class CardsManager : MonoBehaviour
{
    [SerializeField] private GameObject cardPrefab;

    private void Start()
    {
        GenerateView();
    }

    public void GenerateView()
    {
        YovaUtilities.ClearChildren(transform);
        foreach (var card in GameManager.instance.galery.item_list)
        {
            switch (GameManager.instance.currentCategory)
            {
                case GameManager.Categories.All:
                    AddACardToView(card);
                    break;
                case GameManager.Categories.Image:
                    if(card.category == "image")
                    {
                        AddACardToView(card);
                    }
                    break;
                case GameManager.Categories.ThreeD:
                    if (card.category == "3D")
                    {
                        AddACardToView(card);
                    }
                    break;
                default:
                    break;
            }
        }
    }

    private void AddACardToView(CardData cardData)
    {
        GameObject newCard = Instantiate(cardPrefab, transform);



        newCard.name = cardData.name;
        newCard.transform.Find("Title").GetComponent<TMP_Text>().text = cardData.name;
        newCard.transform.Find("Subtitle").GetComponent<TMP_Text>().text = cardData.category;
        GameObject previewPic = YovaUtilities.FindChildrenWithTag(newCard, "PreviewPic")[0];

        

        /*if (cardData.categorie == "image")
        {*/
            string path = Path.Combine(Application.streamingAssetsPath, "pictures/" + cardData.asset_name);
            Debug.Log("path " + path);
            if (File.Exists(path))
            {
                Texture2D tex = new Texture2D(2, 2);
                byte[] imgData = File.ReadAllBytes(path);
                tex.LoadImage(imgData);
                Vector2 pivot = new Vector2(0.5f, 0.5f);
                Sprite sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), pivot, 100.0f);


                previewPic.GetComponent<Image>().sprite = sprite;
            }
            else
            {
                Destroy(previewPic);
            }

        //}
    }

}
