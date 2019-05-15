using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    RESTART,
    MAIN_MENU,
    GAMEPLAY,
    TOTAL
}

public class MyGameManager : MonoBehaviour
{
    public static MyGameManager Instance;

    bool doScreenTransition = false;

    int highscore = 99;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else if (Instance != this)
            Destroy(gameObject);
    }

    void OnEnable()
    {
        //Tell our 'OnLevelFinishedLoading' function to start listening for a scene change as soon as this script is enabled.
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnDisable()
    {
        //Tell our 'OnLevelFinishedLoading' function to stop listening for a scene change as soon as this script is disabled. Remember to always have an unsubscription for every delegate you subscribe to!
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        if(doScreenTransition)
        {
            if (scene.name == "MainMenu")
            {
                StartCoroutine(ScreenTransition_LoadIntoNewLevel(GameState.MAIN_MENU, ScreenTransitionType.CENTER_TO_RIGHT));
            }
            else if (scene.name == "Gameplay")
            {
                StartCoroutine(ScreenTransition_LoadIntoNewLevel(GameState.GAMEPLAY, ScreenTransitionType.CENTER_TO_RIGHT));
                UIManager.Instance.Update_Gameplay_Teaser_HighScore_Text(highscore);
            }
        }

        // Only do this once, because just start game dont do screen transition
        if (!doScreenTransition)
            doScreenTransition = true;
    }


    public void ChangeScene(GameState currentGS, GameState nextGS)
    {
        StartCoroutine(ScreenTransition_ToNextLevel(currentGS, nextGS, ScreenTransitionType.RIGHT_TO_CENTER));
    }

    IEnumerator ScreenTransition_ToNextLevel(GameState currentGS, GameState nextGS, ScreenTransitionType type)
    {
        Time.timeScale = 0.0f;
        // Start Animate the Screen Transition
        if (currentGS == GameState.MAIN_MENU)
        {
            UIManager.Instance.Start_Menu_Transition(type);
        }
        else if (currentGS == GameState.GAMEPLAY)
        {
            UIManager.Instance.Start_Gameplay_Transition(type);
        }

        // Delay for 1.5seconds
        yield return new WaitForSecondsRealtime(1.5f);

        Time.timeScale = 1.0f;
        // Go to next State after 1.5seconds
        if (nextGS == GameState.MAIN_MENU)
            SceneManager.LoadScene("MainMenu");
        else if (nextGS == GameState.GAMEPLAY || nextGS == GameState.RESTART)
        {
            SceneManager.LoadScene("Gameplay");
        }
    }

    IEnumerator ScreenTransition_LoadIntoNewLevel(GameState currentGS, ScreenTransitionType type)
    {
        Time.timeScale = 0.0f;
        // Start Animate the Screen Transition
        if (currentGS == GameState.MAIN_MENU)
        {
            UIManager.Instance.Start_Menu_Transition(type);
        }
        else if (currentGS == GameState.GAMEPLAY)
        {
            UIManager.Instance.Start_Gameplay_Transition(type);
        }

        // Delay for 1.5seconds
        yield return new WaitForSecondsRealtime(1.5f);

        Time.timeScale = 1.0f;
    }

    public void EndGame(int score)
    {
        // Record new highscore
        if(score > highscore)
            highscore = score;

        UIManager.Instance.Update_GameOver_Score_Text(score);
        UIManager.Instance.Show_GameOver_Screen();
    }
}
