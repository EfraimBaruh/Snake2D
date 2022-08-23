using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils : MonoBehaviour
{
    /// <summary>
    /// left, right, up, down ---- in order
    /// </summary>
    public static Vector2 GetRandomDirection2D()
    {
        int direction = Random.Range(0, 4);

        switch (direction)
        {
            case 0:
                return Vector2.left;
            case 1:
                return Vector2.right;
            case 2:
                return Vector2.up;
            case 3:
                return Vector2.down;
            default:
                return Vector2.left;
        }
    }
}
