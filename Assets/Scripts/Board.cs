using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    
    //変数の作成
    //ボード基盤用の四角枠　格納用
    //ボードの高さ
    //ボードの幅
    //ボードの高さ調整用数値
    [SerializeField]
    private Transform emptySprite;//空の絵
    [SerializeField]
    private int height = 30, width = 10, header = 8;



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
        }
        return true;//おさまっていたよ
    }
    //枠内にあるのか判定する関数
    bool BoardOutCheck(int x,int y)
    {
        //X軸は0以上、width未満。y軸は0以上だ
        return (x >= 0 && x < width && y >= 0);
    }
}
