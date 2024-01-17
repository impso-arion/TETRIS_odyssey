using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

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

    //�N���V�b�N���b�N�_�E���V�X�e�����̗p����
    //�~�m�����ɗ�����ƁA���b�N�_�E���^�C�}�[�����Z�b�g�����
    //�~�m������������0.5�b�ȏア��ƁA���b�N�_�E�������B
    private float lockDownInterval = 0.5f; // �~�m�����n���Ă���̃^�C�}�[
    private float lockDownTime;



    [SerializeField]
    private GameObject gameOverPanel;

    //�Q�[���I�[�o�[����
    bool gameOver;

    private void Start()
    {
        
        spawner = GameObject.FindObjectOfType<Spawner>();//�X�|�i�[�Ƃ����R���|�[�l���g�����Ă���I�u�W�F�N�g��T��
        board = GameObject.FindObjectOfType<Board>();//�{�[�h��ϐ��Ɋi�[
        spawner.transform.position = Rounding.Round(spawner.transform.position);

        //���b�N�_�E���^�C�}�[�����ݒ�
        lockDownTime = Time.time + lockDownInterval;

        //�p�l����\��
        if (gameOverPanel.activeInHierarchy)
        {
            gameOverPanel.SetActive(false);
        }
    }
    private void Update()
    {
        if (gameOver)
        {
            return;
        }
        //spawner�N���X����u���b�N�����֐����Ă�ŕϐ��Ɋi�[
        if (!activeMino)//��̂Ƃ�
        {
            activeMino = spawner.getNext1Mino();
            activeMino.transform.position = spawner.transform.position;
        }
        //activeMino�̎��Ԃ��`�F�b�N����֐�
        //activeMino��y���`�F�b�N���āA���l��������Ύ��Ԃ����Z�b�g����
        //���Z�b�g���ꂸ0.5�b�������烍�b�N�_�E����



        //PlayerInput();
        if ((Time.time > nextdropTimer))//�{�^���A�łłȂ��A���Ԍo�߂ŗ�����悤�ɂ������B
        {
            activeMino.MoveDown();//���ɓ�����
            nextdropTimer = Time.time + dropInterval;

            //���ɍs����������Œ�
            if (!board.CheckPosition(activeMino))
            {
                //�Œ肳�ꂽ�Ƃ��ɃI�[�o�[���~�b�g�������ǂ����`�F�b�N����
                if (board.OverLimit(activeMino))
                {
                //�Q�[���[�o�[�ɂ���
                    GameOver();
                }
                else
                {
                //���b�N�_�E���^�C�}�[
                //��ɂ����Ƃ��̏���
                BottomBoard();
                }
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
        // direction��0����3�͈̔͂Ń��s�[�g
        activeMino.direction = (activeMino.direction + 1) % 4;
        //��]�Ɏ��s������t��]�����܂��傤
        if (!board.CheckPosition(activeMino))
        {
            activeMino.RotateLeft();
            activeMino.direction -= 1;
        }
    }
    public void RotateLeftActiveMino()
    {
        activeMino.RotateLeft();
        // direction��0����3�͈̔͂Ń��s�[�g
        activeMino.direction = (activeMino.direction - 1) % 4;
        if (activeMino.direction < 0)
        {
            activeMino.direction += 4;
        }
        //��]�Ɏ��s������t��]�����܂��傤
        if (!board.CheckPosition(activeMino))
        {
            activeMino.RotateRight();
            activeMino.direction = (activeMino.direction + 1) % 4;
        }
    }

    public void MoveActiveMinoDown()
    {
        activeMino.MoveDown();//���ɓ�����
        //���ɍs����������Œ�
        if (!board.CheckPosition(activeMino))
        {
            //�Œ肳�ꂽ�Ƃ��ɃI�[�o�[���~�b�g�������ǂ����`�F�b�N����
            if (board.OverLimit(activeMino))
            {
            //�Q�[���[�o�[�ɂ���
                GameOver();
            }
            else
            {
            //��ɂ����Ƃ��̏���
                BottomBoard();
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
        board.ClearAllRows();//���܂��Ă���΍폜����
    }

    //�Q�[���I�[�o�[�ɂȂ�����p�l����\��
    void GameOver()
    {
        activeMino.MoveUp();
        if (!gameOverPanel.activeInHierarchy)
        {
            gameOverPanel.SetActive(true);
        }
        gameOver = true;
    }
    //�V�[���ēǂݍ���
    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

}
