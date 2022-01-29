using System;
using Domains.Core.Subdomains.Battle.Code.Enums;
using UnityEngine;

namespace Domains.Core.Subdomains.Battle.Code.Configuration
{
    [CreateAssetMenu(fileName = "InputConfiguration", menuName = "Atomic/Input Configuration", order = 0)]
    public class InputConfiguration : ScriptableObject
    {
        public PlayerInput Player1;
        public PlayerInput Player2;

        public PlayerInput GetInput(PlayerType playerType)
        {
            return playerType == PlayerType.Workaholic ? Player1 : Player2;
        }
    }

    [Serializable]
    public class PlayerInput
    {
        public KeyCode X;
        public KeyCode Y;
        public KeyCode A;
        public KeyCode B;
        public KeyCode Charge;
    }
}