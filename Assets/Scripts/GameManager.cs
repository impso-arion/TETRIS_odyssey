using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //変数の作成
    //Spawner
    Spawner spawner;

    //生成されたミノを格納
    Tetrimino activeMino;
    //ボードのスクリプトを格納
    Board board;

    //次にブロックが落ちるまでのインターバル時間
    //次のブロックが落ちるまでの時間
    [SerializeField] private float dropInterval = 0.25f;
    [SerializeField] float nextdropTimer;

    //spawnerオブジェクトをspawner変数に格納するコード
    private void Start()
    {
        
        spawner = GameObject.FindObjectOfType<Spawner>();//スポナーというコンポーネントをついているオブジェクトを探す
        board = GameObject.FindObjectOfType<Board>();//ボードを変数に格納
    }
    private void Update()
    {
        //spawnerクラスからブロック生成関数を呼んで変数に格納

        if (!activeMino)//空のとき
        {

            activeMino = spawner.getSpawnMino();


        }
        //Updateで時間の判定をして、判定しだいで落下関数を呼ぶ
        if (Time.time > nextdropTimer)
        {
            nextdropTimer = Time.time + dropInterval;//現在のゲーム時間とドロップインターバル
            if (activeMino)//中身があるとき
            {

                activeMino.MoveDown();

                //Boardクラスで外に出たかチェック
                if (!board.CheckPosition(activeMino))
                {
                    //はみ出た時に動く
                    activeMino.MoveUp();
                    //少し待つ
                    //新しいものが出てくる
                    activeMino = spawner.getSpawnMino() ;
                }

            }
        }



        
        
    }




}
