using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlaceHolder_Player : MonoBehaviour
{
    // Twerkable Variable
    public float speed = 10.0f;
    public float strafeSpeed = 5.0f;

    // Private Variable
    Vector3 inputDir;
    bool canMove = false;
    bool IsMobile;
    int score;

    // Every 3s increment score by 1m
    const int SCORE_INCREMENT_AMOUNT = 1;
    const float SCORE_COOLDOWN_RATE = 1.0f;
    float nextScoreIncrementTime;


    void Start()
    {
        IsMobile = Application.isMobilePlatform;
        SetupForCrossPlatform();
    }

    void Update()
    {
        // Wait for the Cue to Start Game
        if (!canMove)
            return;

        // Reset to zero
        inputDir = Vector3.zero;

        // Read Input 
        if (!IsMobile)
            ReadInput_PC();

        // Update Score
        UpdateScore();

        // Apply Movement
        UpdateMovement();
    }
    void UpdateMovement()
    {
        transform.position += new Vector3(inputDir.x * strafeSpeed * Time.deltaTime,
                                          0,
                                          speed * Time.deltaTime);
    }


    //==================================================================//
    //
    // PC Platform: Function
    //
    //==================================================================//
    void ReadInput_PC()
    {
        //Read input
        inputDir = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
    }


    //==================================================================//
    //
    // Mobile Platform: Function
    //
    //==================================================================//

    public void MoveLeft()
    {
        inputDir.x = -1;
    }

    public void MoveRight()
    {
        inputDir.x = 1;
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.tag == "END_CHECK_POINT")
        {
            // Spawn a new Pattern
            EndlessManager.Instance.SpawnNextPattern();

            // Turn off the current pattern after a few second
            EndlessEntity entity = col.GetComponentInParent<EndlessEntity>();
            if(entity)
            {
                entity.Kill_Pattern_After_Delay(4);
            }
        }

        if(col.tag == "OBSTACLE")
        {
            this.enabled = false;
            MyGameManager.Instance.EndGame(score);
        }

        if (col.tag == "ENEMY")
        {
            this.enabled = false;
            MyGameManager.Instance.EndGame(score);
        }
    }

    public void StartMove()
    {
        canMove = true;
    }

    void SetupForCrossPlatform()
    {
        if(IsMobile)
        {
            // Turn On Touch Screen Controller
            UIManager.Instance.GO_Gameplay_TouchScreenController.SetActive(true);

            // Assign UIControl player instance
            UIControl[] uiArray = FindObjectsOfType<UIControl>();
            foreach (UIControl u in uiArray)
                u.player_cs = this;
        }
        else
        {
            // Turn Off Touch Screen Controller
            UIManager.Instance.GO_Gameplay_TouchScreenController.SetActive(false);
        }

    }

    void UpdateScore()
    {
        // Every 1 second increase score by 1m
        if(Time.time > nextScoreIncrementTime )
        {
            // Increase score
            IncreaseScore(SCORE_INCREMENT_AMOUNT);

            // Set next timing to increase score again
            nextScoreIncrementTime = Time.time + SCORE_COOLDOWN_RATE;
        }
    }

    void IncreaseScore(int incrementAmount)
    {
        score += incrementAmount;
        UIManager.Instance.UpdateCurrentScoreText(score);
    }
}
