/*

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using static Unity.Collections.AllocatorManager;

public class Spawner : MonoBehaviour
{
    //�z��̍쐬(��������u���b�N���ׂĂ��i�[����)
    [SerializeField]
    Tetrimino[] tetriminos;//�~�m�z��̒��Ƀu���b�N���i�[����B�C���X�y�N�^�[�ŁB

    //�֐��̍쐬
    //�����_���ȃu���b�N���ЂƂI�Ԋ֐�
    Tetrimino GetRandomBlock()
    {
        int i = Random.Range(0, tetriminos.Length);//0�ȏ�7�������I�΂��

        if (tetriminos[i])//Block����null�łȂ��Ȃ�
        {
            return tetriminos[i];
        }
        else
        {
            return null;
        }



    }
    //�I�΂ꂽ�u���b�N�𐶐�����֐�
    public Tetrimino SpawnMino()
    {
        //�u���b�N���̂�ɂ����̂Ő����\
        Tetrimino mino = Instantiate(GetRandomBlock(),
            transform.position, Quaternion.identity);
        if (mino)
        {
            return mino;
        }
        else
        {
            return null;
        }

    }




}

*/