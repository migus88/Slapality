using Cysharp.Threading.Tasks;
using Domains.Core.Subdomains.Battle.Code.Enums;

namespace Domains.Core.Subdomains.Battle.Code.Interfaces
{
    public interface ICountdownView
    {
        CountdownState State { get; }
        
        UniTask Countdown(float seconds);
        UniTask ShowButton(float durationSeconds, GameButtonType buttonType, PlayerType playerType);
    }
}