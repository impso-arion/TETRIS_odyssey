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
    float nextdropTimer;

    //着地からの制限時間を考えるならばこれ。
    //public float landingTime = 1.5f; // ミノが着地してからの制限時間
    //private bool landed = false;
    //private float landingTimer = 0f;


    private void Start()
    {
        
        spawner = GameObject.FindObjectOfType<Spawner>();//スポナーというコンポーネントをついているオブジェクトを探す
        board = GameObject.FindObjectOfType<Board>();//ボードを変数に格納
        spawner.transform.position = Rounding.Round(spawner.transform.position);

    }
    private void Update()
    {
        //spawnerクラスからブロック生成関数を呼んで変数に格納
        if (!activeMino)//空のとき
        {
            activeMino = spawner.getNext1Mino();
            activeMino.transform.position = spawner.transform.position;
        }
        //PlayerInput();
        if ((Time.time > nextdropTimer))//ボタン連打でなく、時間経過で落ちるようにしたい。
        {
            activeMino.MoveDown();//下に動かす
            nextdropTimer = Time.time + dropInterval;

            //下に行きすぎたら固定
            if (!board.CheckPosition(activeMino))
            {
                //固定されたときにオーバーリミットしたかどうかチェックする
                //if (board.OverLimit(activeMino))
                //{
                //ゲームーバーにする
                //    GameOver();
                //}
                //else
                //{
                //底についたときの処理
                BottomBoard();
                //}

            }
        }
    }

    
    public void MoveActiveMinoLeft()
    {
        // activeMinoを左に移動する処理
        activeMino.MoveLeft();//左に動かす
        //左に行きすぎたら右に戻す
        if (!board.CheckPosition(activeMino))
        {
            activeMino.MoveRight();
        }
    }
    public void MoveActiveMinoRight()
    {
        // activeMinoを左に移動する処理
        activeMino.MoveRight();//左に動かす
        //左に行きすぎたら右に戻す
        if (!board.CheckPosition(activeMino))
        {
            activeMino.MoveLeft();
        }
    }
    public void RotateRightActiveMino()
    {
        activeMino.RotateRight();
        //回転に失敗したら逆回転させましょう
        if (!board.CheckPosition(activeMino))
        {
            activeMino.RotateLeft();
        }
    }
    public void RotateLeftActiveMino()
    {
        activeMino.RotateLeft();
        //回転に失敗したら逆回転させましょう
        if (!board.CheckPosition(activeMino))
        {
            activeMino.RotateRight();
        }
    }

    public void MoveActiveMinoDown()
    {
        activeMino.MoveDown();//下に動かす
        //下に行きすぎたら固定
        if (!board.CheckPosition(activeMino))
        {
            //固定されたときにオーバーリミットしたかどうかチェックする
            //if (board.OverLimit(activeMino))
            //{
            //ゲームーバーにする
            //    GameOver();
            //}
            //else
            //{
            //底についたときの処理
            BottomBoard();
            //}

        }
    }




    //下に着地したときの処理
    //ボードのそこに着いた時に次のブロックを生成する関数
    void BottomBoard()
    {
        activeMino.MoveUp();//上にはめこむ
        board.SaveBlockInGrid(activeMino);//今のブロックの位置を登録してあげる

        activeMino = spawner.getNext1Mino();//次のブロック生成
        activeMino.transform.position = spawner.transform.position;//次のブロックの位置変更
        board.ClearAllRows();//埋まっていれば削除する
    }




}
