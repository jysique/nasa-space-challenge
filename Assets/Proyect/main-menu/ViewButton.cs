using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewButton : MonoBehaviour
{
    public SimpleUIPanel panel;
    public string id;
    public void Show()
    {
        panel.Show(id);
    }
}
