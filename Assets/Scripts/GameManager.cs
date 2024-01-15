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



    //spawnerオブジェクトをspawner変数に格納するコード
    private void Start()
    {
        spawner = GameObject.FindObjectOfType<Spawner>();//スポナーというコンポーネントをついているオブジェクトを探す
        
    }
    private void Update()
    {
        //spawnerクラスからブロック生成関数を呼んで変数に格納
        /*
        if (!activeMino)//空のとき
        {

            activeMino = spawner.getActiveMino();
            if(activeMino != null )
            {
                Debug.Log("nullやないでえ");
                activeMino.transform.position = Vector3.zero;
            }

        }
        */
    }




}
