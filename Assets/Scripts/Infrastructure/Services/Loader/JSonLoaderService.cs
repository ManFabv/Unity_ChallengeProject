using System;
using Newtonsoft.Json;
using Zenject;

public class JSonLoaderService : ILoaderService 
{
    private readonly IReader _reader;
    private readonly ISchemaValidator _schemaValidator;

    [Inject]
    public JSonLoaderService(IReader reader, ISchemaValidator schemaValidator)
    {
        _reader = reader;
        _schemaValidator = schemaValidator;
    }

    public T Read<T>(string fileName)
    {
        var json = _reader.Read(fileName);

        if (!_schemaValidator.ValidateAsSchemaType<T>(json))
            throw new ArgumentException($"File {fileName} doesn't comply with schema for type {typeof(T)}");

        try
        {
            var result = JsonConvert.DeserializeObject<T>(json);
            return result;
        }
        catch (JsonException)
        {
            throw new ArgumentException($"Error while trying to read file {fileName}");
        }
    }
}