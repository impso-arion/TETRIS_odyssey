using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ���l���ۂ߂�
/// </summary>
/// Vector2�̒l���ۂ߂鏈���͉��x���g���̂ŕʃN���X
/// �E
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