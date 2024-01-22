using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class KeyInputHandler : MonoBehaviour
{
    private GameManager gameManager; // GameManager�ւ̎Q�Ƃ�ێ�
    private bool isPaused = false;
    
    //���͎�t�^�C�}�[(3���)
    float nextKeyDownTimer, nextKeyLeftRightTimer, nextKeyRotateTimer,nextPauseTimer,hardDropTimer,holdTimer;
    //���̓C���^�[�o��(3���)
    [SerializeField]
    public float nextKeyDownInterval, nextKeyLeftRightInterval, nextKeyRotateInterval,pauseInterval,HardDropInterval,HoldInterval;
    //�|�[�Y�e�L�X�g
    public TextMeshProUGUI pauseTxt;
    public RawImage KeyInfo;

    void Start()
    {
        //�A�N�V�����̎Q�Ɠn���̂��߂�Find���s��
        gameManager = FindObjectOfType<GameManager>();
        //�^�C�}�[�����ݒ�
        nextKeyDownTimer = Time.time + nextKeyDownInterval;
        nextKeyLeftRightTimer = Time.time + nextKeyLeftRightInterval;
        nextKeyRotateTimer = Time.time + nextKeyRotateInterval;
        hardDropTimer = Time.time + hardDropTimer;
        holdTimer = Time.time + holdTimer;

        // �Q�[���J�n����pauseTxt��KeyInfo���\���ɂ���
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
        //�n�[�h�h���b�v
        if (Input.GetKey(KeyCode.Space) && (Time.time > hardDropTimer)
            || Input.GetKey(KeyCode.Keypad8) && (Time.time > hardDropTimer))
        {
            hardDropTimer = Time.time + HardDropInterval;
            gameManager.HardDrop();
        }
        //�|�[�Y�{�^�����A�Ő���
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
                //���ړ�

        }
        //MoveTetriminoRight
        if (Input.GetKey(KeyCode.RightArrow) && (Time.time > nextKeyLeftRightTimer)
            || Input.GetKey(KeyCode.Keypad6) && (Time.time > nextKeyLeftRightTimer))
        {
                nextKeyLeftRightTimer = Time.time + nextKeyLeftRightInterval;
                gameManager.MoveActiveMinoRight();
                //�E�ړ�
        }
        //MoveSoftDrop�{�^���ŉ��ɂ䂭
        if (Input.GetKey(KeyCode.DownArrow) && (Time.time > nextKeyDownTimer)
            || Input.GetKey(KeyCode.Keypad2) && (Time.time > nextKeyDownTimer)
            )
        {
                nextKeyDownTimer = Time.time + nextKeyDownInterval;
                gameManager.MoveActiveMinoDown();
                //�\�t�g�h���b�v
        }
        //RotateClockwise
        if (Input.GetKey(KeyCode.UpArrow) && (Time.time > nextKeyRotateTimer)
            || Input.GetKey(KeyCode.X) && (Time.time > nextKeyRotateTimer)
            || Input.GetKey(KeyCode.Keypad1) && (Time.time > nextKeyRotateTimer) 
            || Input.GetKey(KeyCode.Keypad5) && (Time.time > nextKeyRotateTimer) 
            || Input.GetKey(KeyCode.Keypad9) && (Time.time > nextKeyRotateTimer))
        {
                nextKeyRotateTimer = Time.time + nextKeyRotateInterval;
                //�E��]
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
                //����]
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
            // �Q�[�����ꎞ��~���ꂽ�Ƃ��̏���
            Time.timeScale = 0f;
            pauseTxt.gameObject.SetActive(true);
            KeyInfo.gameObject.SetActive(true);
        }
        else
        {
            // �Q�[�����ĊJ���ꂽ�Ƃ��̏���
            Time.timeScale = 1f;
            pauseTxt.gameObject.SetActive(false);
            KeyInfo.gameObject.SetActive(false);
        }
    }


}
