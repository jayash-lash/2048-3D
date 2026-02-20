using ScriptableObjects;
using UnityEngine;
using Zenject;

namespace Installers
{
    [CreateAssetMenu(fileName = "GameSettingsInstaller", menuName = "2048/GameSettingsInstaller")]
    public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
    {
        [SerializeField] private BoardConfig _boardConfig;
        [SerializeField] private CubeConfig _cubeConfig;
        [SerializeField] private VFXConfig _vfxConfig;

        public BoardConfig Config
        {
            get => _boardConfig;
            set => _boardConfig = value;
        }

        public override void InstallBindings()
        {
            Container.BindInstance(Config).AsSingle();
            Container.BindInstance(_cubeConfig).AsSingle();
            Container.BindInstance(_vfxConfig).AsSingle();
        }
    }
}