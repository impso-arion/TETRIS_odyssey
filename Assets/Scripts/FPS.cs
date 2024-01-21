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
    /// FPSを定義する
    /// </summary>
    internal void Init()
    {
        Application.targetFrameRate = 60;
        //gameObject.SetActive(true);
    }

    int frm = 0;//フレーム数をカウントする
    float sec = 0, fps = 0;//経過時間をカウントする
    void Update()
    {
        frm++;
        sec += Time.deltaTime;
        //以下で、実際のFPSをチェックして右下に表示する
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
