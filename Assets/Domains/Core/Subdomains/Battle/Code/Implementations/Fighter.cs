using Domains.Core.Subdomains.Battle.Code.Configuration;
using Domains.Core.Subdomains.Battle.Code.Enums;
using Domains.Core.Subdomains.Battle.Code.Interfaces;

namespace Domains.Core.Subdomains.Battle.Code.Implementations
{
    public class Fighter : IFighter
    {
        public PlayerType PlayerType { get; private set; }
        public float CurrentHP { get; private set; }
        public bool IsDead => CurrentHP <= 0;

        public Fighter(PlayerType playerType, FightConfiguration configuration)
        {
            PlayerType = playerType;
            CurrentHP = configuration.MaxHP;
        }
    }
}