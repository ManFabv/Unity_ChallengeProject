using System;
using UnityEngine;

public class UnityResourcesReader : IReader
{
    public string Read(string fileName)
    {
        try
        {
            var targetFile = Resources.Load<TextAsset>(fileName);
            return targetFile.text;
        }
        catch (NullReferenceException)
        {
            throw new ArgumentException($"File: {fileName} not found in a Resources folder");
        }
    }

    public string ReadSchema(Type typeOfSchema)
    {
        var fileName = $"Schemas\\{typeOfSchema}Schema";

        return Read(fileName);
    }
}