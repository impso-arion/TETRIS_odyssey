using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyInputHandler : MonoBehaviour
{
    private GameManager gameManager; // GameManagerへの参照を保持
    private bool isPaused = false;
    
    //入力受付タイマー(3種類)
    float nextKeyDownTimer, nextKeyLeftRightTimer, nextKeyRotateTimer;
    //入力インターバル(3種類)
    [SerializeField]
    public float nextKeyDownInterval, nextKeyLeftRightInterval, nextKeyRotateInterval;



    void Start()
    {
        //アクションの参照渡しのためにFindを行う
        gameManager = FindObjectOfType<GameManager>();
        //タイマー初期設定
        nextKeyDownTimer = Time.time + nextKeyDownInterval;
        nextKeyLeftRightTimer = Time.time + nextKeyLeftRightInterval;
        nextKeyRotateTimer = Time.time + nextKeyRotateInterval;
    }
    // Update is called once per frame
    void Update()
    {
        //ポーズボタン
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.F1))
        {
            TogglePause();
        }

        //MoveTetriminoLeft
        if(Input.GetKeyDown(KeyCode.LeftArrow) && (Time.time > nextKeyLeftRightTimer)
            || Input.GetKeyDown(KeyCode.Keypad4) && (Time.time > nextKeyLeftRightTimer))
        {

            if(!isPaused)
            {
                nextKeyLeftRightTimer = Time.time + nextKeyLeftRightInterval;
                gameManager.MoveActiveMinoLeft();
                //左移動
            }

        }
        //MoveTetriminoRight
        if (Input.GetKeyDown(KeyCode.RightArrow) && (Time.time > nextKeyLeftRightTimer)
            || Input.GetKeyDown(KeyCode.Keypad6) && (Time.time > nextKeyLeftRightTimer))
        {
            if (!isPaused)
            {
                nextKeyLeftRightTimer = Time.time + nextKeyLeftRightInterval;
                gameManager.MoveActiveMinoRight();
                //右移動
            }
        }
        //MoveSoftDropボタンで下にゆく
        if (Input.GetKeyDown(KeyCode.DownArrow) && (Time.time > nextKeyDownTimer)
            || Input.GetKeyDown(KeyCode.Keypad2) && (Time.time > nextKeyDownTimer)
            )
        {
            if (!isPaused)
            {
                //Debug.Log("下押した");
                nextKeyDownTimer = Time.time + nextKeyDownInterval;
                gameManager.MoveActiveMinoDown();
                //ソフトドロップ
            }
        }
        //RotateClockwise
        if (Input.GetKeyDown(KeyCode.UpArrow) && (Time.time > nextKeyRotateTimer)
            || Input.GetKeyDown(KeyCode.X) && (Time.time > nextKeyRotateTimer)
            || Input.GetKeyDown(KeyCode.Keypad1) && (Time.time > nextKeyRotateTimer) 
            || Input.GetKeyDown(KeyCode.Keypad5) && (Time.time > nextKeyRotateTimer) 
            || Input.GetKeyDown(KeyCode.Keypad9) && (Time.time > nextKeyRotateTimer))
        {
            if (!isPaused)
            {
                nextKeyRotateTimer = Time.time + nextKeyRotateInterval;
                //右回転
                gameManager.RotateRightActiveMino();
            }
        }
        //RotateCounterclockwise
        if (Input.GetKeyDown(KeyCode.Z) && (Time.time > nextKeyRotateTimer) 
            || Input.GetKeyDown(KeyCode.Keypad3) && (Time.time > nextKeyRotateTimer)
            || Input.GetKeyDown(KeyCode.Keypad7) && (Time.time > nextKeyRotateTimer))
        {
            if (!isPaused)
            {
                nextKeyRotateTimer = Time.time + nextKeyRotateInterval;
                //左回転
                gameManager.RotateLeftActiveMino();
            }
        }

    }

    void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            // ゲームが一時停止されたときの処理
            Time.timeScale = 0f;
        }
        else
        {
            // ゲームが再開されたときの処理
            Time.timeScale = 1f;
        }
    }



}
