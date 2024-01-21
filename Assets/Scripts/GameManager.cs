using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    //Spawner
    Spawner spawner;

    //生成されたミノを格納
    Tetrimino activeMino;
    //生成されたゴーストを格納
    Ghost activeGhost;
    private List<Ghost> ghosts;
    //ボードのスクリプトを格納
    Board board;

    //次にブロックが落ちるまでのインターバル時間
    //次のブロックが落ちるまでの時間
    [SerializeField] private float dropInterval = 0.25f;
    float nextdropTimer;

    //クラシックロックダウンシステムを採用する
    //ミノが下に落ちると、ロックダウンタイマーがリセットされる
    //ミノが同じ高さに0.5秒以上いると、ロックダウンされる。
    private float lockDownInterval = 0.5f; // ミノが着地してからのインターバル
    private float lockDownTimer;//ロックダウンタイマー
    [SerializeField] private float lockDownTimeStart;//ロックダウンタイマースタート時間
    [SerializeField] private int lastMinoPosY = 21;//初期位置は21
    [SerializeField]private bool isLockDown = false;


    [SerializeField]
    private GameObject gameOverPanel;

    //ゲームオーバー判定
    public bool gameOver;

    private void Start()
    {
        
        spawner = GameObject.FindObjectOfType<Spawner>();//スポナーというコンポーネントをついているオブジェクトを探す
        board = GameObject.FindObjectOfType<Board>();//ボードを変数に格納
        spawner.transform.position = Rounding.Round(spawner.transform.position);

        //Ghostリストの取得
        ghosts = spawner.GhostList;

        //ロックダウンタイマー初期設定
        lockDownTimer = 0;

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
        if (!activeMino)//空のとき。これは初回だけ呼ばれるだろう
        {
            //spawnerクラスからブロック生成関数を呼んで変数に格納
            activeMino = spawner.getNext1Mino();
            activeMino.transform.position = spawner.transform.position;
            //ghostsの中からひとつ選択してactiveGhostにする。
            GhostChange(activeMino);
            //activeGhostの位置はactiveMinoに追従する
        }
        //ゴーストの位置を決める
        GhostPosition(activeMino,activeGhost);

        if ((Time.time > nextdropTimer))//ボタン連打でなく、時間経過による落下。
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
                    activeMino.MoveUp();
                    LockDownTimeCheck(activeMino);
                }
            }
        }
        //底についたときの処理
        if (isLockDown)//ロックダウン可ならロックダウンする
        {
            BottomBoard();
        }
    }

    void GhostPosition(Tetrimino activeMino,Ghost activeGhost)
    {
        //ゴーストの位置を決める
        //方角をもとに回転
        int direct = activeMino.direction;
        activeGhost.Rotate(direct);
        //pos.xはactiveMinoに追従
        float posX = activeMino.transform.position.x;
        activeGhost.transform.position = new Vector3(posX,20,0);//とりあえず最上部
        //pos.yは最上部から順にチェック
        for (int i = 20; i >= 0; i--)
        {
            activeGhost.transform.position = new Vector3(posX, i, 0);
            if (!board.ghostCheckPosition(activeGhost))//
            {
                //ポジションチェック。一度あたればOK
                activeGhost.transform.position = new Vector3(posX, i+1, 0);
                break;
            }
        }


    }



    //
    /// <summary>
    /// ゴースト呼び出しと戻し
    /// </summary>
    /// <param name="activeMino"></param>
    /// 待機ゴーストの位置はVector3(-100,0,0)非表示にはしない
    void GhostChange(Tetrimino activeMino)
    {
        Vector3 prPos = new Vector3(-100, 0, 0);
        //activeMinoから、とりだすゴーストをゲット。
        //それ以外のゴーストたちは画面外に飛ばす。
        string tagname = activeMino.tag;
        //Debug.Log(tagname);
        // Ghostリストが空でないことを確認してから利用
        if (ghosts.Count > 0)
        {
            //すべてのGhostを先にしまう
            for(int i = 0; i < ghosts.Count; i++)
            {
                ghosts[i].transform.position = prPos;
            }
            switch (tagname)
            {
                case "Imino":
                    activeGhost = ghosts[0];
                    break;
                case "Jmino":
                    activeGhost = ghosts[1];
                    break;
                case "Lmino":
                    activeGhost = ghosts[2];
                    break;
                case "Omino":
                    activeGhost = ghosts[3];
                    break;
                case "Smino":
                    activeGhost = ghosts[4];
                    break;
                case "Tmino":
                    activeGhost = ghosts[5];
                    break;
                case "Zmino":
                    activeGhost = ghosts[6];
                    break;
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
        string minotag = activeMino.tag;//タグと、direction(東西南北)で回転判定
        activeMino.RotateRight();
        // directionを0から3の範囲でリピート
        activeMino.direction = (activeMino.direction + 1) % 4;
        //壁に当たったようなら内側に寄せるが、ここでミノ制御
        if (board.OverLeftWall(activeMino))
        {
            
            //左壁オーバーしたら右に2移動したうえで回転
            if (minotag == "Imino" && activeMino.direction == 2)
            {
                //Iミノdirection1だったら右に３動く
                activeMino.transform.position += new Vector3(2, 0, 0);
            }
            else
            {
                activeMino.transform.position += new Vector3(1, 0, 0);
            }
        }
        if (board.OverRightWall(activeMino))
        {
            
            //右壁オーバーしたら左に移動
            if (minotag == "Imino" && activeMino.direction == 0)
            {
                //Iミノdirection1だったら左に３動く
                activeMino.transform.position += new Vector3(-2, 0, 0);
            }
            else
            {
                activeMino.transform.position += new Vector3(-1, 0, 0);
            }
        }
        //回転に失敗したら逆回転させましょう
        if (!board.CheckPosition(activeMino))
        {
            activeMino.RotateLeft();
            activeMino.direction -= 1;
        }
    }
    public void RotateLeftActiveMino()
    {
        string minotag = activeMino.tag;//タグと、direction(東西南北)で回転判定
        activeMino.RotateLeft();
        // directionを0から3の範囲でリピート
        activeMino.direction = (activeMino.direction - 1) % 4;
        if (activeMino.direction < 0)
        {
            activeMino.direction += 4;
        }
        //壁に当たったようなら内側に寄せるが、ここでミノ制御
        if (board.OverLeftWall(activeMino))
        {
            if (minotag == "Imino" && activeMino.direction == 2)
            {
                //Iミノdirection0だったら右に３動く
                activeMino.transform.position += new Vector3(2, 0, 0);
            }
            else
            {//0 3
                activeMino.transform.position += new Vector3(1, 0, 0);
            }
        }
        if (board.OverRightWall(activeMino))
        {
            if (minotag == "Imino" && activeMino.direction == 0)
            {
                //Iミノdirection0だったら右に３動く
                activeMino.transform.position += new Vector3(-2, 0, 0);
            }
            else
            {//0 3
                activeMino.transform.position += new Vector3(-1, 0, 0);
            }
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
            activeMino.MoveUp();
            //固定されたときにオーバーリミットしたかどうかチェックする
            if (board.OverLimit(activeMino))
            {
            //ゲームーバーにする
                GameOver();
            }
            else
            {
                //底についたときの処理
                LockDownTimeCheck(activeMino);
                
                //底についたときの処理
                if (!isLockDown)//ロックダウン可ならロックダウンする
                {
                    BottomBoard();
                }
            }
        }
    }




    //下に着地したときの処理
    //ボードのそこに着いた時に次のブロックを生成する関数
    void BottomBoard()
    {
        //activeMino.MoveUp();//上にはめこむ
        board.SaveBlockInGrid(activeMino);//今のブロックの位置を登録してあげる
        
        isLockDown = false;//ロックダウン初期化
        lockDownTimer = 0;//ロックダウンタイマーリセット
        lastMinoPosY = 21;
        activeMino = spawner.getNext1Mino();//次のブロック生成
        activeMino.transform.position = spawner.transform.position;//次のブロックの位置変更
        GhostChange(activeMino);//ゴースト生成
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
    /// <summary>
    /// ロックダウンタイマー
    /// </summary>
    /// リセットされず0.5秒すぎたらロックダウン可
    void LockDownTimeCheck(Tetrimino activeMino)
    {
        float posY = activeMino.transform.position.y;
        if ((int)posY < lastMinoPosY)//過去のポジションより下がっているならば
        {
            lastMinoPosY = (int)posY;//ポジションを更新する
            lockDownTimeStart = Time.time;//スタート時間を更新する
            lockDownTimer = 0;//ロックダウンタイマーリセット
            isLockDown = false;
            return;
        }else if ((int)posY == lastMinoPosY)//過去のポジションと同じならば
        {
            //タイマー内容チェック Time.timeは、ゲームが開始してからの経過時間
            //タイマーの加算
            lockDownTimer = lockDownTimeStart + Time.time;//ロックダウンスタートからの経過時間
            if (lockDownTimer >= lockDownInterval)
            {
                //ロックダウンタイマーが大きければロックダウン
                isLockDown = true;
            }
        }
    }

    /// <summary>
    /// ハードドロップ
    /// </summary>
    public void HardDrop()
    {
        //activeGhostの位置にactiveMinoを入れ替え
        Vector3 newpos = activeGhost.transform.position;
        activeMino.transform.position = newpos;
        BottomBoard();
    }


}
