using System.Collections;
using System.Collections.Generic;
//using UnityEditor.ShaderKeywordFilter;
using UnityEngine;
using static Unity.Collections.AllocatorManager;

public class Board : MonoBehaviour
{
    //2�����z��̍쐬
    private Transform[,] grid;
    
    //�ϐ��̍쐬
    //�{�[�h��՗p�̎l�p�g�@�i�[�p
    //�{�[�h�̍���
    //�{�[�h�̕�
    //�{�[�h�̍��������p���l
    [SerializeField]
    private Transform emptySprite;//��̊G
    [SerializeField]
    private int height = 30, width = 10, header = 8;


    private void Awake()
    {
        grid = new Transform[width, height];
    }

    private void Start()
    {
        CreateBoard();
    }

    //�֐��̍쐬
    //�{�[�h�𐶐�����֐��̍쐬
    void CreateBoard()
    {
        if (emptySprite)
        {
            for (int y = 0; y < height - header; y++) 
            {
                for(int x = 0; x < width; x++)
                {
                    Transform clone = Instantiate(emptySprite,
                        new Vector3(x,y,0),Quaternion.identity);

                    clone.transform.parent = transform;
                }
            }
        }
    }

    //�u���b�N���g���ɂ���̂����肷��֐�
    public bool CheckPosition(Tetrimino tetrimino)
    {
        foreach(Transform item in tetrimino.transform)//tetrimino�Ɋ܂܂�Ă���transform�̐��������[�v���񂷁B
        {
            //���l���ۂ߂�
            Vector2 pos = new Vector2(Mathf.Round(item.position.x),
                Mathf.Round(item.position.y));
            //�g����݂͂����Ă��Ȃ��̂��`�F�b�N
            if(!BoardOutCheck((int)pos.x,(int)pos.y))
            {
                return false;
            }
            //���łɑ��̃u���b�N�����邩�`�F�b�N
            if (BlockCheck((int)pos.x,(int)pos.y,tetrimino))
            {
                return false;
            }
        }
        return true;//�����܂��Ă�����
    }
    //�g���ɂ���̂����肷��֐�
    bool BoardOutCheck(int x,int y)
    {
        //X����0�ȏ�Awidth�����By����0�ȏゾ
        return (x >= 0 && x < width && y >= 0);
    }
    //�ړ���Ƀu���b�N���Ȃ������肷��֐�
    bool BlockCheck(int x, int y,Tetrimino tetrimino)
    {
        //�����z�񂪋�łȂ��@�A���h�@grid�̐e��tetrimino�̐e���Ⴄ
        //�Ƃ����Ƃ���true��Ԃ��Ă����
        return (grid[x, y] != null && grid[x, y].parent != tetrimino.transform);
    }
    
    //�u���b�N���������|�W�V�������L�^����֐�
    public void SaveBlockInGrid(Tetrimino tetrimino)
    {
        foreach(Transform item in tetrimino.transform)
        {
            //���l���ۂ߂�
            Vector2 pos = Rounding.Round(item.position);
            //�ϐ��Ɋi�[����
            grid[(int)pos.x, (int)pos.y] = item;
        }
    }



    //�֐��̍쐬
    //���ׂĂ̍s���`�F�b�N���āA���܂��Ă���΍폜����֐�
    public void ClearAllRows()
    {
        for (int y = 0; y < height; y++)
        {
            //���܂��Ă��邩�`�F�b�N����
            if (IsComplete(y))
            {
                //�폜����
                ClearRow(y);

                //��i�폜����
                ShiftRowsDown(y + 1);//�v���X�P����̂́Ay�̏�̒i�����ɉ�����̂�
                y--;//y�𐳏�Ȓl�ɖ߂�
            }
        }
    }
    //���ׂĂ̍s���`�F�b�N����֐�
    bool IsComplete(int y)
    {
        for (int x = 0; x < width; x++)
        {
            if (grid[x, y] == null)//����2�����z��̍s�͋󔒂ł���
            {
                return false;
            }
        }
        return true;
    }
    //�폜����֐�
    void ClearRow(int y)
    {
        for (int x = 0; x < width; x++)
        {
            if (grid[x, y] != null)//����2�����z��̍s�͋󔒂ł���
            {
                Destroy(grid[x, y].gameObject);//�Y���I�u�W�F�N�g���폜
            }
            grid[x, y] = null;//�f�[�^���폜
        }
    }
    //��ɂ���u���b�N��1�i������֐�
    void ShiftRowsDown(int startY)
    {
        for (int y = startY; y < height; y++)//
        {
            for (int x = 0; x < width; x++)
            {
                if (grid[x, y] != null)
                {
                    grid[x, y - 1] = grid[x, y];//���݂̃O���b�h����i����
                    grid[x, y] = null;
                    //�O���b�h�̒��g�����łȂ��A�O���b�h�̃I�u�W�F�N�g�̃|�W�V�������ύX����
                    grid[x, y - 1].position += new Vector3(0, -1, 0);
                }
            }
        }
    }

    //�~�m���������C�����I�[�o�[�������ǂ���
    public bool OverLimit(Tetrimino tetrimino)
    {
        foreach (Transform item in tetrimino.transform)
        {
            if (item.transform.position.y >= height - header)
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// �E�ǂ��I�[�o�[������true
    /// </summary>
    /// <param name="tetrimino"></param>
    /// <returns></returns>
    public bool OverRightWall(Tetrimino tetrimino)
    {
        foreach(Transform item in tetrimino.transform)
        {
            if(item.transform.position.x > width-1)
            {
                return true;
            }
        }
        return false;
    }
    /// <summary>
    /// ���ǂ��I�[�o�[������true
    /// </summary>
    /// <param name="tetrimino"></param>
    /// <returns></returns>
    public bool OverLeftWall(Tetrimino tetrimino)
    {
        foreach (Transform item in tetrimino.transform)
        {
            if (item.transform.position.x < 0)
            {
                return true;
            }
        }
        return false;
    }

    public bool ghostCheckPosition(Ghost activeGhost)//true�Ȃ炨���܂��Ă���
    {
        foreach (Transform item in activeGhost.transform)//tetrimino�Ɋ܂܂�Ă���transform�̐��������[�v���񂷁B
        {
            //���l���ۂ߂�
            Vector2 pos = new Vector2(Mathf.Round(item.position.x),
                Mathf.Round(item.position.y));
            //�g����݂͂����Ă��Ȃ��̂��`�F�b�N
            if (!BoardOutCheck((int)pos.x, (int)pos.y))
            {
                return false;
            }
            //���łɑ��̃u���b�N�����邩�`�F�b�N
            if (GhostBlockCheck((int)pos.x, (int)pos.y, activeGhost))
            {
                return false;
            }
        }
        return true;//�����܂��Ă�����
    }
    bool GhostBlockCheck(int x, int y, Ghost ghost)
    {
        //�����z�񂪋�łȂ��@�A���h�@grid�̐e��ghost�̐e���Ⴄ
        //�Ƃ����Ƃ���true��Ԃ��Ă����
        return (grid[x, y] != null && grid[x, y].parent != ghost.transform);
    }

}
