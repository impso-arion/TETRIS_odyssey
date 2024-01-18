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
    private int currentTetriminoIndex = 0; // 現在のミノのインデックス
    Tetrimino next1, next2, next3;//ネクストのミノ
    //ネクストの場所
    private Vector3 next1pos = new Vector3(12, 20, 0);
    private Vector3 next2pos = new Vector3(12, 15, 0);
    private Vector3 next3pos = new Vector3(12, 10, 0);
    //関数の作成
    //次のテトリミノインデックスを作成

    // ヒエラルキー上のGhostのリストを取得。1度きり
    [SerializeField] private List<Ghost> Ghosts = new List<Ghost>();
    // Ghostのリストを他のスクリプトから読み取るためのプロパティ
    public List<Ghost> GhostList
    {
        get { return Ghosts; }
    }

    void Awake()
    {
        //初期インデックス
        InitializeTetriminoIndexes();
        
        //ゴースト取得
        InitializeGhosts();
    }


    void Start()
    {
        next1 = getSpawnMino();
        next1.transform.position = next1pos;
        next2 = getSpawnMino();
        next2.transform.position = next2pos;
        next3 = getSpawnMino();
        next3.transform.position = next3pos;
    }
    private void Update()
    {
        if(next1 == null)
        {
            next1 = next2;
            next1.transform.position = next1pos;
            next2 = next3;
            next2.transform.position = next2pos;
            next3 = getSpawnMino();
            next3.transform.position = next3pos;
        }
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
    Tetrimino spawnNextTetrimino()
    {
        // 現在のミノを生成
        int nextIndex = tetriminoIndexes[currentTetriminoIndex];
        Tetrimino nextTetrimino = tetriminos[nextIndex];
        //Debug.Log(nextIndex);
        // ミノ生成後、次のミノのためにインデックスを更新
        currentTetriminoIndex = (currentTetriminoIndex + 1) % 7;//7種1順である
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
    Tetrimino getSpawnMino()
    {
        //ミノを生成
        Tetrimino tetrimino = Instantiate(spawnNextTetrimino(),transform.position,
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
    public Tetrimino getNext1Mino()
    {
        if (next1)
        {
            Tetrimino nextMino = next1;
            next1 = null;
            return nextMino;
        }
        else
        {
            return null;
        }
    }

    //すでにヒエラルキーに存在するゴーストをfindして利用しやすいようリストに格納する。
    //ゴーストは、再利用するため。
    private void InitializeGhosts()
    {
        for(int i = 0;i < 7; i++)
        {
            GameObject childObject = transform.GetChild(i).gameObject;
            Ghost ghostComponent = childObject.GetComponent<Ghost>();
            if (ghostComponent != null)
            {
                // Ghostのコンポーネントが取得できた場合、リストに追加
                Ghosts.Add(ghostComponent);
            }
            else
            {
                // Ghostのコンポーネントが取得できなかった場合のエラー処理
                Debug.LogError("Ghostコンポーネントが取得できませんでした。");
            }
        }
        
    }



}

