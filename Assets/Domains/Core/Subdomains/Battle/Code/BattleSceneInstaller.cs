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
        [SerializeField] private InputConfiguration _inputConfiguration;
        
        [Header("Input")]
        [SerializeField] private InputHandler _workaholicInputHandler;
        [SerializeField] private InputHandler _bumInputHandler;

        [Header("Views")]
        [SerializeField] private FighterView _workaholicView;
        [SerializeField] private FighterView _bumView;
        [SerializeField] private CountdownView _countdownView;
        

        [Header("Handlers")]
        [SerializeField] private BattleHandler _battleHandler;
        


        public override void InstallBindings()
        {
            Container.Bind<FightConfiguration>().FromInstance(_fightConfiguration).AsSingle();
            Container.Bind<InputConfiguration>().FromInstance(_inputConfiguration).AsSingle();

            Container.Bind<ICountdownView>().To<CountdownView>().FromInstance(_countdownView);
            
            Container.Bind<IInputHandler>().To<InputHandler>()
                .FromInstance(_workaholicInputHandler)
                .WithConcreteId(PlayerType.Workaholic);

            Container.Bind<IInputHandler>().To<InputHandler>()
                .FromInstance(_bumInputHandler)
                .WithConcreteId(PlayerType.Bum);

            var workaholic = new Fighter(PlayerType.Workaholic, _fightConfiguration, _workaholicInputHandler, _workaholicView);
            Container.Bind<IFighter>()
                .WithId(PlayerType.Workaholic)
                .To<Fighter>()
                .FromInstance(workaholic);

            var bum = new Fighter(PlayerType.Bum, _fightConfiguration, _bumInputHandler, _bumView);
            Container.Bind<IFighter>()
                .WithId(PlayerType.Bum)
                .To<Fighter>()
                .FromInstance(bum);

            Container.Bind<IGameState>().To<GameState>().AsTransient();

            Container.Bind<IBattleHandler>().To<BattleHandler>().FromInstance(_battleHandler).AsTransient();
        }
    }
}