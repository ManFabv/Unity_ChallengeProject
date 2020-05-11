using Zenject;

public class DomainMonoInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<ISchemaBuilder>().To<JsonSchemaBuilder>().AsSingle();
        Container.Bind<ISchemaValidator>().To<JsonSchemaValidator>().AsSingle();
        Container.Bind<IReader>().To<UnityResourcesReader>().AsSingle();
        Container.Bind<ILoaderService>().To<JSonLoaderService>().AsSingle();
        Container.Bind<ILevel>().To<GridLevel>().AsSingle();
    }
}