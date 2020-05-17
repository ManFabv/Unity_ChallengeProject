using System;
using Newtonsoft.Json.Schema;

namespace PPop.Infrastructure.Validators.SchemaBuilder
{
    public interface ISchemaBuilder
    {
        JSchema Build(Type schemaAsType);
    }
}