namespace PPop.Infrastructure.Validators.Validators
{
    public interface ISchemaValidator
    {
        bool ValidateAsSchemaType<T>(string sourceObjectInfo);
    }
}