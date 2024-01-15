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
    //配列の作成(生成するブロックすべてを格納する)
    [SerializeField]
    Tetrimino[] tetriminos;//ミノ配列の中にブロックを格納する。インスペクターで。

    //関数の作成
    //ランダムなブロックをひとつ選ぶ関数
    Tetrimino GetRandomBlock()
    {
        int i = Random.Range(0, tetriminos.Length);//0以上7未満が選ばれる

        if (tetriminos[i])//Block内がnullでないなら
        {
            return tetriminos[i];
        }
        else
        {
            return null;
        }



    }
    //選ばれたブロックを生成する関数
    public Tetrimino SpawnMino()
    {
        //ブロックを採りにいくので生成可能
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