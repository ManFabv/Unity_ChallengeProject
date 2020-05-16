using System;
using UnityEngine;

public class UnityResourcesReader : IReader
{
    public T Read<T>(string fileName) where T : UnityEngine.Object
    {
        try
        {
            var targetFile = Resources.Load<T>(fileName);
            return targetFile;
        }

        catch (NullReferenceException)
        {
            throw new ArgumentException($"File: {fileName} not found in a Resources folder");
        }
    }

    public string Read(string fileName)
    {
        var result = Read<TextAsset>(fileName);

        if(result is null)
            throw new ArgumentException($"File: {fileName} not found in a Resources folder");

        return result.text;
    }

    public string ReadSchema(Type typeOfSchema)
    {
        var fileName = $"Schemas\\{typeOfSchema}Schema";

        return Read(fileName);
    }
}