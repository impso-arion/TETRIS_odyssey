using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    
    //�ϐ��̍쐬
    //�{�[�h��՗p�̎l�p�g�@�i�[�p
    //�{�[�h�̍���
    //�{�[�h�̕�
    //�{�[�h�̍��������p���l
    [SerializeField]
    private Transform emptySprite;//��̊G
    [SerializeField]
    private int height = 30, width = 10, header = 8;



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
        }
        return true;//�����܂��Ă�����
    }
    //�g���ɂ���̂����肷��֐�
    bool BoardOutCheck(int x,int y)
    {
        //X����0�ȏ�Awidth�����By����0�ȏゾ
        return (x >= 0 && x < width && y >= 0);
    }
}
