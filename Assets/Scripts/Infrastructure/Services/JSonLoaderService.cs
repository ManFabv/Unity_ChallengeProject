using UnityEngine;
using Zenject;

public class JSonLoaderService : ILoaderService 
{
    private readonly IReader _reader;

    [Inject]
    public JSonLoaderService(IReader reader)
    {
        _reader = reader;
    }

    public T Read<T>(string fileName)
    {
        var json = _reader.Read(fileName);
        var result = JsonUtility.FromJson<T>(json);
        return result;
    }
}