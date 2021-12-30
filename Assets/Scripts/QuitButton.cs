using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitButton : RedButton
{

    public override void OnClick()
    {
        base.OnClick();
        Application.Quit();
    }

}
