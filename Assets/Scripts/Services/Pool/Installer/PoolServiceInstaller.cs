using Services.Pool.Abstractions;
using Services.Pool.Runtime;
using Zenject;

namespace Services.Pool.Installer
{
    public class PoolServiceInstaller : MonoInstaller<PoolServiceInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<IPoolService>().To<PoolService>().AsSingle();
        }
    }
}
