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
        try
        {
            var json = _reader.Read(fileName);
            _schemaValidator.ValidateAsSchemaType<T>(json);
            var result = JsonConvert.DeserializeObject<T>(json);
            return result;
        }
        catch (JsonException)
        {
            throw new ArgumentException($"Error while trying to read file {fileName}");
        }
    }
}