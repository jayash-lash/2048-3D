using Services.Pool.Abstractions;
using Services.Pool.Runtime;
using Zenject;

namespace Installers
{
    public class ProjectInstaller : MonoInstaller<ProjectInstaller>
    {
        public override void InstallBindings()
        {
            BindPoolService();
        }

        private void BindPoolService()
        {
            Container.Bind<IPoolService>().To<PoolService>().AsSingle();
        }
    }
}