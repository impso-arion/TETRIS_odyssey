using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class KeyInputHandler : MonoBehaviour
{
    private GameManager gameManager; // GameManagerへの参照を保持
    private bool isPaused = false;
    
    //入力受付タイマー(3種類)
    float nextKeyDownTimer, nextKeyLeftRightTimer, nextKeyRotateTimer,nextPauseTimer,hardDropTimer,holdTimer;
    //入力インターバル(3種類)
    [SerializeField]
    public float nextKeyDownInterval, nextKeyLeftRightInterval, nextKeyRotateInterval,pauseInterval,HardDropInterval,HoldInterval;
    //ポーズテキスト
    public TextMeshProUGUI pauseTxt;
    public RawImage KeyInfo;

    void Start()
    {
        //アクションの参照渡しのためにFindを行う
        gameManager = FindObjectOfType<GameManager>();
        //タイマー初期設定
        nextKeyDownTimer = Time.time + nextKeyDownInterval;
        nextKeyLeftRightTimer = Time.time + nextKeyLeftRightInterval;
        nextKeyRotateTimer = Time.time + nextKeyRotateInterval;
        hardDropTimer = Time.time + hardDropTimer;
        holdTimer = Time.time + holdTimer;

        // ゲーム開始時にpauseTxtとKeyInfoを非表示にする
        pauseTxt.gameObject.SetActive(false);
        KeyInfo.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (gameManager.gameOver)
        {
            return;
        }
        //ハードドロップ
        if (Input.GetKey(KeyCode.Space) && (Time.time > hardDropTimer)
            || Input.GetKey(KeyCode.Keypad8) && (Time.time > hardDropTimer))
        {
            hardDropTimer = Time.time + HardDropInterval;
            gameManager.HardDrop();
        }
        //ポーズボタンも連打制御
        if (Input.GetKeyDown(KeyCode.Escape) && (Time.realtimeSinceStartup > nextPauseTimer)
            || Input.GetKeyDown(KeyCode.F1) && (Time.realtimeSinceStartup > nextPauseTimer))
        {
            nextPauseTimer = Time.realtimeSinceStartup + pauseInterval;
            TogglePause();
        }
        if (isPaused)
        {
            return;
        }
        //MoveTetriminoLeft
        if (Input.GetKey(KeyCode.LeftArrow) && (Time.time > nextKeyLeftRightTimer)
            || Input.GetKey(KeyCode.Keypad4) && (Time.time > nextKeyLeftRightTimer))
        {
                nextKeyLeftRightTimer = Time.time + nextKeyLeftRightInterval;
                gameManager.MoveActiveMinoLeft();
                //左移動

        }
        //MoveTetriminoRight
        if (Input.GetKey(KeyCode.RightArrow) && (Time.time > nextKeyLeftRightTimer)
            || Input.GetKey(KeyCode.Keypad6) && (Time.time > nextKeyLeftRightTimer))
        {
                nextKeyLeftRightTimer = Time.time + nextKeyLeftRightInterval;
                gameManager.MoveActiveMinoRight();
                //右移動
        }
        //MoveSoftDropボタンで下にゆく
        if (Input.GetKey(KeyCode.DownArrow) && (Time.time > nextKeyDownTimer)
            || Input.GetKey(KeyCode.Keypad2) && (Time.time > nextKeyDownTimer)
            )
        {
                nextKeyDownTimer = Time.time + nextKeyDownInterval;
                gameManager.MoveActiveMinoDown();
                //ソフトドロップ
        }
        //RotateClockwise
        if (Input.GetKey(KeyCode.UpArrow) && (Time.time > nextKeyRotateTimer)
            || Input.GetKey(KeyCode.X) && (Time.time > nextKeyRotateTimer)
            || Input.GetKey(KeyCode.Keypad1) && (Time.time > nextKeyRotateTimer) 
            || Input.GetKey(KeyCode.Keypad5) && (Time.time > nextKeyRotateTimer) 
            || Input.GetKey(KeyCode.Keypad9) && (Time.time > nextKeyRotateTimer))
        {
                nextKeyRotateTimer = Time.time + nextKeyRotateInterval;
                //右回転
                gameManager.RotateRightActiveMino();
        }
        //RotateCounterclockwise
        if (Input.GetKey(KeyCode.Z) && (Time.time > nextKeyRotateTimer) 
            || Input.GetKey(KeyCode.LeftControl) && (Time.time > nextKeyRotateTimer)
            || Input.GetKey(KeyCode.RightControl) && (Time.time > nextKeyRotateTimer)
            || Input.GetKey(KeyCode.Keypad3) && (Time.time > nextKeyRotateTimer)
            || Input.GetKey(KeyCode.Keypad7) && (Time.time > nextKeyRotateTimer))
        {
                nextKeyRotateTimer = Time.time + nextKeyRotateInterval;
                //左回転
                gameManager.RotateLeftActiveMino();
        }
        //HoldShift, C, Numpad 0
        if (Input.GetKey(KeyCode.C) && (Time.time > holdTimer)
            || Input.GetKey(KeyCode.Keypad0) && (Time.time > holdTimer)
            || Input.GetKey(KeyCode.LeftShift) && (Time.time > holdTimer))
        {
            holdTimer = Time.time + HoldInterval;
            gameManager.Hold();
        }


    }

    void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            // ゲームが一時停止されたときの処理
            Time.timeScale = 0f;
            pauseTxt.gameObject.SetActive(true);
            KeyInfo.gameObject.SetActive(true);
        }
        else
        {
            // ゲームが再開されたときの処理
            Time.timeScale = 1f;
            pauseTxt.gameObject.SetActive(false);
            KeyInfo.gameObject.SetActive(false);
        }
    }


}
