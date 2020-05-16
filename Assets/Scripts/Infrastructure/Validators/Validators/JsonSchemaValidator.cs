using System;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Zenject;

public class JsonSchemaValidator : ISchemaValidator
{
    private readonly ISchemaBuilder _schemaBuilder;

    [Inject]
    public JsonSchemaValidator(ISchemaBuilder schemaBuilder)
    {
        _schemaBuilder = schemaBuilder;
    }

    public bool ValidateAsSchemaType<T>(string sourceObjectInfo)
    {
        var schema = _schemaBuilder.Build(typeof(T));

        try
        {
            var targetJObject = JObject.Parse(sourceObjectInfo);
            return targetJObject.IsValid(schema);
        }

        catch (Newtonsoft.Json.JsonReaderException)
        {
            throw new ArgumentException($"Error trying to validate schema for type: {typeof(T)}, against object: {sourceObjectInfo}");
        }
    }
}