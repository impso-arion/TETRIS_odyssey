using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //�ϐ��̍쐬
    //Spawner
    Spawner spawner;

    //�������ꂽ�~�m���i�[
    Tetrimino activeMino;



    //spawner�I�u�W�F�N�g��spawner�ϐ��Ɋi�[����R�[�h
    private void Start()
    {
        spawner = GameObject.FindObjectOfType<Spawner>();//�X�|�i�[�Ƃ����R���|�[�l���g�����Ă���I�u�W�F�N�g��T��
        
    }
    private void Update()
    {
        //spawner�N���X����u���b�N�����֐����Ă�ŕϐ��Ɋi�[
        /*
        if (!activeMino)//��̂Ƃ�
        {

            activeMino = spawner.getActiveMino();
            if(activeMino != null )
            {
                Debug.Log("null��Ȃ��ł�");
                activeMino.transform.position = Vector3.zero;
            }

        }
        */
    }




}
