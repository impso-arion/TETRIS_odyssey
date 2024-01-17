using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyInputHandler : MonoBehaviour
{
    private GameManager gameManager; // GameManager�ւ̎Q�Ƃ�ێ�
    private bool isPaused = false;
    
    //���͎�t�^�C�}�[(3���)
    float nextKeyDownTimer, nextKeyLeftRightTimer, nextKeyRotateTimer;
    //���̓C���^�[�o��(3���)
    [SerializeField]
    public float nextKeyDownInterval, nextKeyLeftRightInterval, nextKeyRotateInterval;



    void Start()
    {
        //�A�N�V�����̎Q�Ɠn���̂��߂�Find���s��
        gameManager = FindObjectOfType<GameManager>();
        //�^�C�}�[�����ݒ�
        nextKeyDownTimer = Time.time + nextKeyDownInterval;
        nextKeyLeftRightTimer = Time.time + nextKeyLeftRightInterval;
        nextKeyRotateTimer = Time.time + nextKeyRotateInterval;
    }
    // Update is called once per frame
    void Update()
    {
        //�|�[�Y�{�^��
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
                //���ړ�
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
                //�E�ړ�
            }
        }
        //MoveSoftDrop�{�^���ŉ��ɂ䂭
        if (Input.GetKeyDown(KeyCode.DownArrow) && (Time.time > nextKeyDownTimer)
            || Input.GetKeyDown(KeyCode.Keypad2) && (Time.time > nextKeyDownTimer)
            )
        {
            if (!isPaused)
            {
                //Debug.Log("��������");
                nextKeyDownTimer = Time.time + nextKeyDownInterval;
                gameManager.MoveActiveMinoDown();
                //�\�t�g�h���b�v
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
                //�E��]
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
                //����]
                gameManager.RotateLeftActiveMino();
            }
        }

    }

    void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            // �Q�[�����ꎞ��~���ꂽ�Ƃ��̏���
            Time.timeScale = 0f;
        }
        else
        {
            // �Q�[�����ĊJ���ꂽ�Ƃ��̏���
            Time.timeScale = 1f;
        }
    }



}
