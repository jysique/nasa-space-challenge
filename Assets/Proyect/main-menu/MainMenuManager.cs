using DG.Tweening;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    //FIRST PANEL
    public const string MENU = "menu";
    public const string QUIT = "quit";

    //SECOND PANEL
    public const string CREDTIS = "credits";
    public const string PLAY = "play";
    public const string BACK_MENU = "back";

    public static MainMenuManager instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            main_cg = GetComponent<CanvasGroup>();
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    CanvasGroup main_cg;
    public UIPanel mainPanel;
    public UIPanel menuPanel;

    
    public void OnClick(string id)
    {
        switch (id)
        {

            case MENU:
                OnMenu(); break;
            case QUIT:
                OnQuit();break;
            case CREDTIS:
                OnCredits(); break;
            case PLAY:
                OnPlay();break;
            case BACK_MENU:
                OnBack();break;
        }
    }
    void OnMenu()
    {
        mainPanel.InitExit();
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
    void OnPlay()
    {
        main_cg.DOFade(0, 0.3f).OnComplete(() =>
        {
            this.gameObject.SetActive(false);
        });
        
    }
    void OnBack()
    {
        mainPanel.InitEnter();
        menuPanel.InitExit();
        mainPanel.gameObject.SetActive(true);
        menuPanel.OnExit(() =>
        {
            mainPanel.OnEnter(null);
        });
    }
    void OnCredits()
    {
        Debug.Log(" on credtis");
    }
    // animation
    
    public CanvasGroup[] cg_texts;
    CanvasGroup current_cg;
    float time_btns = 0.2f;
    public void ActivateTxtBtns(int index)
    {
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

}
