using Zenject;

public class DomainInstaller : Installer<DomainInstaller>
{
    public override void InstallBindings()
    {
        Container.Bind<ISchemaBuilder>().To<JsonSchemaBuilder>().AsSingle();
        Container.Bind<ISchemaValidator>().To<JsonSchemaValidator>();
        Container.Bind<IReader>().To<UnityResourcesReader>().AsSingle();
        Container.Bind<ILoaderService>().To<JSonLoaderService>().AsSingle();
    }
}