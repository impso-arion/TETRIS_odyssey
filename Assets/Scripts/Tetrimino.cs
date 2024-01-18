using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tetrimino : MonoBehaviour
{

    
    //回転していいブロックかどうかなど使ってない・・・
    [SerializeField] private bool canRotate = true;
    public int direction = 0;
    //方角　0 = 北,1=東,2＝南, 3=西


    


    //移動用
    void Move(Vector3 moveDirection)//動く方向をもらう
    {
        transform.position += moveDirection;
    }

    //移動関数を呼ぶ関数(4種類)
    public void MoveLeft()
    {
        Move(new Vector3(-1, 0, 0));
    }
    public void MoveRight()
    {
        Move(new Vector3(1, 0, 0));
    }
    public void MoveUp()
    {
        Move(new Vector3(0, 1, 0));
    }
    public void MoveDown()
    {
        Move(new Vector3(0, -1, 0));
    }
    //回転用(2種類)
    public void RotateRight()
    {
        //ミノの種類だけ回転の種類がある
        if (canRotate)
        {
            transform.Rotate(0, 0, -90);
        }
        
    }
    public void RotateLeft()
    {
        if (canRotate)
        {
            transform.Rotate(0, 0, 90);
        }

    }

}
