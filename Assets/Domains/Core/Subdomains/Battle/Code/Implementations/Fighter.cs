using Domains.Core.Subdomains.Battle.Code.Configuration;
using Domains.Core.Subdomains.Battle.Code.Enums;
using Domains.Core.Subdomains.Battle.Code.Interfaces;

namespace Domains.Core.Subdomains.Battle.Code.Implementations
{
    public class Fighter : IFighter
    {
        public PlayerType PlayerType { get; }
        public float CurrentHP { get; set; }
        public bool IsDead => CurrentHP <= 0;
        public IInputHandler InputHandler { get; }
        public IFighterView View { get; }

        public Fighter(PlayerType playerType, FightConfiguration configuration, IInputHandler inputHandler, IFighterView view)
        {
            PlayerType = playerType;
            InputHandler = inputHandler;
            View = view;
            CurrentHP = configuration.MaxHP;
        }
    }
}