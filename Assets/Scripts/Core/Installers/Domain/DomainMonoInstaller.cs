using PPop.Domain.Levels;
using PPop.Domain.Tiles;
using PPop.Game.GridLevels;
using PPop.Game.LevelManagers;
using PPop.Game.LevelManagers.TilemapStatus;
using PPop.Infrastructure.Helpers.FileAndDirectory;
using PPop.Infrastructure.Services.Loader;
using PPop.Infrastructure.Validators.SchemaBuilder;
using PPop.Infrastructure.Validators.Validators;
using PPops.Domain.Statics.LevelStatics;
using Zenject;

namespace PPop.Domain.Installers
{
    public class DomainMonoInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<TileNode>().AsTransient();
            Container.Bind<ITileMapStatus<TileNode>>().To<TileMap_Status_Idle>().AsSingle();
            Container.Bind<IGameStaticsLevelValues>().To<GameStaticsLevelValues>().AsSingle();
            Container.Bind<ISchemaBuilder>().To<JsonSchemaBuilder>().AsSingle();
            Container.Bind<ISchemaValidator>().To<JsonSchemaValidator>().AsSingle();
            Container.Bind<IReader>().To<UnityResourcesReader>().AsSingle();
            Container.Bind<ILoaderService>().To<JSonLoaderService>().AsSingle();
            Container.Bind<ILevel>().To<GridLevel>().AsSingle();
            Container.Bind<IGridLevelFactory>().To<GridLevelFactory>().AsSingle();
            Container.Bind<ILevelStateManager<TileNode>>().To<LevelStateManager>().AsSingle();
        }
    }
}