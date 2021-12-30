using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AddModeButton : MonoBehaviour
{

    public void Confirm()
    {

        TMP_InputField input = YovaUtilities.FindChildrenWithTag(gameObject, "InputField")[0].GetComponent<TMP_InputField>();
        string name = input.text;
        input.text = "";

        if(name == "")
        {
            name = "New Entry";
        }
        string category = "image";
        if(GameManager.instance.addMode == GameManager.AddMode.ThreeD)
        {
            category = "3D";
        }
        GameManager.instance.AddNewItemToGalery(name, category);
        GameManager.instance.CurrentStatus = GameManager.AppStatus.Default;


    }

}
