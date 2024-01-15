using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEditor.Tilemaps;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    //配列の作成
    //[SerializeField] List<Tetrimino> Minos;//ミノリストの中にミノを格納する。インスペクターで。
    [SerializeField] Tetrimino[] tetriminos;//ミノリストの中にミノを格納する。インスペクターで登録します

    private List<int> tetriminoIndexes; // ランダムな順序のミノのインデックスを保持するリスト
    private int currentTetriminoIndex; // 現在のミノのインデックス

    //private int? next1 = null, next2 = null, next3 = null;//ネクストに格納される値 NullableIntである


    //関数の作成
    //次のテトリミノインデックスを作成


    
    void Start()
    {
        //インデックス生成
        InitializeTetriminoIndexes();
        //next1 = tetriminoIndexes[0];
        //next2 = tetriminoIndexes[1];
        //next3 = tetriminoIndexes[2];
        currentTetriminoIndex = 3;
        //Debug.Log("next1:" + next1 + "next1:" + next1 + "next1:" + next1);
    }
    private void Update()
    {
        //next1,next2,next3を表示。存在しなければ生成
        //まず生成
                

        //SpawnNextTetrimino();
    }

    /// <summary>
    /// 初期化
    /// </summary>
    void InitializeTetriminoIndexes()
    {
        // ランダムな順序のミノのインデックスを生成
        tetriminoIndexes = new List<int>();
        for (int i = 0; i < tetriminos.Length; i++)
        {
            tetriminoIndexes.Add(i);
        }
        // インデックスをシャッフル
        ShuffleIndexes();
    }
    /// <summary>
    /// インデックスのシャッフル
    /// </summary>
    void ShuffleIndexes()
    {
        System.Random rng = new System.Random();
        tetriminoIndexes = tetriminoIndexes.OrderBy(x => rng.Next()).ToList();
    }

    /// <summary>
    /// 新ミノ生成
    /// </summary>
    /// <returns>生成ミノを返す</returns>
    Tetrimino GetNextTetrimino()
    {
        // 現在のミノを生成
        int nextIndex = tetriminoIndexes[currentTetriminoIndex];
        Tetrimino nextTetrimino = tetriminos[nextIndex];
        //Debug.Log(nextIndex);
        // ミノ生成後、次のミノのためにインデックスを更新
        currentTetriminoIndex = (currentTetriminoIndex + 1) % tetriminos.Length;
        if(currentTetriminoIndex == 0)
        {
            ShuffleIndexes();
        }
        if (nextTetrimino)
        {
            return nextTetrimino;
        }
        else
        {
            return null;
        }
    }



    /// <summary>
    /// ミノを渡す
    /// </summary>
    /// <returns></returns>
    public Tetrimino getSpawnMino()
    {
        //ミノを生成
        Tetrimino tetrimino = Instantiate(GetNextTetrimino(),transform.position,
            Quaternion.identity);
        if (tetrimino)
        {
            return tetrimino;
        }
        else
        {
            return null;
        }

    }
    



}

