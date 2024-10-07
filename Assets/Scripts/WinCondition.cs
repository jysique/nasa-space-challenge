using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class WinCondition : MonoBehaviour
{
    public static WinCondition instance;
    public int counter;
    public int maxCounter;
    public TMP_Text uiCounter;
    public SpriteRenderer fade;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        counter = 0;
        uiCounter.text = counter + "/" + maxCounter;
    }

    public void AddCounter()
    {
        counter++;
        uiCounter.text = counter + "/" + maxCounter;
        if (counter == maxCounter)
        {
            Invoke("WinGame", 4);
            

        }
    }
    private void WinGame()
    {
        print("GANASTE!!!!");
        //CONECTAR A ESCENA PRINCIPAL
        fade.DOFade(1f, 1f).OnComplete(() => {
            //CONECTAR A ESCENA PRINCIPAL
            if(SaveManager.instance != null)
            {
                SaveManager.instance.SaveSelection();
            }
            SceneManager.LoadScene("MainMenu");
            
        });
    }



    
}
