using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 数値を丸める
/// </summary>
/// Vector2の値を丸める処理は何度も使うので別クラス
/// ・
public static class Rounding
{
    public static Vector2 Round(Vector2 i)
    {
        return new Vector2(Mathf.Round(i.x), Mathf.Round(i.y));
    }
    public static Vector2 Round(Vector3 i)
    {
        return new Vector2(Mathf.Round(i.x), Mathf.Round(i.y));
    }
}