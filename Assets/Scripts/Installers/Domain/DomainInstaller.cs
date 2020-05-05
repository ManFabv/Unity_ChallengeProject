using Zenject;

public class DomainInstaller : Installer<DomainInstaller>
{
    public override void InstallBindings()
    {
        Container.Bind<ILoaderService>().To<JSonLoaderService>().AsSingle();
    }
}