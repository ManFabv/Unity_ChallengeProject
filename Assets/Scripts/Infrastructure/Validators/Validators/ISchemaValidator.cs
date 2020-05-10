public interface ISchemaValidator
{
    bool ValidateAsSchemaType<T>(string sourceObjectInfo);
}