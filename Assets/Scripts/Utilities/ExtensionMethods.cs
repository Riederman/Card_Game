using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods
{
    public static List<T> Clone<T>(this List<T> toClone) where T : System.ICloneable
    {
        // Returns a list of cloned objects
        List<T> list = new List<T>();
        foreach (T item in toClone)
            list.Add((T)item.Clone());
        return list;
    }

    public static void ToggleActive(this GameObject gameObj)
    {
        gameObj.SetActive(!gameObj.activeSelf);
    }
}