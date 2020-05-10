using System;
using Newtonsoft.Json.Schema;

public interface ISchemaBuilder
{
    JSchema Build(Type schemaAsType);
}