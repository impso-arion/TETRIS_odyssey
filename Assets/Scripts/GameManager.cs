using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //�ϐ��̍쐬
    //Spawner
    Spawner spawner;

    //�������ꂽ�~�m���i�[
    Tetrimino activeMino;
    //�{�[�h�̃X�N���v�g���i�[
    Board board;

    //���Ƀu���b�N��������܂ł̃C���^�[�o������
    //���̃u���b�N��������܂ł̎���
    [SerializeField] private float dropInterval = 0.25f;
    float nextdropTimer;

    //�ϐ��̍쐬
    //���͎�t�^�C�}�[(3���)
    float nextKeyDownTimer, nextKeyLeftRightTimer, nextKeyRotateTimer;
    //���̓C���^�[�o��(3���)
    [SerializeField]
    public float nextKeyDownInterval, nextKeyLeftRightInterval, nextKeyRotateInterval;
    //[SerializeField]
    //public float bottomTouchInterval = 0.5f;

    //���n����̐������Ԃ��l����Ȃ�΂���B
    //public float landingTime = 1.5f; // �~�m�����n���Ă���̐�������
    //private bool landed = false;
    //private float landingTimer = 0f;


    private void Start()
    {
        
        spawner = GameObject.FindObjectOfType<Spawner>();//�X�|�i�[�Ƃ����R���|�[�l���g�����Ă���I�u�W�F�N�g��T��
        board = GameObject.FindObjectOfType<Board>();//�{�[�h��ϐ��Ɋi�[
        spawner.transform.position = Rounding.Round(spawner.transform.position);

        //�^�C�}�[�����ݒ�
        nextKeyDownTimer = Time.time + nextKeyDownInterval;
        nextKeyLeftRightTimer = Time.time + nextKeyLeftRightInterval;
        nextKeyRotateTimer = Time.time + nextKeyRotateInterval;

        //bottomTouchTimer = Time.time + bottomTouchInterval;

        if (!activeMino)//��̂Ƃ�
        {
            activeMino = spawner.getNext1Mino();
            //activeMino.transform.position = new Vector3(4.5f, 25f, -1f);
        }
    }
    private void Update()
    {
        //spawner�N���X����u���b�N�����֐����Ă�ŕϐ��Ɋi�[
        if (!activeMino)//��̂Ƃ�
        {
            activeMino = spawner.getNext1Mino();
            activeMino.transform.position = spawner.transform.position;
        }
        PlayerInput();
        //Update�Ŏ��Ԃ̔�������āA���肵�����ŗ����֐����Ă�
        //if (Time.time > nextdropTimer)
        //{
        //nextdropTimer = Time.time + dropInterval;//���݂̃Q�[�����Ԃƃh���b�v�C���^�[�o��
        //if (activeMino)//���g������Ƃ�
        //{
        //
        //activeMino.MoveDown();

        //Board�N���X�ŊO�ɏo�����`�F�b�N
        //if (!board.CheckPosition(activeMino))
        //{
        //�͂ݏo�����ɓ���
        //activeMino.MoveUp();

        //board�ɕۑ�����
        //board.SaveBlockInGrid(activeMino);

        //�V�������̂��o�Ă���
        //activeMino = spawner.getNext1Mino() ;
        //activeMino.transform.position = spawner.transform.position;
        //}
        //
        //}
        //}
    }
    //�֐��쐬
    //�L�[�̓��͂����m���ău���b�N�𓮂����֐�
    void PlayerInput()
    {
        //�{�^�������A�{�^���������ςȂ��𐧌�A�{�^���A�ł�����
        if (Input.GetKey(KeyCode.D) && (Time.time > nextKeyLeftRightTimer)
            || Input.GetKeyDown(KeyCode.D))
        {
            activeMino.MoveRight();//�E�ɓ�����

            nextKeyLeftRightTimer = Time.time + nextKeyLeftRightInterval;

            //�E�ɍs���������獶�ɖ߂�
            if (!board.CheckPosition(activeMino))
            {
                activeMino.MoveLeft();
            }
        }
        else if (Input.GetKey(KeyCode.A) && (Time.time > nextKeyLeftRightTimer)
            || Input.GetKeyDown(KeyCode.A))
        {
            activeMino.MoveLeft();//���ɓ�����

            nextKeyLeftRightTimer = Time.time + nextKeyLeftRightInterval;

            //���ɍs����������E�ɖ߂�
            if (!board.CheckPosition(activeMino))
            {
                activeMino.MoveRight();
            }
        }
        else if (Input.GetKey(KeyCode.E) && (Time.time > nextKeyRotateTimer)//��]
            || Input.GetKeyDown(KeyCode.E))
        {
            activeMino.RotateRight();
            nextKeyRotateTimer = Time.time + nextKeyRotateInterval;
            //��]�Ɏ��s������t��]�����܂��傤
            if (!board.CheckPosition(activeMino))
            {
                activeMino.RotateLeft();
            }
        }
        else if (Input.GetKey(KeyCode.S) && (Time.time > nextKeyDownTimer)//����
            || (Time.time > nextdropTimer))//�{�^���A�łłȂ��A���Ԍo�߂ŗ�����悤�ɂ������B
        {
            activeMino.MoveDown();//���ɓ�����

            nextKeyDownTimer = Time.time + nextKeyDownInterval;
            nextdropTimer = Time.time + dropInterval;

            //���ɍs����������Œ�
            if (!board.CheckPosition(activeMino))
            {
                //�Œ肳�ꂽ�Ƃ��ɃI�[�o�[���~�b�g�������ǂ����`�F�b�N����
                //if (board.OverLimit(activeMino))
                //{
                    //�Q�[���[�o�[�ɂ���
                //    GameOver();
                //}
                //else
                //{
                    //��ɂ����Ƃ��̏���
                    BottomBoard();
                //}

            }
        }
    }

    //���ɒ��n�����Ƃ��̏���
    //�{�[�h�̂����ɒ��������Ɏ��̃u���b�N�𐶐�����֐�
    void BottomBoard()
    {
        activeMino.MoveUp();//��ɂ͂߂���
        board.SaveBlockInGrid(activeMino);//���̃u���b�N�̈ʒu��o�^���Ă�����

        activeMino = spawner.getNext1Mino();//���̃u���b�N����
        activeMino.transform.position = spawner.transform.position;//���̃u���b�N�̈ʒu�ύX

        nextKeyDownTimer = Time.time;
        nextKeyLeftRightTimer = Time.time;
        nextKeyRotateTimer = Time.time;


        board.ClearAllRows();//���܂��Ă���΍폜����
    }




}
