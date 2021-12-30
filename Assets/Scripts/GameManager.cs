using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


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

    private void UpdateAppStatus()
    {
        switch (currentStatus)
        {
            case AppStatus.Default:
                addWindow.SetActive(false);
                break;
            case AppStatus.Adding:
                addWindow.SetActive(true);
                break;
            case AppStatus.Deleting:
                addWindow.SetActive(false);
                break;
            case AppStatus.ConfirmDelete:
                addWindow.SetActive(false);
                break;
            default:
                break;
        }
    }

    private void Awake()
    {
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
        Screen.SetResolution(2562, 1602, true);
        cardsManager = GameObject.FindGameObjectWithTag("Cards").GetComponent<CardsManager>();
        addWindow = GameObject.FindGameObjectWithTag("AddWindow");

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
    }

}
