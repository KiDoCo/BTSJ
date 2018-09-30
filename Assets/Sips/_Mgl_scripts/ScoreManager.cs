using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static int killedEnemies = 0;
    public static float steveSurvivedTime = 0f;

    public static void KilledPlayer()
    {
        killedEnemies++;
    }

    public static void Time(float time)
    {
        steveSurvivedTime = time;
    }
}
