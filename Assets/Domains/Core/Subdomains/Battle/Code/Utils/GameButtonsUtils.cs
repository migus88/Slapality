using System;
using Domains.Core.Subdomains.Battle.Code.Configuration;
using Domains.Core.Subdomains.Battle.Code.Enums;
using UnityEngine;

namespace Domains.Core.Subdomains.Battle.Code.Utils
{
    public static class GameButtonsUtils
    {
        public static KeyCode GetKeyCode(this PlayerInput configuration, GameButtonType buttonType)
        {
            return buttonType switch
            {
                GameButtonType.A => configuration.A,
                GameButtonType.B => configuration.B,
                GameButtonType.X => configuration.X,
                GameButtonType.Y => configuration.Y,
                _ => throw new IndexOutOfRangeException()
            };
        }

        public static GameButtonType GetOppositeButton(this GameButtonType buttonType)
        {
            return buttonType switch
            {
                GameButtonType.A => GameButtonType.B,
                GameButtonType.B => GameButtonType.A,
                GameButtonType.X => GameButtonType.Y,
                GameButtonType.Y => GameButtonType.X,
                GameButtonType.None => GameButtonType.None,
                _ => throw new IndexOutOfRangeException()
            };
        }
    }
}