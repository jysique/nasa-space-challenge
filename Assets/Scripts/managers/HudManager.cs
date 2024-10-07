using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HudManager : MonoBehaviour
{
    public static HudManager instance;
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
    }
    void Awakee()
    {
        radialMenu.gameObject.SetActive(active);
    }

    bool active = false;
    public Radial_UI.RadialMenu radialMenu;
    void Update()
    {
        RespondToSpaceInput();
    }
    void RespondToSpaceInput()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (radialMenu.gameObject.activeInHierarchy)
            {
                Time.timeScale = 1;

            }
            active = !active;
            radialMenu.gameObject.SetActive(active);
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (radialMenu.gameObject.activeInHierarchy)
            {
                Time.timeScale = 1;
                Invoke("TurnOffMenu", 0.3f);

            }
        }
    }
    void TurnOffMenu()
    {
        active = false;
        radialMenu.gameObject.SetActive(false);
    }
}
