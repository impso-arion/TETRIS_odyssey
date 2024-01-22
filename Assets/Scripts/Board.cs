using System.Collections;
using System.Collections.Generic;
//using UnityEditor.ShaderKeywordFilter;
using UnityEngine;
using static Unity.Collections.AllocatorManager;

public class Board : MonoBehaviour
{
    //2次元配列の作成
    private Transform[,] grid;
    
    //変数の作成
    //ボード基盤用の四角枠　格納用
    //ボードの高さ
    //ボードの幅
    //ボードの高さ調整用数値
    [SerializeField]
    private Transform emptySprite;//空の絵
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

    //関数の作成
    //ボードを生成する関数の作成
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

    //ブロックが枠内にあるのか判定する関数
    public bool CheckPosition(Tetrimino tetrimino)
    {
        foreach(Transform item in tetrimino.transform)//tetriminoに含まれているtransformの数だけループを回す。
        {
            //数値を丸める
            Vector2 pos = new Vector2(Mathf.Round(item.position.x),
                Mathf.Round(item.position.y));
            //枠からはみだしていないのかチェック
            if(!BoardOutCheck((int)pos.x,(int)pos.y))
            {
                return false;
            }
            //すでに他のブロックがあるかチェック
            if (BlockCheck((int)pos.x,(int)pos.y,tetrimino))
            {
                return false;
            }
        }
        return true;//おさまっていたよ
    }
    //枠内にあるのか判定する関数
    bool BoardOutCheck(int x,int y)
    {
        //X軸は0以上、width未満。y軸は0以上だ
        return (x >= 0 && x < width && y >= 0);
    }
    //移動先にブロックがないか判定する関数
    bool BlockCheck(int x, int y,Tetrimino tetrimino)
    {
        //次元配列が空でない　アンド　gridの親とtetriminoの親が違う
        //というときにtrueを返してくれる
        return (grid[x, y] != null && grid[x, y].parent != tetrimino.transform);
    }
    
    //ブロックが落ちたポジションを記録する関数
    public void SaveBlockInGrid(Tetrimino tetrimino)
    {
        foreach(Transform item in tetrimino.transform)
        {
            //数値を丸める
            Vector2 pos = Rounding.Round(item.position);
            //変数に格納する
            grid[(int)pos.x, (int)pos.y] = item;
        }
    }



    //関数の作成
    //すべての行をチェックして、埋まっていれば削除する関数
    public void ClearAllRows()
    {
        for (int y = 0; y < height; y++)
        {
            //埋まっているかチェックする
            if (IsComplete(y))
            {
                //削除する
                ClearRow(y);

                //一段削除する
                ShiftRowsDown(y + 1);//プラス１するのは、yの上の段を下に下げるので
                y--;//yを正常な値に戻す
            }
        }
    }
    //すべての行をチェックする関数
    bool IsComplete(int y)
    {
        for (int x = 0; x < width; x++)
        {
            if (grid[x, y] == null)//その2次元配列の行は空白ですか
            {
                return false;
            }
        }
        return true;
    }
    //削除する関数
    void ClearRow(int y)
    {
        for (int x = 0; x < width; x++)
        {
            if (grid[x, y] != null)//その2次元配列の行は空白ですか
            {
                Destroy(grid[x, y].gameObject);//該当オブジェクトを削除
            }
            grid[x, y] = null;//データも削除
        }
    }
    //上にあるブロックを1段下げる関数
    void ShiftRowsDown(int startY)
    {
        for (int y = startY; y < height; y++)//
        {
            for (int x = 0; x < width; x++)
            {
                if (grid[x, y] != null)
                {
                    grid[x, y - 1] = grid[x, y];//現在のグリッドを一段下に
                    grid[x, y] = null;
                    //グリッドの中身だけでなく、グリッドのオブジェクトのポジションも変更する
                    grid[x, y - 1].position += new Vector3(0, -1, 0);
                }
            }
        }
    }

    //ミノが高さラインをオーバーしたかどうか
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
    /// 右壁をオーバーしたらtrue
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
    /// 左壁をオーバーしたらtrue
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

    public bool ghostCheckPosition(Ghost activeGhost)//trueならおさまっている
    {
        foreach (Transform item in activeGhost.transform)//tetriminoに含まれているtransformの数だけループを回す。
        {
            //数値を丸める
            Vector2 pos = new Vector2(Mathf.Round(item.position.x),
                Mathf.Round(item.position.y));
            //枠からはみだしていないのかチェック
            if (!BoardOutCheck((int)pos.x, (int)pos.y))
            {
                return false;
            }
            //すでに他のブロックがあるかチェック
            if (GhostBlockCheck((int)pos.x, (int)pos.y, activeGhost))
            {
                return false;
            }
        }
        return true;//おさまっていたよ
    }
    bool GhostBlockCheck(int x, int y, Ghost ghost)
    {
        //次元配列が空でない　アンド　gridの親とghostの親が違う
        //というときにtrueを返してくれる
        return (grid[x, y] != null && grid[x, y].parent != ghost.transform);
    }

}
