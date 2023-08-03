using ReMod.Core.VRChat;
using System;
using System.Collections;
using MelonLoader;
using TMPro;
using UnhollowerRuntimeLib;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using VRC.UI;
using VRC.UI.Core.Styles;
using VRC.UI.Elements.Controls;
using Object = UnityEngine.Object;

namespace ReMod.Core.UI.QuickMenu
{
    public class ReMenuToggle
    {
        public readonly ReMenuButton _toggleComponent;

        public bool Interactable
        {
            get => _toggleComponent.Interactable;
            set
            {
                _toggleComponent.Interactable = value;
            }
        }

        private bool _valueHolder;

        private readonly StyleElement _toggleStyleElement;

        private readonly TextMeshProUGUI _textComponent;
        public string Text
        {
            get => _textComponent.text;
            set => _textComponent.text = value;
        }
        
        private readonly VRC.UI.Elements.Tooltips.UiToggleTooltip _tooltip;
        public string Tooltip;
        
        public void SetToolTIp(string tooltip)
        {
            if (_tooltip == null) return;
            Tooltip = tooltip;
            
            _tooltip.text = tooltip;
            _tooltip.alternateText = tooltip;
            _tooltip.Method_Public_UiTooltip_String_0(tooltip);
        }
        
        public ImageEx Background { get; }
        private readonly Sprite _toggleOnImage;
        private readonly Sprite _toggleOffImage;
        
        public ReMenuToggle(string text, string tooltip, Action<bool> onToggle, Transform parent, bool defaultValue = false) : this(text, tooltip, onToggle, parent, defaultValue, null, null) { }
        public ReMenuToggle(string text, string tooltip, Action<bool> onToggle, Transform parent, bool defaultValue = false, Sprite iconOn = null, Sprite iconOff = null, string color = "#ffffff")
        {
            _toggleOnImage = iconOn ?? MenuEx.OnIconSprite;
            _toggleOffImage = iconOff ?? MenuEx.OffIconSprite;

            _toggleComponent = new ReMenuButton(text, tooltip, () =>
            {
                _valueHolder = !_valueHolder;
                
                ChangeSpriteState(_valueHolder);
                if (onToggle != null)
                    onToggle.Invoke(_valueHolder);
            }, parent, _toggleOffImage, color: color);
            
            _valueHolder = defaultValue;
            ChangeSpriteState(defaultValue);
        }
        
        private void ChangeSpriteState(bool state)
        {
            var iconImage = _toggleComponent.RectTransform.Find("Icon").GetComponent<Image>();
            iconImage.sprite = state ? _toggleOnImage : _toggleOffImage;
            iconImage.overrideSprite = state ? _toggleOnImage : _toggleOffImage;
        }
    }
}
