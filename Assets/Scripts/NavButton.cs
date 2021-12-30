using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavButton : MonoBehaviour
{

    public void AddPicture()
    {
        GameManager.instance.addMode = GameManager.AddMode.Image;
        GameManager.instance.CurrentStatus = GameManager.AppStatus.Adding;
    }

    public void Add3D()
    {
        GameManager.instance.addMode = GameManager.AddMode.ThreeD;
        GameManager.instance.CurrentStatus = GameManager.AppStatus.Adding;
    }

    public void ReturnToDefaultMode()
    {
        GameManager.instance.CurrentStatus = GameManager.AppStatus.Default;

    }

    public void DeleteEntry()
    {
        GameManager.instance.CurrentStatus = GameManager.AppStatus.Deleting;
    }



}
