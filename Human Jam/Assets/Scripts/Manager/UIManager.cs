using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public enum ScreenTransitionType
{
    CENTER_TO_RIGHT,
    RIGHT_TO_CENTER,
    TOTAL
}

public class UIManager : Singleton<UIManager>
{
    [Header("<-- Main Menu UI -->")]
    public GameObject GO_Menu_ScreenTransition;
    public Button Btn_Menu_Play;
    public Button Btn_Menu_Setting;
    public Button Btn_Menu_Credit;

    [Header("<-- Gameplay UI -->")]
    public GameObject GO_Gameplay_ScreenTransition;
    public GameObject GO_Gameplay_TouchScreenController;
    public GameObject GO_Gameplay_PauseMenu;
    public GameObject GO_Gameplay_ScreenFader;
    public GameObject GO_Gameplay_GameOver;
    public Button Btn_Gameplay_Pause;
    public Button Btn_Gameplay_Resume;
    public Button Btn_Gameplay_Restart;
    public Button Btn_Gameplay_Home;
    public Button Btn_Gameplay_ClickToStart;
    public Button Btn_GameOver_Restart;
    public Button Btn_GameOver_Home;
    public TextMeshProUGUI Txt_CurrentScore;
    public TextMeshProUGUI Txt_GameOver_Score;
    public TextMeshProUGUI Txt_Teaser_HighScore;

    // Start is called before the first frame update
    void Start()
    {
        SetupButtonClick();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetupButtonClick()
    {
        if(SceneManager.GetActiveScene().name == "MainMenu")
        {
            Btn_Menu_Play.onClick.AddListener(Click_Menu_Play);
            Btn_Menu_Setting.onClick.AddListener(Click_Menu_Setting);
            Btn_Menu_Credit.onClick.AddListener(Click_Menu_Credit);
        }

        else if (SceneManager.GetActiveScene().name == "Gameplay")
        {
            Btn_Gameplay_Pause.onClick.AddListener(Click_Gameplay_Pause);
            Btn_Gameplay_Resume.onClick.AddListener(Click_Gameplay_Resume);
            Btn_Gameplay_Restart.onClick.AddListener(Click_Gameplay_Restart);
            Btn_Gameplay_Home.onClick.AddListener(Click_Gameplay_Home);
            Btn_Gameplay_ClickToStart.onClick.AddListener(Click_Gameplay_ClickToStart);

            Btn_GameOver_Restart.onClick.AddListener(Click_Gameplay_Restart);
            Btn_GameOver_Home.onClick.AddListener(Click_Gameplay_Home);
        }
    }

    //================================================//
    //
    // Menu Buttons: Event(s)
    //
    //================================================//
    void Click_Menu_Play()
    {
        MyGameManager.Instance.ChangeScene(GameState.MAIN_MENU, GameState.GAMEPLAY);
    }
    void Click_Menu_Setting()
    {
    }
    void Click_Menu_Credit()
    {
    }

    public void Start_Menu_Transition(ScreenTransitionType type)
    {

        GO_Menu_ScreenTransition.SetActive(true);

        Animator anim = GO_Menu_ScreenTransition.GetComponent<Animator>();
        if (anim)
        {
            if (type == ScreenTransitionType.CENTER_TO_RIGHT)
                anim.SetTrigger("Trigger_CenterToRight");
            else if (type == ScreenTransitionType.RIGHT_TO_CENTER)
                anim.SetTrigger("Trigger_RightToCenter");
        }
    }

    //================================================//
    //
    // Gameplay Buttons: Event(s)
    //
    //================================================//
    void Click_Gameplay_Pause()
    {
        GO_Gameplay_PauseMenu.SetActive(true);
        GO_Gameplay_ScreenFader.SetActive(true);
        Time.timeScale = 0.0f;
    }
    void Click_Gameplay_Resume()
    {
        GO_Gameplay_PauseMenu.SetActive(false);
        GO_Gameplay_ScreenFader.SetActive(false);
        Time.timeScale = 1.0f;
    }
    void Click_Gameplay_Restart()
    {
        MyGameManager.Instance.ChangeScene(GameState.GAMEPLAY, GameState.RESTART);
    }
    void Click_Gameplay_Home()
    {
        MyGameManager.Instance.ChangeScene(GameState.GAMEPLAY, GameState.MAIN_MENU);
    }

    void Click_Gameplay_ClickToStart()
    {
        Btn_Gameplay_ClickToStart.gameObject.SetActive(false);
        FindObjectOfType<PlaceHolder_Player>().StartMove();
    }

    public void Start_Gameplay_Transition(ScreenTransitionType type)
    {
        GO_Gameplay_ScreenTransition.SetActive(true);

        Animator anim = GO_Gameplay_ScreenTransition.GetComponent<Animator>();
        if(anim)
        {
            if (type == ScreenTransitionType.CENTER_TO_RIGHT)
                anim.SetTrigger("Trigger_CenterToRight");
            else if (type == ScreenTransitionType.RIGHT_TO_CENTER)
                anim.SetTrigger("Trigger_RightToCenter");
        }
    }

    public void Show_GameOver_Screen()
    {
        GO_Gameplay_GameOver.SetActive(true);
        GO_Gameplay_ScreenFader.SetActive(true);
    }

    public void UpdateCurrentScoreText(int score)
    {
        Txt_CurrentScore.text = score.ToString() + "m";
    }

    public void Update_GameOver_Score_Text(int score)
    {
        Txt_GameOver_Score.text = score.ToString() + "m";
    }
    
    public void Update_Gameplay_Teaser_HighScore_Text(int score)
    {
        Txt_Teaser_HighScore.text = score.ToString() + "m";
    }
}
