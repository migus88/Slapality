using Cysharp.Threading.Tasks;
using Domains.Core.Subdomains.Battle.Code.Enums;

namespace Domains.Core.Subdomains.Battle.Code.Interfaces
{
    public interface IBattleHandler
    {
        UniTask<(float charge, GameButtonType buttonPressed)> ChargeHit();
        UniTask<GameButtonType> SelectDodgeButton();
        UniTask<(float damage, DamageResult result)> CalculateDamage(float charge, GameButtonType hitButton, GameButtonType dodgeButton);
        UniTask<RoundResult> ApplyDamage(float damage, DamageResult damageResult);
        UniTask SwitchPlayers();
        UniTask GameOver();
    }
}