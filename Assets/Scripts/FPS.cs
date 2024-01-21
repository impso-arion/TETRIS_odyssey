using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FPS : MonoBehaviour
{
    public TextMeshProUGUI txtFPS;

    private void Awake()
    {
        Init();
    }

    /// <summary>
    /// FPS���`����
    /// </summary>
    internal void Init()
    {
        Application.targetFrameRate = 60;
        //gameObject.SetActive(true);
    }

    int frm = 0;//�t���[�������J�E���g����
    float sec = 0, fps = 0;//�o�ߎ��Ԃ��J�E���g����
    void Update()
    {
        frm++;
        sec += Time.deltaTime;
        //�ȉ��ŁA���ۂ�FPS���`�F�b�N���ĉE���ɕ\������
        if (sec >= 1f)
        {
            fps = frm / sec;
            txtFPS.text = string.Format(
              "{0:00}", fps
            ) + " fps";
            frm = 0;
            sec = 0;
        }
    }

}
