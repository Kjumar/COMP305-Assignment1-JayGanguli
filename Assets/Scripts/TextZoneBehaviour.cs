using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextZoneBehaviour : MonoBehaviour
{
    public PanelLogic textPanel;

    public void ShowText()
    {
        textPanel.ShowPanel(true);
    }

    public void HideText()
    {
        textPanel.ShowPanel(false);
    }
}
