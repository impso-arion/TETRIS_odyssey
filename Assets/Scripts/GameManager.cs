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
    [SerializeField] float nextdropTimer;

    //spawner�I�u�W�F�N�g��spawner�ϐ��Ɋi�[����R�[�h
    private void Start()
    {
        
        spawner = GameObject.FindObjectOfType<Spawner>();//�X�|�i�[�Ƃ����R���|�[�l���g�����Ă���I�u�W�F�N�g��T��
        board = GameObject.FindObjectOfType<Board>();//�{�[�h��ϐ��Ɋi�[
    }
    private void Update()
    {
        //spawner�N���X����u���b�N�����֐����Ă�ŕϐ��Ɋi�[

        if (!activeMino)//��̂Ƃ�
        {

            activeMino = spawner.getSpawnMino();


        }
        //Update�Ŏ��Ԃ̔�������āA���肵�����ŗ����֐����Ă�
        if (Time.time > nextdropTimer)
        {
            nextdropTimer = Time.time + dropInterval;//���݂̃Q�[�����Ԃƃh���b�v�C���^�[�o��
            if (activeMino)//���g������Ƃ�
            {

                activeMino.MoveDown();

                //Board�N���X�ŊO�ɏo�����`�F�b�N
                if (!board.CheckPosition(activeMino))
                {
                    //�͂ݏo�����ɓ���
                    activeMino.MoveUp();
                    //�����҂�
                    //�V�������̂��o�Ă���
                    activeMino = spawner.getSpawnMino() ;
                }

            }
        }



        
        
    }




}
