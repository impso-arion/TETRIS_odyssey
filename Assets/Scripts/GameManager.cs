using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    //Spawner
    Spawner spawner;

    //�������ꂽ�~�m���i�[
    Tetrimino activeMino;
    //�������ꂽ�S�[�X�g���i�[
    Ghost activeGhost;
    private List<Ghost> ghosts;
    //�{�[�h�̃X�N���v�g���i�[
    Board board;

    //���Ƀu���b�N��������܂ł̃C���^�[�o������
    //���̃u���b�N��������܂ł̎���
    [SerializeField] private float dropInterval = 0.25f;
    float nextdropTimer;

    //�N���V�b�N���b�N�_�E���V�X�e�����̗p����
    //�~�m�����ɗ�����ƁA���b�N�_�E���^�C�}�[�����Z�b�g�����
    //�~�m������������0.5�b�ȏア��ƁA���b�N�_�E�������B
    private float lockDownInterval = 0.5f; // �~�m�����n���Ă���̃C���^�[�o��
    private float lockDownTimer;//���b�N�_�E���^�C�}�[
    [SerializeField] private float lockDownTimeStart;//���b�N�_�E���^�C�}�[�X�^�[�g����
    [SerializeField] private int lastMinoPosY = 21;//�����ʒu��21
    [SerializeField]private bool isLockDown = false;


    [SerializeField]
    private GameObject gameOverPanel;

    //�Q�[���I�[�o�[����
    public bool gameOver;

    private void Start()
    {
        
        spawner = GameObject.FindObjectOfType<Spawner>();//�X�|�i�[�Ƃ����R���|�[�l���g�����Ă���I�u�W�F�N�g��T��
        board = GameObject.FindObjectOfType<Board>();//�{�[�h��ϐ��Ɋi�[
        spawner.transform.position = Rounding.Round(spawner.transform.position);

        //Ghost���X�g�̎擾
        ghosts = spawner.GhostList;

        //���b�N�_�E���^�C�}�[�����ݒ�
        lockDownTimer = 0;

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
        if (!activeMino)//��̂Ƃ��B����͏��񂾂��Ă΂�邾�낤
        {
            //spawner�N���X����u���b�N�����֐����Ă�ŕϐ��Ɋi�[
            activeMino = spawner.getNext1Mino();
            activeMino.transform.position = spawner.transform.position;
            //ghosts�̒�����ЂƂI������activeGhost�ɂ���B
            GhostChange(activeMino);
            //activeGhost�̈ʒu��activeMino�ɒǏ]����
        }
        //�S�[�X�g�̈ʒu�����߂�
        GhostPosition(activeMino,activeGhost);

        if ((Time.time > nextdropTimer))//�{�^���A�łłȂ��A���Ԍo�߂ɂ�闎���B
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
                    activeMino.MoveUp();
                    LockDownTimeCheck(activeMino);
                }
            }
        }
        //��ɂ����Ƃ��̏���
        if (isLockDown)//���b�N�_�E���Ȃ烍�b�N�_�E������
        {
            BottomBoard();
        }
    }

    void GhostPosition(Tetrimino activeMino,Ghost activeGhost)
    {
        //�S�[�X�g�̈ʒu�����߂�
        //���p�����Ƃɉ�]
        int direct = activeMino.direction;
        activeGhost.Rotate(direct);
        //pos.x��activeMino�ɒǏ]
        float posX = activeMino.transform.position.x;
        activeGhost.transform.position = new Vector3(posX,20,0);//�Ƃ肠�����ŏ㕔
        //pos.y�͍ŏ㕔���珇�Ƀ`�F�b�N
        for (int i = 20; i >= 0; i--)
        {
            activeGhost.transform.position = new Vector3(posX, i, 0);
            if (!board.ghostCheckPosition(activeGhost))//
            {
                //�|�W�V�����`�F�b�N�B��x�������OK
                activeGhost.transform.position = new Vector3(posX, i+1, 0);
                break;
            }
        }


    }



    //
    /// <summary>
    /// �S�[�X�g�Ăяo���Ɩ߂�
    /// </summary>
    /// <param name="activeMino"></param>
    /// �ҋ@�S�[�X�g�̈ʒu��Vector3(-100,0,0)��\���ɂ͂��Ȃ�
    void GhostChange(Tetrimino activeMino)
    {
        Vector3 prPos = new Vector3(-100, 0, 0);
        //activeMino����A�Ƃ肾���S�[�X�g���Q�b�g�B
        //����ȊO�̃S�[�X�g�����͉�ʊO�ɔ�΂��B
        string tagname = activeMino.tag;
        //Debug.Log(tagname);
        // Ghost���X�g����łȂ����Ƃ��m�F���Ă��痘�p
        if (ghosts.Count > 0)
        {
            //���ׂĂ�Ghost���ɂ��܂�
            for(int i = 0; i < ghosts.Count; i++)
            {
                ghosts[i].transform.position = prPos;
            }
            switch (tagname)
            {
                case "Imino":
                    activeGhost = ghosts[0];
                    break;
                case "Jmino":
                    activeGhost = ghosts[1];
                    break;
                case "Lmino":
                    activeGhost = ghosts[2];
                    break;
                case "Omino":
                    activeGhost = ghosts[3];
                    break;
                case "Smino":
                    activeGhost = ghosts[4];
                    break;
                case "Tmino":
                    activeGhost = ghosts[5];
                    break;
                case "Zmino":
                    activeGhost = ghosts[6];
                    break;
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
        string minotag = activeMino.tag;//�^�O�ƁAdirection(������k)�ŉ�]����
        activeMino.RotateRight();
        // direction��0����3�͈̔͂Ń��s�[�g
        activeMino.direction = (activeMino.direction + 1) % 4;
        //�ǂɓ��������悤�Ȃ�����Ɋ񂹂邪�A�����Ń~�m����
        if (board.OverLeftWall(activeMino))
        {
            
            //���ǃI�[�o�[������E��2�ړ����������ŉ�]
            if (minotag == "Imino" && activeMino.direction == 2)
            {
                //I�~�mdirection1��������E�ɂR����
                activeMino.transform.position += new Vector3(2, 0, 0);
            }
            else
            {
                activeMino.transform.position += new Vector3(1, 0, 0);
            }
        }
        if (board.OverRightWall(activeMino))
        {
            
            //�E�ǃI�[�o�[�����獶�Ɉړ�
            if (minotag == "Imino" && activeMino.direction == 0)
            {
                //I�~�mdirection1�������獶�ɂR����
                activeMino.transform.position += new Vector3(-2, 0, 0);
            }
            else
            {
                activeMino.transform.position += new Vector3(-1, 0, 0);
            }
        }
        //��]�Ɏ��s������t��]�����܂��傤
        if (!board.CheckPosition(activeMino))
        {
            activeMino.RotateLeft();
            activeMino.direction -= 1;
        }
    }
    public void RotateLeftActiveMino()
    {
        string minotag = activeMino.tag;//�^�O�ƁAdirection(������k)�ŉ�]����
        activeMino.RotateLeft();
        // direction��0����3�͈̔͂Ń��s�[�g
        activeMino.direction = (activeMino.direction - 1) % 4;
        if (activeMino.direction < 0)
        {
            activeMino.direction += 4;
        }
        //�ǂɓ��������悤�Ȃ�����Ɋ񂹂邪�A�����Ń~�m����
        if (board.OverLeftWall(activeMino))
        {
            if (minotag == "Imino" && activeMino.direction == 2)
            {
                //I�~�mdirection0��������E�ɂR����
                activeMino.transform.position += new Vector3(2, 0, 0);
            }
            else
            {//0 3
                activeMino.transform.position += new Vector3(1, 0, 0);
            }
        }
        if (board.OverRightWall(activeMino))
        {
            if (minotag == "Imino" && activeMino.direction == 0)
            {
                //I�~�mdirection0��������E�ɂR����
                activeMino.transform.position += new Vector3(-2, 0, 0);
            }
            else
            {//0 3
                activeMino.transform.position += new Vector3(-1, 0, 0);
            }
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
            activeMino.MoveUp();
            //�Œ肳�ꂽ�Ƃ��ɃI�[�o�[���~�b�g�������ǂ����`�F�b�N����
            if (board.OverLimit(activeMino))
            {
            //�Q�[���[�o�[�ɂ���
                GameOver();
            }
            else
            {
                //��ɂ����Ƃ��̏���
                LockDownTimeCheck(activeMino);
                
                //��ɂ����Ƃ��̏���
                if (!isLockDown)//���b�N�_�E���Ȃ烍�b�N�_�E������
                {
                    BottomBoard();
                }
            }
        }
    }




    //���ɒ��n�����Ƃ��̏���
    //�{�[�h�̂����ɒ��������Ɏ��̃u���b�N�𐶐�����֐�
    void BottomBoard()
    {
        //activeMino.MoveUp();//��ɂ͂߂���
        board.SaveBlockInGrid(activeMino);//���̃u���b�N�̈ʒu��o�^���Ă�����
        
        isLockDown = false;//���b�N�_�E��������
        lockDownTimer = 0;//���b�N�_�E���^�C�}�[���Z�b�g
        lastMinoPosY = 21;
        activeMino = spawner.getNext1Mino();//���̃u���b�N����
        activeMino.transform.position = spawner.transform.position;//���̃u���b�N�̈ʒu�ύX
        GhostChange(activeMino);//�S�[�X�g����
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
    /// <summary>
    /// ���b�N�_�E���^�C�}�[
    /// </summary>
    /// ���Z�b�g���ꂸ0.5�b�������烍�b�N�_�E����
    void LockDownTimeCheck(Tetrimino activeMino)
    {
        float posY = activeMino.transform.position.y;
        if ((int)posY < lastMinoPosY)//�ߋ��̃|�W�V������艺�����Ă���Ȃ��
        {
            lastMinoPosY = (int)posY;//�|�W�V�������X�V����
            lockDownTimeStart = Time.time;//�X�^�[�g���Ԃ��X�V����
            lockDownTimer = 0;//���b�N�_�E���^�C�}�[���Z�b�g
            isLockDown = false;
            return;
        }else if ((int)posY == lastMinoPosY)//�ߋ��̃|�W�V�����Ɠ����Ȃ��
        {
            //�^�C�}�[���e�`�F�b�N Time.time�́A�Q�[�����J�n���Ă���̌o�ߎ���
            //�^�C�}�[�̉��Z
            lockDownTimer = lockDownTimeStart + Time.time;//���b�N�_�E���X�^�[�g����̌o�ߎ���
            if (lockDownTimer >= lockDownInterval)
            {
                //���b�N�_�E���^�C�}�[���傫����΃��b�N�_�E��
                isLockDown = true;
            }
        }
    }

    /// <summary>
    /// �n�[�h�h���b�v
    /// </summary>
    public void HardDrop()
    {
        //activeGhost�̈ʒu��activeMino�����ւ�
        Vector3 newpos = activeGhost.transform.position;
        activeMino.transform.position = newpos;
        BottomBoard();
    }


}
