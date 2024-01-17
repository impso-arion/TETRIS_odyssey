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

    //���n����̐������Ԃ��l����Ȃ�΂���B
    //public float landingTime = 1.5f; // �~�m�����n���Ă���̐�������
    //private bool landed = false;
    //private float landingTimer = 0f;


    private void Start()
    {
        
        spawner = GameObject.FindObjectOfType<Spawner>();//�X�|�i�[�Ƃ����R���|�[�l���g�����Ă���I�u�W�F�N�g��T��
        board = GameObject.FindObjectOfType<Board>();//�{�[�h��ϐ��Ɋi�[
        spawner.transform.position = Rounding.Round(spawner.transform.position);

    }
    private void Update()
    {
        //spawner�N���X����u���b�N�����֐����Ă�ŕϐ��Ɋi�[
        if (!activeMino)//��̂Ƃ�
        {
            activeMino = spawner.getNext1Mino();
            activeMino.transform.position = spawner.transform.position;
        }
        //PlayerInput();
        if ((Time.time > nextdropTimer))//�{�^���A�łłȂ��A���Ԍo�߂ŗ�����悤�ɂ������B
        {
            activeMino.MoveDown();//���ɓ�����
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

    
    public void MoveActiveMinoLeft()
    {
        // activeMino�����Ɉړ����鏈��
        activeMino.MoveLeft();//���ɓ�����
        //���ɍs����������E�ɖ߂�
        if (!board.CheckPosition(activeMino))
        {
            activeMino.MoveRight();
        }
    }
    public void MoveActiveMinoRight()
    {
        // activeMino�����Ɉړ����鏈��
        activeMino.MoveRight();//���ɓ�����
        //���ɍs����������E�ɖ߂�
        if (!board.CheckPosition(activeMino))
        {
            activeMino.MoveLeft();
        }
    }
    public void RotateRightActiveMino()
    {
        activeMino.RotateRight();
        //��]�Ɏ��s������t��]�����܂��傤
        if (!board.CheckPosition(activeMino))
        {
            activeMino.RotateLeft();
        }
    }
    public void RotateLeftActiveMino()
    {
        activeMino.RotateLeft();
        //��]�Ɏ��s������t��]�����܂��傤
        if (!board.CheckPosition(activeMino))
        {
            activeMino.RotateRight();
        }
    }

    public void MoveActiveMinoDown()
    {
        activeMino.MoveDown();//���ɓ�����
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




    //���ɒ��n�����Ƃ��̏���
    //�{�[�h�̂����ɒ��������Ɏ��̃u���b�N�𐶐�����֐�
    void BottomBoard()
    {
        activeMino.MoveUp();//��ɂ͂߂���
        board.SaveBlockInGrid(activeMino);//���̃u���b�N�̈ʒu��o�^���Ă�����

        activeMino = spawner.getNext1Mino();//���̃u���b�N����
        activeMino.transform.position = spawner.transform.position;//���̃u���b�N�̈ʒu�ύX
        board.ClearAllRows();//���܂��Ă���΍폜����
    }




}
