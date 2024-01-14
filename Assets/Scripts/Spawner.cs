using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    //�z��̍쐬



    [SerializeField]
    //Tetrimino[] Minos;//(��������~�m���u���b�N�̂��ׂĂ��i�[����)
    public List<GameObject> Minos;
    private List<int> tetriminoIndexes; // �����_���ȏ����̃~�m�̃C���f�b�N�X��ێ����郊�X�g
    private int currentTetriminoIndex; // ���݂̃~�m�̃C���f�b�N�X
    private List<GameObject> nextQueue; // ���ɐ�������~�m�̃L���[

    private bool IsView;//Update�\�����邩�ǂ���
    void Start()
    {
        InitializeTetriminoIndexes();
        currentTetriminoIndex = 0;
        nextQueue = new List<GameObject>();
        IsView = false;
    }
    private void Update()
    {
        //next1,next2,next3��\���B���݂��Ȃ���ΐ���
        //�܂�����
        if(nextQueue.Count < 2)//�J�E���g��3�ȉ��Ȃ�
        {
            for (int i = 0; i < 3; i++)
            {
                nextQueue.Add(SpawnNextTetrimino());
            }
            IsView = true;
        }
        if (IsView)
        {
            Vector3 spawnOffset1 = new Vector3(6.5f, -5f, 0f);//next1�̈ʒu�փI�t�Z�b�g
            Vector3 spawnOffset2 = new Vector3(6.5f, -10f, 0f);//next2�̈ʒu�փI�t�Z�b�g
            Vector3 spawnOffset3 = new Vector3(6.5f, -15f, 0f);//next3�̈ʒu�փI�t�Z�b�g
            //��ʂɕ\��
            Instantiate(nextQueue[0], transform.position + spawnOffset1, Quaternion.identity);
            Instantiate(nextQueue[1], transform.position + spawnOffset2, Quaternion.identity);
            Instantiate(nextQueue[2], transform.position + spawnOffset3, Quaternion.identity);
            IsView = false;
        }
        

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
    private GameObject SpawnNextTetrimino()
    {
        // ���݂̃~�m�𐶐�
        int nextIndex = tetriminoIndexes[currentTetriminoIndex];
        GameObject nextTetrimino = Minos[nextIndex];
        Debug.Log(nextIndex);
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
    public GameObject getActiveMino()
    {
        Debug.Log("�͂���܂���");
        if (nextQueue != null && nextQueue.Count > 0)
        {
            // nextQueue[0] �����o���ă��X�g����폜
            GameObject nextTetrimino = nextQueue[0];
            nextQueue.RemoveAt(0);
            // �O���ɓn�����I�u�W�F�N�g��Spawner�̈ʒu�Ɉړ�
            nextTetrimino.transform.position = transform.position;
            // �O���ɓn�����I�u�W�F�N�g��Ԃ�
            //IsView = true;
            Debug.Log("�悢");
            return nextTetrimino;
            
        }
        else
        {
            Debug.Log("��邢");
            return null;
        }
    }



}

