using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuManager : MonoBehaviour
{
    //FIRST PANEL
    public const string MENU = "menu";
    public const string QUIT = "quit";

    //SECOND PANEL
    public const string CREDITS = "credits";
    public const string PLAY = "play";
    public const string BACK = "back";
    public const string BACK_CREDITS = "back_credits";
    public const string BACK_GALERY = "back_galery";
    public const string BACK_VIEW = "back_view";
    public const string FINAL_PLAY = "final_play";

    public static MainMenuManager instance;
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
        main_cg = GetComponent<CanvasGroup>();
        loading.SetActive(PlayerPrefs.GetInt("selection") == 1);
    }
    private void Start()
    {
        if(PlayerPrefs.GetInt("selection") == 1)
        {
            loading.SetActive(false);
            mainPanel.gameObject.SetActive(false);
            OnClick(PLAY);
            SaveManager.instance.ResetSelection();
        }
    }
    CanvasGroup main_cg;
    public UIPanel mainPanel;
    public UIPanel menuPanel;
    public UIPanel creditsPanel;
    public UIPanel galleryPanel;
    public SimpleUIPanel viewEPPanel;
    public GameObject loading;
    public void BackCreditsPanel() => OnClick(BACK_CREDITS);

    public void GoTutorial()
    {
        PlayGood();
        main_cg.DOFade(0, 0.3f).OnComplete(() =>
        {
            this.gameObject.SetActive(false);
        });
        SceneManager.LoadScene("NASA");
    }

    public void GoGameplay(string id)
    {
        PlayGood();
        main_cg.DOFade(0, 0.3f).OnComplete(() =>
        {
            this.gameObject.SetActive(false);
        });
        SceneManager.LoadScene(id);
    }



    public void OnClick(string id)
    {
        switch (id)
        {
            case FINAL_PLAY:
              //  OnPlay(); 
              //  break;
            case "pegasi":
                viewEPPanel.View("pegasi");
                GoViewPlanet();
                break;
            case "ogle":
                viewEPPanel.View("ogle");
                GoViewPlanet();
                break;
            case MENU:
                OnMenu(); break;
            case QUIT:
                OnQuit();break;
            case CREDITS:
                GoCredits(); break;
            case PLAY:
                GoGallery(); break;
            case BACK:
                GoBack();break;
            case BACK_CREDITS:
                GoBackCredits();break;
            case BACK_GALERY:
                GoBackGallery(); break; 
            case BACK_VIEW:
                GoBackView(); break;
        }
        PlayGood();
    }
    void OnMenu()
    {
        creditsPanel.gameObject.SetActive(false);
        viewEPPanel.gameObject.SetActive(false);
        galleryPanel.gameObject.SetActive(false);
        mainPanel.InitExit();

        menuPanel.gameObject.SetActive(true);
        menuPanel.InitEnter();
        SetActiveTxt(false);
        mainPanel.OnExit(() =>
        {
            mainPanel.gameObject.SetActive(false);
            menuPanel.OnEnter(null);
        });
    }
    void OnQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // Si estamos en una aplicación compilada, la cerramos
        Application.Quit();
#endif
    }


    void GoBackView()
    {
        menuPanel.InitEnter();
        menuPanel.gameObject.SetActive(true);
        menuPanel.OnEnter(null);
        viewEPPanel.OnExit(() =>
        {
            viewEPPanel.gameObject.SetActive(false);
        });
    }

    void GoBackGallery()
    {

        mainPanel.gameObject.SetActive(false);

        menuPanel.InitEnter();
        menuPanel.gameObject.SetActive(true);
        menuPanel.OnEnter(null);
        galleryPanel.OnExit(() =>
        {
            galleryPanel.gameObject.SetActive(false);
        });
    }
    void GoBackCredits()
    {
        //credits to main
        
        menuPanel.InitEnter();
        menuPanel.gameObject.SetActive(true);
        menuPanel.OnEnter(null);
        creditsPanel.OnExit(() =>
        {
            creditsPanel.gameObject.SetActive(false);
        });
    }
    void GoBack()
    {
        //main to menu
        mainPanel.InitEnter();
        menuPanel.InitExit();
        mainPanel.gameObject.SetActive(true);
        menuPanel.OnExit(() =>
        {
            mainPanel.OnEnter(null);
        });
    }


    void GoViewPlanet()
    {
        //view
        creditsPanel.gameObject.SetActive(false);
        galleryPanel.gameObject.SetActive(false);


        viewEPPanel.gameObject.SetActive(true);
        menuPanel.InitExit();
        viewEPPanel.InitEnter();

        SetActiveTxt(false);
        menuPanel.OnExit(() =>
        {
            menuPanel.gameObject.SetActive(false);
            viewEPPanel.OnEnter(null);
        });
    }

    void GoGallery()
    {
        //galery
        creditsPanel.gameObject.SetActive(false);
        viewEPPanel.gameObject.SetActive(false);

        galleryPanel.gameObject.SetActive(true);
        menuPanel.InitExit();
        galleryPanel.InitEnter();
        
        SetActiveTxt(false);
        menuPanel.OnExit(() =>
        {
            menuPanel.gameObject.SetActive(false);
            galleryPanel.OnEnter(null);
        });
    }
    
    void GoCredits()
    {
        galleryPanel.gameObject.SetActive(false);
        viewEPPanel.gameObject.SetActive(false);

        creditsPanel.gameObject.SetActive(true);
        menuPanel.InitExit();
        creditsPanel.InitEnter();

        SetActiveTxt(false);
        menuPanel.OnExit(() =>
        {
            menuPanel.gameObject.SetActive(false);
            creditsPanel.OnEnter(null);
        });

    }
    // animation
    
    public CanvasGroup[] cg_texts;
    CanvasGroup current_cg;
    float time_btns = 0.2f;
    int current_index = -1;
    public void ActivateTxtBtns(int index)
    {
        if(index == current_index)
        {
            return;
        }
        current_index = index;
        if (current_cg != null)
        {
            current_cg.DOFade(0, time_btns).OnComplete(() =>
            {
                SetActiveTxt(false);
                current_cg = cg_texts[index];
                current_cg.gameObject.SetActive(true);
                current_cg.DOFade(1, time_btns);

            });
        }
        else
        {
            SetActiveTxt(false);

            current_cg = cg_texts[index];
            current_cg.gameObject.SetActive(true);
            current_cg.DOFade(1, time_btns);
        }

    }
    void SetActiveTxt(bool active)
    {
        foreach (CanvasGroup cg in cg_texts)
        {
            cg.gameObject.SetActive(active);
        }
    }

    public AudioSource audioSource; // Referencia al componente AudioSource
    public AudioClip god_click; // Referencia al AudioClip del SFX
    public AudioClip bad_click; // Referencia al AudioClip del SFX

    public void PlayGood()
    {
        // Reproducir el AudioClip en una posición específica
        audioSource.PlayOneShot(god_click);
    }
    public void PlayBad()
    {
        // Reproducir el AudioClip en una posición específica
        audioSource.PlayOneShot(bad_click);
    }
}
