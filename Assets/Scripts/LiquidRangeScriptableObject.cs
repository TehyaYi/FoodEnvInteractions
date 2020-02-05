using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/LiquidRangeScriptableObject", order = 2)]
public class LiquidRangeScriptableObject : RangeScriptableObject
{
    //Only a water tile with an absolute distance of less than the tolerance
    //to targetRGB will be counted towards water need
    [SerializeField] float[] targetRGB = new float[3];
    [SerializeField] float tolerance;


    /*
     * float[][] is for illustration purposes. When project merge, this should
     * be reimplemented with CustomTile[], and read the rgb value if it is a
     * water tile
    */
    public int getValue(float[][] rgbValues) {
        int count = 0;
        for (int i = 0; i < rgbValues.Length; i++) {
            if (Mathf.Sqrt(Mathf.Pow(targetRGB[0]-rgbValues[i][0],2) +
                Mathf.Pow(targetRGB[1] - rgbValues[i][1], 2) +
                Mathf.Pow(targetRGB[2] - rgbValues[i][2], 2)) <= tolerance) {
                count++;
            }
        }
        return count;
    }
}
