using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tetrimino : MonoBehaviour
{

    
    //��]���Ă����u���b�N���ǂ���
    [SerializeField] private bool canRotate = true;
    public int direction = 0;
    //���p�@0 = �k,1=��,2����, 3=��


    


    //�ړ��p
    void Move(Vector3 moveDirection)//�������������炤
    {
        transform.position += moveDirection;
    }

    //�ړ��֐����ĂԊ֐�(4���)
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
    //��]�p(2���)
    public void RotateRight()
    {
        //�~�m�̎�ނ�����]�̎�ނ�����
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
