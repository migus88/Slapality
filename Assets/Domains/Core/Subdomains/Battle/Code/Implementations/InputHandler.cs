using System;
using Domains.Core.Subdomains.Battle.Code.Configuration;
using Domains.Core.Subdomains.Battle.Code.Enums;
using Domains.Core.Subdomains.Battle.Code.Interfaces;
using UnityEngine;
using Zenject;

namespace Domains.Core.Subdomains.Battle.Code.Implementations
{
    //TODO: Port to a normal input system
    public class InputHandler : MonoBehaviour, IInputHandler
    {
        public PlayerInput InputConfig => _config.GetInput(_playerType);
        public bool IsChargeButtonPressed => Input.GetKeyDown(InputConfig.Charge);
        public bool IsGameButtonPressed => IsButtonXPressed || IsButtonYPressed || IsButtonAPressed || IsButtonBPressed;
        public bool IsButtonXPressed => Input.GetKeyDown(InputConfig.X);
        public bool IsButtonYPressed => Input.GetKeyDown(InputConfig.Y);
        public bool IsButtonAPressed => Input.GetKeyDown(InputConfig.A);
        public bool IsButtonBPressed => Input.GetKeyDown(InputConfig.B);
        public GameButtonType LastGameButtonPressed { get; private set; }
        

        [SerializeField] private PlayerType _playerType;
        

        [Inject] private InputConfiguration _config;

        private void Update()
        {
            if (!IsGameButtonPressed)
            {
                return;
            }

            if (IsButtonAPressed)
            {
                LastGameButtonPressed = GameButtonType.A;
            }
            else if (IsButtonBPressed)
            {
                LastGameButtonPressed = GameButtonType.B;
            }
            else if (IsButtonXPressed)
            {
                LastGameButtonPressed = GameButtonType.X;
            }
            else
            {
                LastGameButtonPressed = GameButtonType.Y;
            }
        }
        
        public void ResetLastDirection()
        {
            LastGameButtonPressed = GameButtonType.None;
        }
    }
}