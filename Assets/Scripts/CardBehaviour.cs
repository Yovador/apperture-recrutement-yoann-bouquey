using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBehaviour : MonoBehaviour
{
    [HideInInspector] public int myIndex;

    public void OnDelete()
    {
        GameManager.instance.deleteIndex = myIndex;
        GameManager.instance.CurrentStatus = GameManager.AppStatus.ConfirmDelete;
    }

}
