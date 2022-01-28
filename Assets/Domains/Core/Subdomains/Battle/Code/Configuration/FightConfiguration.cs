using UnityEngine;

namespace Domains.Core.Subdomains.Battle.Code.Configuration
{
    [UnityEngine.CreateAssetMenu(fileName = "FightConfiguration", menuName = "Atomic/Fight Configuration", order = 0)]
    public class FightConfiguration : ScriptableObject
    {
        public float MaxSecondsToCharge;
        public float MaxSecondsToDodge;
        public float MaxHP;
        public float BasicDamage;
        public float CritMultiplier;
        public float MaxCharge;
        [Range(0, 1f)]
        public float ChargePerTap;
        [Range(0, 1f)]
        public float ChargeDropPerTick;
    }
}