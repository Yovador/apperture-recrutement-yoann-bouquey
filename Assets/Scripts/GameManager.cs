using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


//The GameManager is a singleton that handle the aspect of the app that are global to the totality of the app.
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [HideInInspector]
    public CardsManager cardsManager;
    public enum Categories { All, Image, ThreeD };
    [HideInInspector]
    public Categories currentCategory = Categories.All;
    [HideInInspector]
    public Galery galery;
    [HideInInspector]
    public enum AddMode { Image, ThreeD};
    [HideInInspector]
    public AddMode addMode = AddMode.Image;
    public enum AppStatus { Default, Adding, Deleting, ConfirmDelete }



    public AppStatus CurrentStatus {
        get { return CurrentStatus; }
        set { 
            currentStatus = value;
            UpdateAppStatus();
        } 
    }

    AppStatus currentStatus = AppStatus.Default;

    private GameObject addWindow;
    private GameObject deleteWindow;
    [HideInInspector]
    public int deleteIndex = 0;

    GameObject deleteEntryButton;
    [SerializeField] Color redColor;

    Color textBaseColor;
    Color pictoBaseColor;

    private void UpdateAppStatus()
    {
        switch (currentStatus)
        {
            case AppStatus.Default:
                addWindow.SetActive(false);
                deleteWindow.SetActive(false);
                StartCoroutine( ChangeDeleteElementActive(false) );
                break;
            case AppStatus.Adding:
                addWindow.SetActive(true);
                deleteWindow.SetActive(false);
                StartCoroutine( ChangeDeleteElementActive(false) );
                break;
            case AppStatus.Deleting:
                addWindow.SetActive(false);
                deleteWindow.SetActive(false);
                StartCoroutine( ChangeDeleteElementActive(true) );
                break;
            case AppStatus.ConfirmDelete:
                addWindow.SetActive(false);
                deleteWindow.SetActive(true);
                StartCoroutine( ChangeDeleteElementActive(true) );
                break;
        }
        cardsManager.GenerateView();
    }


    IEnumerator ChangeDeleteElementActive(bool status)
    {
        Debug.Log("Delete element");
        yield return new WaitForEndOfFrame();

        List<GameObject> deleteElements = new List<GameObject>(GameObject.FindGameObjectsWithTag("DeleteElement"));
        foreach (var element in deleteElements)
        {
            element.SetActive(status);
            Debug.Log("Delete " + element + "element status" + status + "current one : " + element.activeSelf );
        }


        if (status)
        {
            deleteEntryButton.GetComponent<Image>().color = redColor;
            YovaUtilities.FindChildrenWithTag(deleteEntryButton, "PictoDelete")[0].GetComponent<Image>().color = Color.white;
            deleteEntryButton.GetComponentInChildren<TMP_Text>().color = Color.white;

        }
        else
        {

            deleteEntryButton.GetComponent<Image>().color = Color.white;
            YovaUtilities.FindChildrenWithTag(deleteEntryButton, "PictoDelete")[0].GetComponent<Image>().color = pictoBaseColor;
            deleteEntryButton.GetComponentInChildren<TMP_Text>().color = textBaseColor;

        }
    }

    private void Awake()
    {
        Screen.SetResolution(2562, 1602, true);

        galery = JSONManager.ReadJson();
        if(galery == null)
        {
            galery = new Galery();
            galery.item_list = new List<CardData>();
        }

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        Application.targetFrameRate = 60;
        cardsManager = GameObject.FindGameObjectWithTag("Cards").GetComponent<CardsManager>();
        addWindow = GameObject.FindGameObjectWithTag("AddWindow");
        deleteWindow = GameObject.FindGameObjectWithTag("DeleteWindow");
        deleteEntryButton = GameObject.FindGameObjectWithTag("DeleteEntryButton");
        pictoBaseColor  = YovaUtilities.FindChildrenWithTag(deleteEntryButton, "PictoDelete")[0].GetComponent<Image>().color;
        textBaseColor = deleteEntryButton.GetComponentInChildren<TMP_Text>().color;
            
    }

    private void Start()
    {
        SwitchCategory(currentCategory);
        UpdateAppStatus();

    }

    public void AddNewItemToGalery(string name, string category = "image")
    {
        CardData newCardData = new CardData();
        newCardData.name = name;
        newCardData.category = category;
        if(category == "image")
        {
            newCardData.asset_name = "visuel"  + Random.Range(1, 12) + ".png";
            Debug.Log(newCardData.asset_name);
        }
        galery.item_list.Add(newCardData);
        JSONManager.WriteJson();
    }

    public void DeleteGaleryItem(int index)
    {
        galery.item_list.RemoveAt(index);
        JSONManager.WriteJson();

    }

    public void SwitchCategory(Categories newCategory)
    {
        List<CategoriesButton> categoriesButtons = new List<CategoriesButton>(FindObjectsOfType<CategoriesButton>());

        foreach (var btn in categoriesButtons)
        {
            if(btn.myCategory == currentCategory)
            {
                btn.SwitchVisual();
            }
            else
            {
                if (btn.myCategory == newCategory)
                {
                    btn.SwitchVisual();
                }
            }


        }

        currentCategory = newCategory;
        UpdateAppStatus();
    }

}
