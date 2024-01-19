using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{

    //方角を持つ
    public int direction = 0;
    //方角　0 = 北,1=東,2＝南, 3=西
    //Oテトリミノは回転しない。インスペクターでチェックを外す
    [SerializeField] private bool canRotate = true;
    //初期回転
    private Quaternion initialRotation;

    void Start()
    {
        // 初期状態の回転を記録
        initialRotation = transform.rotation;
    }

    public void Rotate(int minoDirection)
    {
        if (canRotate)
        {

            if (direction != minoDirection)
            {
                //前回と違うようならば
                // 初期状態の回転に戻す
                transform.rotation = initialRotation;

                switch (minoDirection)
                {
                    case 0:
                        transform.Rotate(0, 0, 0);
                        direction = 0;
                        break;
                    case 1:
                        transform.Rotate(0, 0, -90);
                        direction = 1;
                        break;
                    case 2:
                        transform.Rotate(0, 0, -180);
                        direction = 2;
                        break;
                    case 3:
                        transform.Rotate(0, 0, 90);
                        direction = 3;
                        break;
                }
            }
        }
    }
    void Move(Vector3 moveDirection)//動く方向をもらう
    {
        transform.position += moveDirection;
    }
    public void MoveUp()//上に上がる
    {
        Move(new Vector3(0, 1, 0));
    }

}
