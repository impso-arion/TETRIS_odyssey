using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{

    //���p������
    public int direction = 0;
    //���p�@0 = �k,1=��,2����, 3=��
    //O�e�g���~�m�͉�]���Ȃ��B�C���X�y�N�^�[�Ń`�F�b�N���O��
    [SerializeField] private bool canRotate = true;
    //������]
    private Quaternion initialRotation;

    void Start()
    {
        // ������Ԃ̉�]���L�^
        initialRotation = transform.rotation;
    }

    public void Rotate(int minoDirection)
    {
        if (canRotate)
        {

            if (direction != minoDirection)
            {
                //�O��ƈႤ�悤�Ȃ��
                // ������Ԃ̉�]�ɖ߂�
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
    void Move(Vector3 moveDirection)//�������������炤
    {
        transform.position += moveDirection;
    }
    public void MoveUp()//��ɏオ��
    {
        Move(new Vector3(0, 1, 0));
    }

}
