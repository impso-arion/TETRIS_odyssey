using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    //配列の作成
    [SerializeField] List<Tetrimino> Minos;//ミノリストの中にミノを格納する。インスペクターで。
    private List<int> tetriminoIndexes; // ランダムな順序のミノのインデックスを保持するリスト
    private int currentTetriminoIndex; // 現在のミノのインデックス

    private int? next1 = null, next2 = null, next3 = null;//ネクストに格納される値 NullableIntである


    //関数の作成
    //次のテトリミノインデックスを作成




    
    void Start()
    {
        //インデックス生成
        InitializeTetriminoIndexes();
        next1 = tetriminoIndexes[0];
        next2 = tetriminoIndexes[1];
        next3 = tetriminoIndexes[2];
        currentTetriminoIndex = 3;
        Debug.Log("next1:" + next1 + "next1:" + next1 + "next1:" + next1);
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
        for (int i = 0; i < Minos.Count; i++)
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
    private Tetrimino SpawnNextTetrimino()
    {
        // 現在のミノを生成
        int nextIndex = tetriminoIndexes[currentTetriminoIndex];
        Tetrimino nextTetrimino = Minos[nextIndex];
        //Debug.Log(nextIndex);
        // ミノ生成後、次のミノのためにインデックスを更新
        currentTetriminoIndex = (currentTetriminoIndex + 1) % Minos.Count;
        if(currentTetriminoIndex == 0)
        {
            ShuffleIndexes();
        }
        return nextTetrimino;
    }

    /// <summary>
    /// キューのミノを渡す
    /// </summary>
    /// <returns></returns>
    /*
    public Tetrimino getActiveMino()
    {
        Debug.Log("はいりました");
        if (nextQueue != null && nextQueue.Count > 0)
        {

            // nextQueue[0] を取り出してリストから削除
            Tetrimino nextTetrimino = nextQueue[0];
            nextQueue.RemoveAt(0);
            Debug.Log("キューは" + nextQueue.Count);
            // 外部に渡したオブジェクトをSpawnerの位置に移動
            //nextTetrimino.transform.position = transform.position;
            nextTetrimino.transform.position = Vector3.zero;

            // 外部に渡したオブジェクトを返す
            //IsView = true;
            Debug.Log("nextTetrimino: " + nextTetrimino);
            Debug.Log("nextTetrimino position: " + nextTetrimino.transform.position);
            Debug.Log("よい");
            return nextTetrimino;
            
        }
        else
        {
            Debug.Log("わるい");
            return null;
        }
    }
    */



}

