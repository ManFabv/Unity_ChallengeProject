using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Schema;
using Zenject;

public class JsonSchemaBuilder : ISchemaBuilder
{
    private readonly IReader _reader;

    [Inject]
    public JsonSchemaBuilder(IReader reader)
    {
        _reader = reader;
    }

    public JSchema Build(Type schemaAsType)
    {
        var schemaAsText = _reader.ReadSchema(schemaAsType);

        try
        {
            var schema = JSchema.Parse(schemaAsText);
            return schema;
        }

        catch (JsonException)
        {
            throw new ArgumentException($"Schema {schemaAsText} couldn't be validated");
        }
    }
}