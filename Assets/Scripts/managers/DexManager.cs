using Radial_UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DexManager : MonoBehaviour
{

    public static DexManager instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            Awakee();
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public Exoplanet[] exoplanets;
    public Dictionary<string, int> dict_exoplanets;
    void Awakee()
    {
        dict_exoplanets = new Dictionary<string, int>();
        for (int i = 0; i < exoplanets.Length; i++)
        {
            dict_exoplanets.Add(exoplanets[i].name, i);
        }
    }
    public Exoplanet GetExoplanet(string id)
    {
        if(dict_exoplanets.TryGetValue(id,out int value)){
            return exoplanets[value];
        }
        return null;
    }
}
