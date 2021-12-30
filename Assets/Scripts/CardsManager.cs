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
        for (int i = 0; i < GameManager.instance.galery.item_list.Count; i++)
        {
            var card = GameManager.instance.galery.item_list[i];
            switch (GameManager.instance.currentCategory)
            {
                case GameManager.Categories.All:
                    AddACardToView(card, i);
                    break;
                case GameManager.Categories.Image:
                    if(card.category == "image")
                    {
                        AddACardToView(card, i);
                    }
                    break;
                case GameManager.Categories.ThreeD:
                    if (card.category == "3D")
                    {
                        AddACardToView(card, i);
                    }
                    break;
                default:
                    break;
            }
        }
    }

    private void AddACardToView(CardData cardData, int index)
    {
        GameObject newCard = Instantiate(cardPrefab, transform);



        newCard.name = cardData.name;
        newCard.transform.Find("Title").GetComponent<TMP_Text>().text = cardData.name;
        newCard.transform.Find("Subtitle").GetComponent<TMP_Text>().text = cardData.category;
        GameObject previewPic = YovaUtilities.FindChildrenWithTag(newCard, "PreviewPic")[0];
        CardBehaviour cardBehaviour = newCard.GetComponent<CardBehaviour>();
        cardBehaviour.myIndex = index;



        if (cardData.category == "image")
        {
        string path = Path.Combine(Application.streamingAssetsPath, "pictures/" + cardData.asset_name);
            Debug.Log("path " + path);
            if (File.Exists(path))
            {
                Texture2D tex = new Texture2D(2, 2);
                byte[] imgData = File.ReadAllBytes(path);
                tex.LoadImage(imgData);
                previewPic.GetComponent<RawImage>().texture = tex;
            }
            else
            {
                Destroy(previewPic);
            }

        }
    }

}
