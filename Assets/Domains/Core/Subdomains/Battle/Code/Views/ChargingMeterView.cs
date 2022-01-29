using System;
using Domains.Core.Subdomains.Battle.Code.Configuration;
using Domains.Core.Subdomains.Battle.Code.Enums;
using Domains.Core.Subdomains.Battle.Code.Interfaces;
using UnityEngine;
using Zenject;

namespace Domains.Core.Subdomains.Battle.Code.Views
{
    public class ChargingMeterView : MonoBehaviour
    {
        [SerializeField] private float _minVerticalPosition;
        [SerializeField] private float _maxVerticalPosition;
        [SerializeField] private RectTransform _backgroundTransform;
        
        [Range(0,1f)]
        [SerializeField] private float _value;

        private void Update()
        {
            // TODO: Get real value
            //_value = charginMeter;
            
            var newVerticalPosition = Mathf.Lerp(_minVerticalPosition, _maxVerticalPosition, _value);
            _backgroundTransform.anchoredPosition =
                new Vector2(_backgroundTransform.anchoredPosition.x, newVerticalPosition);
        }
    }
}