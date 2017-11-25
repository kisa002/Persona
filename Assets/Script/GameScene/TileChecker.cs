using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TileChecker
{
    public static int totalCount = 1;
    public static int colorTileCount
    {
        get
        {
            return totalCount - tileCount[0];
        }
    }
    public static double colorTilePercent
    {
        get
        {
            return (double)colorTileCount / totalCount;
        }
    }
    public static int[] tileCount = new int[5];
    public static double[] tilePercent = new double[5];

    public static void ChangeOneTile(TitleInfo.TileType beforeType, TitleInfo.TileType afterType)
    {
        tileCount[(int)beforeType] -= 1;
        tileCount[(int)afterType] += 1;
    }

    public static void RefreshPercent()
    {
        for(int i=0; i<5; i++)
        {
            tilePercent[i] = (double)tileCount[i] / totalCount;
        }
        PrintInfo();
    }

    public static void PrintInfo()
    {
        //Debug.Log("Info - total: " + totalCount + "\n" +
        //          "       tileCnt: " + "[ " + tileCount[0] + ", " + tileCount[1] + ", " + tileCount[2] + ", " + tileCount[3] + ", " + tileCount[4] + "]");
        //Debug.Log("       tilePercent: " + "[ " + tilePercent[0] + ", " + tilePercent[1] + ", " + tilePercent[2] + ", " + tilePercent[3] + ", " + tilePercent[4] + "]");
    }
}
