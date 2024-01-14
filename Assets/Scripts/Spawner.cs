using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    //配列の作成



    [SerializeField]
    //Tetrimino[] Minos;//(生成するミノ内ブロックのすべてを格納する)
    public List<GameObject> Minos;
    private List<int> tetriminoIndexes; // ランダムな順序のミノのインデックスを保持するリスト
    private int currentTetriminoIndex; // 現在のミノのインデックス
    private List<GameObject> nextQueue; // 次に生成するミノのキュー

    private bool IsView;//Update表示するかどうか
    void Start()
    {
        InitializeTetriminoIndexes();
        currentTetriminoIndex = 0;
        nextQueue = new List<GameObject>();
        IsView = false;
    }
    private void Update()
    {
        //next1,next2,next3を表示。存在しなければ生成
        //まず生成
        if(nextQueue.Count < 2)//カウントが3以下なら
        {
            for (int i = 0; i < 3; i++)
            {
                nextQueue.Add(SpawnNextTetrimino());
            }
            IsView = true;
        }
        if (IsView)
        {
            Vector3 spawnOffset1 = new Vector3(6.5f, -5f, 0f);//next1の位置へオフセット
            Vector3 spawnOffset2 = new Vector3(6.5f, -10f, 0f);//next2の位置へオフセット
            Vector3 spawnOffset3 = new Vector3(6.5f, -15f, 0f);//next3の位置へオフセット
            //画面に表示
            Instantiate(nextQueue[0], transform.position + spawnOffset1, Quaternion.identity);
            Instantiate(nextQueue[1], transform.position + spawnOffset2, Quaternion.identity);
            Instantiate(nextQueue[2], transform.position + spawnOffset3, Quaternion.identity);
            IsView = false;
        }
        

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
    private GameObject SpawnNextTetrimino()
    {
        // 現在のミノを生成
        int nextIndex = tetriminoIndexes[currentTetriminoIndex];
        GameObject nextTetrimino = Minos[nextIndex];
        Debug.Log(nextIndex);
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
    public GameObject getActiveMino()
    {
        Debug.Log("はいりました");
        if (nextQueue != null && nextQueue.Count > 0)
        {
            // nextQueue[0] を取り出してリストから削除
            GameObject nextTetrimino = nextQueue[0];
            nextQueue.RemoveAt(0);
            // 外部に渡したオブジェクトをSpawnerの位置に移動
            nextTetrimino.transform.position = transform.position;
            // 外部に渡したオブジェクトを返す
            //IsView = true;
            Debug.Log("よい");
            return nextTetrimino;
            
        }
        else
        {
            Debug.Log("わるい");
            return null;
        }
    }



}

