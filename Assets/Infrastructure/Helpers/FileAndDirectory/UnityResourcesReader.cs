using UnityEngine;

public class UnityResourcesReader : IReader
{
    public string Read(string fileName)
    {
        var targetFile = Resources.Load<TextAsset>(fileName);
        return targetFile.text;
    }
}