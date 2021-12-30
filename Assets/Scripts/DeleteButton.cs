using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteButton : RedButton
{

    public void ConfirmDelete()
    {
        GameManager.instance.DeleteGaleryItem(GameManager.instance.deleteIndex);
        GameManager.instance.CurrentStatus = GameManager.AppStatus.Default;
    }

    public void RefuseDelete()
    {
        GameManager.instance.CurrentStatus = GameManager.AppStatus.Default;
    }

}
