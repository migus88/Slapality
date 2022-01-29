using System;
using Domains.Core.Subdomains.Battle.Code.Enums;
using UnityEngine;
using UnityEngine.UI;

namespace Domains.Core.Subdomains.Battle.Code.Configuration
{
    [CreateAssetMenu(fileName = "ButtonsConfiguration", menuName = "Atomic/Buttons Configuration", order = 0)]
    public class ButtonsConfiguration : ScriptableObject
    {
        public Sprite X;
        public Sprite Y;
        public Sprite A;
        public Sprite B;

        public Sprite GetSprite(GameButtonType buttonType)
        {
            return buttonType switch
            {
                GameButtonType.X => X,
                GameButtonType.Y => Y,
                GameButtonType.A => A,
                GameButtonType.B => B,
                _ => throw new ArgumentOutOfRangeException(nameof(buttonType), buttonType, null)
            };
        }
    }
}