using System.Collections.Generic;

public static class Extensions 
{
    public static List<T> ToList<T>(this T[] array) 
    {
        List<T> list=new List<T>();
        list.AddRange(array);
        return list;
    }

    public static T GetRandom<T>(this T[] array)
    {
        int random = UnityEngine.Random.Range(0, array.Length);
        UnityEngine.Debug.Log(random);
        return array[random];
        
    }

    public static T GetRandom<T>(this List<T> list)
    {
        int random = UnityEngine.Random.Range(0, list.Count);
        UnityEngine.Debug.Log(random);
        return list[random];
    }
}
