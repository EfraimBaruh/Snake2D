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
    
    /// <summary>
    /// Return random position inside the walls.
    /// </summary>
    /// <param name="mainCamera"></param>
    /// <returns></returns>
    public static Vector2 GetRandomPosition(Camera mainCamera)
    {
        // Return random inside the wallls.

        float left = Screen.width / 5f;
        float right = Screen.width * (10f / 12f);
        float up = Screen.height * (13f / 15f);
        float down = Screen.height / 14f;
        
        float spawnY = Random.Range
            (mainCamera.ScreenToWorldPoint(new Vector2(0, down)).y, mainCamera.ScreenToWorldPoint(new Vector2(0, up)).y);
        float spawnX = Random.Range
            (mainCamera.ScreenToWorldPoint(new Vector2(left, 0)).x, mainCamera.ScreenToWorldPoint(new Vector2(right, 0)).x);
 
        Vector2 spawnPosition = new Vector2(spawnX, spawnY);
        return spawnPosition;
    }

    public static string GetEdibleTag(int score)
    {
        // if score is less than 5, just increase the size.
        if (score < 5)
            return "increase";

        int randTag = Random.Range(0, 7);
        switch (randTag)
        {
            case 0:
            case 1:
            case 2:
                return "increase";
            case 3:
                return "decrease";
            case 4:
                return "slowdown";
            case 5:
                return "speedup";
            case 6:
                return "reverse";
            default:
                return null;
        }
    }
}
