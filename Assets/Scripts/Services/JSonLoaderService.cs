using UnityEngine;

public class JSonLoaderService : ILoaderService 
{
    private string LoadJsonFileFromResources(string fileName)
    {
        var targetFile = Resources.Load<TextAsset>(fileName);
        return targetFile.text;
    }

    public T Read<T>(string fileName)
    {
        var json = LoadJsonFileFromResources(fileName);
        var result = JsonUtility.FromJson<T>(json);
        return result;
    }
}