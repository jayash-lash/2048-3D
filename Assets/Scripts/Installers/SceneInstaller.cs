using Cube.Launcher;
using Cube.Launcher.Abstraction;
using Services.Board.Abstraction;
using Services.Board.Common;
using Services.Factory.Abstraction;
using Services.Factory.Common;
using Services.Input.Common;
using Services.Merge.Abstraction;
using Services.Merge.Common;
using Services.Score.Abstraction;
using Services.Score.Common;
using Services.VFX.Abstraction;
using Services.VFX.Common;
using StateMachine;
using StateMachine.Abstraction;
using StateMachine.States;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class SceneInstaller : MonoInstaller<SceneInstaller>
    {
        [SerializeField] private Transform _canvasTransform;
        
        public override void InstallBindings()
        {
            BindServices();
            BindFactory();
            BindController();
            BindStateMachine();
            BindStates();
        }
        
        private void BindServices()
        {
            Container.Bind<IScoreService>().To<ScoreService>().AsSingle();
            Container.Bind<IScorePopupService>().To<ScorePopupService>().AsSingle().WithArguments(_canvasTransform);
            Container.Bind<IBoardService>().To<BoardService>().AsSingle();

            Container.BindInterfacesAndSelfTo<MobileInputService>().AsSingle();

            Container.Bind<ICubeMergeService>().To<CubeMergeService>().AsSingle();
            Container.Bind<IVFXService>().To<VFXService>().AsSingle();
        }

        private void BindFactory()
        {
            Container.Bind<ICubeFactory>().To<CubeFactory>().AsSingle();
        }

        private void BindController()
        {
            Container.Bind<ICubeLauncher>().To<CubeLauncher>().AsSingle();
        }

        private void BindStateMachine()
        {
            Container.Bind<IGameStateMachine>().To<GameStateMachine>().AsSingle();
        }

        private void BindStates()
        {
            Container.Bind<MainMenuState>().AsSingle();
            Container.Bind<SpawnState>().AsSingle();
            Container.Bind<IdleState>().AsSingle();
            Container.Bind<AimingState>().AsSingle();
            Container.Bind<LaunchedState>().AsSingle();
            Container.Bind<GameOverState>().AsSingle();
        }
    }
}