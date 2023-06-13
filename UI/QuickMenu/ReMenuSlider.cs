using ReMod.Core.Unity;
using ReMod.Core.VRChat;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace ReMod.Core.UI.QuickMenu
{
    public class ReMenuSlider : UiElement
    {
        private readonly Slider _sliderComponent;
        
        private VRC.UI.Elements.Tooltips.UiTooltip _tooltip;
        
        public string Tooltip {
            get => _tooltip != null ? _tooltip.text : "";
            set
            {
                if (_tooltip == null) return;
                _tooltip.text = value;
            }
        }

        public ReMenuSlider(string text, string tooltip, Action<float> onSlide, Transform parent, bool reset = false, float defaultValue = 0, float minValue = 0, float maxValue = 10, string color = "#ffffff") : base(MenuEx.SliderPrefab, parent, $"Slider_{text}")
        {
            Object.DestroyImmediate(GameObject.GetComponent<UIInvisibleGraphic>()); // Fix for having clickable area overlap main quickmenu ui

            var name = RectTransform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>();
            name.text = "<color=" + color + ">" + text + "</color>";
            name.richText = true;

            var value = RectTransform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>();
            value.text = defaultValue.ToString("F");  //This shit don't work
            
            _sliderComponent = GameObject.GetComponentInChildren<Slider>();
            _sliderComponent.onValueChanged = new Slider.SliderEvent();
            _sliderComponent.onValueChanged.AddListener(new Action<float>(onSlide));
            _sliderComponent.onValueChanged.AddListener(new Action<float>(val =>
            {
                value.text = val.ToString("F");
            }));
            _sliderComponent.m_OnValueChanged = _sliderComponent.onValueChanged;

            _sliderComponent.minValue = minValue;
            _sliderComponent.maxValue = maxValue;
            _sliderComponent.value = defaultValue;

            _tooltip = GameObject.GetComponent<VRC.UI.Elements.Tooltips.UiTooltip>();
            
            _tooltip.text = tooltip;
            _tooltip.alternateText = tooltip;
            
            //_tooltip.Method_Public_UiTooltip_String_0(tooltip);
            //_tooltip.Method_Public_UiTooltip_String_1(tooltip);
            _tooltip.Method_Public_UiTooltip_String_2(tooltip);
            _tooltip.Method_Public_UiTooltip_String_3(tooltip);
            //_tooltip.Method_Public_UiTooltip_String_4(tooltip);
            //_tooltip.Method_Public_UiTooltip_String_PDM_2(tooltip);

            Slide(defaultValue,false);
            
            EnableDisableListener.RegisterSafe();
            var edl = GameObject.AddComponent<EnableDisableListener>();
            _sliderComponent.transform.parent.gameObject.transform.Find("Button_MM_Mute").gameObject.SetActive(false);
        }

        public void Slide(float value, bool callback = true)
        {
            _sliderComponent.Set(value, callback);
        }
        
        public void SetNewMaxValue(float value)
        {
            _sliderComponent.maxValue = value;
        }
        
        public void SetNewMinValue(float value)
        {
            _sliderComponent.minValue = value;
        }
        
        public float CurrentValue()
        {
            return _sliderComponent.value;
        }
    }
}
