using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

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

    //クラシックロックダウンシステムを採用する
    //ミノが下に落ちると、ロックダウンタイマーがリセットされる
    //ミノが同じ高さに0.5秒以上いると、ロックダウンされる。
    private float lockDownInterval = 0.5f; // ミノが着地してからのタイマー
    private float lockDownTime;



    [SerializeField]
    private GameObject gameOverPanel;

    //ゲームオーバー判定
    bool gameOver;

    private void Start()
    {
        
        spawner = GameObject.FindObjectOfType<Spawner>();//スポナーというコンポーネントをついているオブジェクトを探す
        board = GameObject.FindObjectOfType<Board>();//ボードを変数に格納
        spawner.transform.position = Rounding.Round(spawner.transform.position);

        //ロックダウンタイマー初期設定
        lockDownTime = Time.time + lockDownInterval;

        //パネル非表示
        if (gameOverPanel.activeInHierarchy)
        {
            gameOverPanel.SetActive(false);
        }
    }
    private void Update()
    {
        if (gameOver)
        {
            return;
        }
        //spawnerクラスからブロック生成関数を呼んで変数に格納
        if (!activeMino)//空のとき
        {
            activeMino = spawner.getNext1Mino();
            activeMino.transform.position = spawner.transform.position;
        }
        //activeMinoの時間をチェックする関数
        //activeMinoのyをチェックして、数値が下がれば時間をリセットする
        //リセットされず0.5秒すぎたらロックダウン可



        //PlayerInput();
        if ((Time.time > nextdropTimer))//ボタン連打でなく、時間経過で落ちるようにしたい。
        {
            activeMino.MoveDown();//下に動かす
            nextdropTimer = Time.time + dropInterval;

            //下に行きすぎたら固定
            if (!board.CheckPosition(activeMino))
            {
                //固定されたときにオーバーリミットしたかどうかチェックする
                if (board.OverLimit(activeMino))
                {
                //ゲームーバーにする
                    GameOver();
                }
                else
                {
                //ロックダウンタイマー
                //底についたときの処理
                BottomBoard();
                }
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
        // directionを0から3の範囲でリピート
        activeMino.direction = (activeMino.direction + 1) % 4;
        //回転に失敗したら逆回転させましょう
        if (!board.CheckPosition(activeMino))
        {
            activeMino.RotateLeft();
            activeMino.direction -= 1;
        }
    }
    public void RotateLeftActiveMino()
    {
        activeMino.RotateLeft();
        // directionを0から3の範囲でリピート
        activeMino.direction = (activeMino.direction - 1) % 4;
        if (activeMino.direction < 0)
        {
            activeMino.direction += 4;
        }
        //回転に失敗したら逆回転させましょう
        if (!board.CheckPosition(activeMino))
        {
            activeMino.RotateRight();
            activeMino.direction = (activeMino.direction + 1) % 4;
        }
    }

    public void MoveActiveMinoDown()
    {
        activeMino.MoveDown();//下に動かす
        //下に行きすぎたら固定
        if (!board.CheckPosition(activeMino))
        {
            //固定されたときにオーバーリミットしたかどうかチェックする
            if (board.OverLimit(activeMino))
            {
            //ゲームーバーにする
                GameOver();
            }
            else
            {
            //底についたときの処理
                BottomBoard();
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
        board.ClearAllRows();//埋まっていれば削除する
    }

    //ゲームオーバーになったらパネルを表示
    void GameOver()
    {
        activeMino.MoveUp();
        if (!gameOverPanel.activeInHierarchy)
        {
            gameOverPanel.SetActive(true);
        }
        gameOver = true;
    }
    //シーン再読み込み
    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

}
