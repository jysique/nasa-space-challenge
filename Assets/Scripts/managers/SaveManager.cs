using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void SaveSelection()
    {
        PlayerPrefs.SetInt("selection", 1);
    }
    
    public void ResetSelection()
    {
        PlayerPrefs.SetInt("selection", 0);
    }
}
