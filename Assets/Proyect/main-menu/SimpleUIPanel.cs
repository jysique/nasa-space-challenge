using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleUIPanel : UIPanel
{

    public GameObject ogle_container;
    public GameObject pegasi_container;

    public GameObject play;
    public GameObject txt_ogle;
    public GameObject txt_pegasi;
    string id;
    public void View(string id)
    {
        ogle_container.SetActive(false);
        pegasi_container.SetActive(false);

        this.id = id;
        //Debug.Log(" simple view " + id);
        switch (id)
        {
            case "ogle":
                ogle_container.SetActive(true);
                break;
            case "pegasi":
                pegasi_container.SetActive(true);
                break;
        }
    }
    public void Go()
    {
        string scene = id == "pegasi" ? "Pegasi" : "Ogle";
        MainMenuManager.instance.GoGameplay(scene);
    }
    public void Show(string id)
    {
        play.SetActive(true);
        switch (id)
        {
            case "ogle":
                txt_ogle.SetActive(true); break;
            case "pegasi":
                txt_pegasi.SetActive(true); break;
        }
    }

}
