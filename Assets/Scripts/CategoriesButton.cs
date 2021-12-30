using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CategoriesButton : RedButton
{

    [SerializeField] public GameManager.Categories myCategory = GameManager.Categories.All;
    Image imageComponent;
    Button buttonComponent;
    TMP_Text textComponent;

    Color backgroundColor;
    Color textColor;

    private void Awake()
    {
        imageComponent = gameObject.GetComponent<Image>();
        buttonComponent = gameObject.GetComponent<Button>();
        textComponent = gameObject.GetComponentInChildren<TMP_Text>();
        backgroundColor = imageComponent.color;
        textColor = textComponent.color;

    }

    public void SwitchVisual()
    {
        Debug.Log(gameObject.name + " / switching");
        imageComponent.color = textColor;
        textComponent.color = backgroundColor;
        buttonComponent.colors.selectedColor.Equals(backgroundColor);

        backgroundColor = imageComponent.color;
        textColor = textComponent.color;
    }

    public override void OnClick()
    {

        GameManager.instance.SwitchCategory(myCategory);
        GameManager.instance.cardsManager.GenerateView();
    }

}
