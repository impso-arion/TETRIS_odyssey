using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEditor.Tilemaps;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    //�z��̍쐬
    //[SerializeField] List<Tetrimino> Minos;//�~�m���X�g�̒��Ƀ~�m���i�[����B�C���X�y�N�^�[�ŁB
    [SerializeField] Tetrimino[] tetriminos;//�~�m���X�g�̒��Ƀ~�m���i�[����B�C���X�y�N�^�[�œo�^���܂�
    

    private List<int> tetriminoIndexes; // �����_���ȏ����̃~�m�̃C���f�b�N�X��ێ����郊�X�g
    private int currentTetriminoIndex = 0; // ���݂̃~�m�̃C���f�b�N�X
    Tetrimino next1, next2, next3;//�l�N�X�g�̃~�m
    //�l�N�X�g�̏ꏊ
    private Vector3 next1pos = new Vector3(12, 20, 0);
    private Vector3 next2pos = new Vector3(12, 15, 0);
    private Vector3 next3pos = new Vector3(12, 10, 0);
    //�֐��̍쐬
    //���̃e�g���~�m�C���f�b�N�X���쐬

    // �q�G�����L�[���Ghost�̃��X�g��find�Ŏ擾�B1�x����
    private List<GameObject> Ghosts = new List<GameObject>();

    void Awake()
    {
        //�����C���f�b�N�X
        InitializeTetriminoIndexes();
        
        //�S�[�X�g�擾
        InitializeGhosts();
    }


    void Start()
    {
        next1 = getSpawnMino();
        next1.transform.position = next1pos;
        next2 = getSpawnMino();
        next2.transform.position = next2pos;
        next3 = getSpawnMino();
        next3.transform.position = next3pos;
    }
    private void Update()
    {
        if(next1 == null)
        {
            next1 = next2;
            next1.transform.position = next1pos;
            next2 = next3;
            next2.transform.position = next2pos;
            next3 = getSpawnMino();
            next3.transform.position = next3pos;
        }
    }
    /// <summary>
    /// ������
    /// </summary>
    void InitializeTetriminoIndexes()
    {
        // �����_���ȏ����̃~�m�̃C���f�b�N�X�𐶐�
        tetriminoIndexes = new List<int>();
        for (int i = 0; i < tetriminos.Length; i++)
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
    Tetrimino spawnNextTetrimino()
    {
        // ���݂̃~�m�𐶐�
        int nextIndex = tetriminoIndexes[currentTetriminoIndex];
        Tetrimino nextTetrimino = tetriminos[nextIndex];
        //Debug.Log(nextIndex);
        // �~�m������A���̃~�m�̂��߂ɃC���f�b�N�X���X�V
        currentTetriminoIndex = (currentTetriminoIndex + 1) % 7;//7��1���ł���
        if(currentTetriminoIndex == 0)
        {
            ShuffleIndexes();
        }
        if (nextTetrimino)
        {
            return nextTetrimino;
        }
        else
        {
            return null;
        }
    }
    /// <summary>
    /// �~�m��n��
    /// </summary>
    /// <returns></returns>
    Tetrimino getSpawnMino()
    {
        //�~�m�𐶐�
        Tetrimino tetrimino = Instantiate(spawnNextTetrimino(),transform.position,
            Quaternion.identity);
        if (tetrimino)
        {
            return tetrimino;
        }
        else
        {
            return null;
        }
    }
    public Tetrimino getNext1Mino()
    {
        if (next1)
        {
            Tetrimino nextMino = next1;
            next1 = null;
            return nextMino;
        }
        else
        {
            return null;
        }
    }

    //�S�[�X�g��find���ė��p���₷���悤���X�g�Ɋi�[����B
    private void InitializeGhosts()
    {
        
        
    }



}

