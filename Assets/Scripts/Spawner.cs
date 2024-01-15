using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    //�z��̍쐬
    [SerializeField] List<Tetrimino> Minos;//�~�m���X�g�̒��Ƀ~�m���i�[����B�C���X�y�N�^�[�ŁB
    private List<int> tetriminoIndexes; // �����_���ȏ����̃~�m�̃C���f�b�N�X��ێ����郊�X�g
    private int currentTetriminoIndex; // ���݂̃~�m�̃C���f�b�N�X

    private int? next1 = null, next2 = null, next3 = null;//�l�N�X�g�Ɋi�[�����l NullableInt�ł���


    //�֐��̍쐬
    //���̃e�g���~�m�C���f�b�N�X���쐬




    
    void Start()
    {
        //�C���f�b�N�X����
        InitializeTetriminoIndexes();
        next1 = tetriminoIndexes[0];
        next2 = tetriminoIndexes[1];
        next3 = tetriminoIndexes[2];
        currentTetriminoIndex = 3;
        Debug.Log("next1:" + next1 + "next1:" + next1 + "next1:" + next1);
    }
    private void Update()
    {
        //next1,next2,next3��\���B���݂��Ȃ���ΐ���
        //�܂�����
                

        //SpawnNextTetrimino();
    }

    /// <summary>
    /// ������
    /// </summary>
    void InitializeTetriminoIndexes()
    {
        // �����_���ȏ����̃~�m�̃C���f�b�N�X�𐶐�
        tetriminoIndexes = new List<int>();
        for (int i = 0; i < Minos.Count; i++)
        {
            tetriminoIndexes.Add(i);
        }
        // �C���f�b�N�X���V���b�t��
        ShuffleIndexes();
    }
    /// <summary>
    /// �C���f�b�N�X�̃V���b�t��
    /// </summary>
    void ShuffleIndexes()
    {
        System.Random rng = new System.Random();
        tetriminoIndexes = tetriminoIndexes.OrderBy(x => rng.Next()).ToList();
    }

    /// <summary>
    /// �V�~�m����
    /// </summary>
    /// <returns>�����~�m��Ԃ�</returns>
    private Tetrimino SpawnNextTetrimino()
    {
        // ���݂̃~�m�𐶐�
        int nextIndex = tetriminoIndexes[currentTetriminoIndex];
        Tetrimino nextTetrimino = Minos[nextIndex];
        //Debug.Log(nextIndex);
        // �~�m������A���̃~�m�̂��߂ɃC���f�b�N�X���X�V
        currentTetriminoIndex = (currentTetriminoIndex + 1) % Minos.Count;
        if(currentTetriminoIndex == 0)
        {
            ShuffleIndexes();
        }
        return nextTetrimino;
    }

    /// <summary>
    /// �L���[�̃~�m��n��
    /// </summary>
    /// <returns></returns>
    /*
    public Tetrimino getActiveMino()
    {
        Debug.Log("�͂���܂���");
        if (nextQueue != null && nextQueue.Count > 0)
        {

            // nextQueue[0] �����o���ă��X�g����폜
            Tetrimino nextTetrimino = nextQueue[0];
            nextQueue.RemoveAt(0);
            Debug.Log("�L���[��" + nextQueue.Count);
            // �O���ɓn�����I�u�W�F�N�g��Spawner�̈ʒu�Ɉړ�
            //nextTetrimino.transform.position = transform.position;
            nextTetrimino.transform.position = Vector3.zero;

            // �O���ɓn�����I�u�W�F�N�g��Ԃ�
            //IsView = true;
            Debug.Log("nextTetrimino: " + nextTetrimino);
            Debug.Log("nextTetrimino position: " + nextTetrimino.transform.position);
            Debug.Log("�悢");
            return nextTetrimino;
            
        }
        else
        {
            Debug.Log("��邢");
            return null;
        }
    }
    */



}

