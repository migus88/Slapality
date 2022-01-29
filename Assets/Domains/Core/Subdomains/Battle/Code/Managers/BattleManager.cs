using Domains.Core.Subdomains.Battle.Code.Interfaces;
using UnityEngine;
using Zenject;

namespace Domains.Core.Subdomains.Battle.Code.Managers
{
    public class BattleManager : MonoBehaviour
    {
        [Inject] private IBattleHandler _battleHandler;
        
        
    }
}