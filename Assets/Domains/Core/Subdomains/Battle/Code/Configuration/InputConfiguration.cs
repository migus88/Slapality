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
        [Header("X")]
        public KeyCode X;
        public Sprite SpriteX;
        
        [Header("Y")]
        public KeyCode Y;
        public Sprite SpriteY;
        
        [Header("A")]
        public KeyCode A;
        public Sprite SpriteA;
        
        [Header("B")]
        public KeyCode B;
        public Sprite SpriteB;
        
        [Header("Charge")]
        public KeyCode Charge;

        public Sprite GetSprite(GameButtonType buttonType)
        {
            return buttonType switch
            {
                GameButtonType.X => SpriteX,
                GameButtonType.Y => SpriteY,
                GameButtonType.A => SpriteA,
                GameButtonType.B => SpriteB,
                _ => throw new IndexOutOfRangeException()
            };
        }
    }
}