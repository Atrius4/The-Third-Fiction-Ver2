using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataExtras
{
    public static Vector2 StringToVector2(string vector)
    {
        vector = vector.Substring(1, vector.Length - 2);
        string[] tmp = vector.Split(',');

        Vector2 result = new Vector2(
            float.Parse(tmp[0]) / 10,
            float.Parse(tmp[1]) / 10);

        return result;
    }
}
