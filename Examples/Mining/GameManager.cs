using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameManager
{
    public static Vector3 storeLocation = new Vector3(-7.14f, 1, -0.57f);
    public static Vector3 woodsLocation = new Vector3(-2.47f, 1, 5.38f);
    public static Vector3 rocksLocation = new Vector3(7f, 1, 2.28f);

    public static int stickValue = 1;
    public static int logValue = 100;
    public static int oreValue = 1000;
    public static int axeValue = 10;
    public static int pickaxeValue = 1000;

    public static float distanceTolerance = 0.1f;
}
