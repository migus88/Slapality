using System;
using Domains.Core.Subdomains.Battle.Code.Configuration;
using Domains.Core.Subdomains.Battle.Code.Enums;
using Domains.Core.Subdomains.Battle.Code.Interfaces;
using UnityEngine;
using Zenject;

namespace Domains.Core.Subdomains.Battle.Code.Views
{
    public class HealthBarView : MonoBehaviour
    {
        [SerializeField] private PlayerType _playerType;
        [SerializeField] private float _minHorizontalPosition;
        [SerializeField] private float _maxHorizontalPosition;
        [SerializeField] private RectTransform _backgroundTransform;
        
        
        [Range(0,1f)]
        [SerializeField] private float _value;
        
        [Inject] private DiContainer _container;
        [Inject] private FightConfiguration _configuration;

        private IFighter _fighter;
        private void Awake()
        {
            _fighter = _container.ResolveId<IFighter>(_playerType);

            if (_fighter == null)
            {
                throw new Exception("Fighter not found");
            }
        }

        private void Update()
        {
            UpdateView();
        }

        private void UpdateView()
        {
            var maxHp = _configuration.MaxHP;
            var currentHp = _fighter.CurrentHP;

            _value = currentHp / maxHp;
            var newHorizontalPosition = Mathf.Lerp(_minHorizontalPosition, _maxHorizontalPosition, _value);
            _backgroundTransform.anchoredPosition =
                new Vector2(newHorizontalPosition, _backgroundTransform.anchoredPosition.y);
        }
    }
}