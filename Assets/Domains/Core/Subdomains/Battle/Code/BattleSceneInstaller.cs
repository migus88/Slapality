using Domains.Core.Subdomains.Battle.Code.Configuration;
using Domains.Core.Subdomains.Battle.Code.Enums;
using Domains.Core.Subdomains.Battle.Code.Implementations;
using Domains.Core.Subdomains.Battle.Code.Interfaces;
using Domains.Core.Subdomains.Battle.Code.Managers;
using Domains.Core.Subdomains.Battle.Code.Views;
using UnityEngine;
using Zenject;

namespace Domains.Core.Subdomains.Battle.Code
{
    public class BattleSceneInstaller : MonoInstaller
    {
        [Header("Configuration")]
        [SerializeField] private FightConfiguration _fightConfiguration;
        [SerializeField] private InputConfiguration _workaholicInputConfiguration;
        [SerializeField] private InputConfiguration _bumInputConfiguration;
        
        [Header("Input")]
        [SerializeField] private InputHandler _workaholicInputHandler;
        [SerializeField] private InputHandler _bumInputHandler;

        [Header("Views")]
        [SerializeField] private FighterView _workaholicView;
        [SerializeField] private FighterView _bumView;

        [Header("Handlers")]
        [SerializeField] private BattleHandler _battleHandler;
        


        public override void InstallBindings()
        {
            Container.Bind<FightConfiguration>().FromInstance(_fightConfiguration).AsSingle();

            Container.Bind<IInputHandler>().To<InputHandler>()
                .FromInstance(_workaholicInputHandler)
                .WithConcreteId(PlayerType.Workaholic);

            Container.Bind<IInputHandler>().To<InputHandler>()
                .FromInstance(_bumInputHandler)
                .WithConcreteId(PlayerType.Bum);

            var workaholic = new Fighter(PlayerType.Workaholic, _fightConfiguration, _workaholicInputHandler, _workaholicView);
            Container.Bind<IFighter>().To<Fighter>()
                .FromInstance(workaholic)
                .WithConcreteId(PlayerType.Workaholic);

            var bum = new Fighter(PlayerType.Bum, _fightConfiguration, _bumInputHandler, _bumView);
            Container.Bind<IFighter>().To<Fighter>().FromInstance(bum)
                .WithConcreteId(PlayerType.Bum);

            Container.Bind<IGameState>().To<GameState>().AsTransient();

            Container.Bind<IBattleHandler>().To<BattleHandler>().FromInstance(_battleHandler).AsTransient();
        }
    }
}