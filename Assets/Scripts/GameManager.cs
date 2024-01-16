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

    //変数の作成
    //入力受付タイマー(3種類)
    float nextKeyDownTimer, nextKeyLeftRightTimer, nextKeyRotateTimer;
    //入力インターバル(3種類)
    [SerializeField]
    public float nextKeyDownInterval, nextKeyLeftRightInterval, nextKeyRotateInterval;
    //[SerializeField]
    //public float bottomTouchInterval = 0.5f;

    //着地からの制限時間を考えるならばこれ。
    //public float landingTime = 1.5f; // ミノが着地してからの制限時間
    //private bool landed = false;
    //private float landingTimer = 0f;


    private void Start()
    {
        
        spawner = GameObject.FindObjectOfType<Spawner>();//スポナーというコンポーネントをついているオブジェクトを探す
        board = GameObject.FindObjectOfType<Board>();//ボードを変数に格納
        spawner.transform.position = Rounding.Round(spawner.transform.position);

        //タイマー初期設定
        nextKeyDownTimer = Time.time + nextKeyDownInterval;
        nextKeyLeftRightTimer = Time.time + nextKeyLeftRightInterval;
        nextKeyRotateTimer = Time.time + nextKeyRotateInterval;

        //bottomTouchTimer = Time.time + bottomTouchInterval;

        if (!activeMino)//空のとき
        {
            activeMino = spawner.getNext1Mino();
            //activeMino.transform.position = new Vector3(4.5f, 25f, -1f);
        }
    }
    private void Update()
    {
        //spawnerクラスからブロック生成関数を呼んで変数に格納
        if (!activeMino)//空のとき
        {
            activeMino = spawner.getNext1Mino();
            activeMino.transform.position = spawner.transform.position;
        }
        PlayerInput();
        //Updateで時間の判定をして、判定しだいで落下関数を呼ぶ
        //if (Time.time > nextdropTimer)
        //{
        //nextdropTimer = Time.time + dropInterval;//現在のゲーム時間とドロップインターバル
        //if (activeMino)//中身があるとき
        //{
        //
        //activeMino.MoveDown();

        //Boardクラスで外に出たかチェック
        //if (!board.CheckPosition(activeMino))
        //{
        //はみ出た時に動く
        //activeMino.MoveUp();

        //boardに保存する
        //board.SaveBlockInGrid(activeMino);

        //新しいものが出てくる
        //activeMino = spawner.getNext1Mino() ;
        //activeMino.transform.position = spawner.transform.position;
        //}
        //
        //}
        //}
    }
    //関数作成
    //キーの入力を検知してブロックを動かす関数
    void PlayerInput()
    {
        //ボタン押下、ボタン押しっぱなしを制御、ボタン連打も制御
        if (Input.GetKey(KeyCode.D) && (Time.time > nextKeyLeftRightTimer)
            || Input.GetKeyDown(KeyCode.D))
        {
            activeMino.MoveRight();//右に動かす

            nextKeyLeftRightTimer = Time.time + nextKeyLeftRightInterval;

            //右に行きすぎたら左に戻す
            if (!board.CheckPosition(activeMino))
            {
                activeMino.MoveLeft();
            }
        }
        else if (Input.GetKey(KeyCode.A) && (Time.time > nextKeyLeftRightTimer)
            || Input.GetKeyDown(KeyCode.A))
        {
            activeMino.MoveLeft();//左に動かす

            nextKeyLeftRightTimer = Time.time + nextKeyLeftRightInterval;

            //左に行きすぎたら右に戻す
            if (!board.CheckPosition(activeMino))
            {
                activeMino.MoveRight();
            }
        }
        else if (Input.GetKey(KeyCode.E) && (Time.time > nextKeyRotateTimer)//回転
            || Input.GetKeyDown(KeyCode.E))
        {
            activeMino.RotateRight();
            nextKeyRotateTimer = Time.time + nextKeyRotateInterval;
            //回転に失敗したら逆回転させましょう
            if (!board.CheckPosition(activeMino))
            {
                activeMino.RotateLeft();
            }
        }
        else if (Input.GetKey(KeyCode.S) && (Time.time > nextKeyDownTimer)//下に
            || (Time.time > nextdropTimer))//ボタン連打でなく、時間経過で落ちるようにしたい。
        {
            activeMino.MoveDown();//下に動かす

            nextKeyDownTimer = Time.time + nextKeyDownInterval;
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

    //下に着地したときの処理
    //ボードのそこに着いた時に次のブロックを生成する関数
    void BottomBoard()
    {
        activeMino.MoveUp();//上にはめこむ
        board.SaveBlockInGrid(activeMino);//今のブロックの位置を登録してあげる

        activeMino = spawner.getNext1Mino();//次のブロック生成
        activeMino.transform.position = spawner.transform.position;//次のブロックの位置変更

        nextKeyDownTimer = Time.time;
        nextKeyLeftRightTimer = Time.time;
        nextKeyRotateTimer = Time.time;


        board.ClearAllRows();//埋まっていれば削除する
    }




}
