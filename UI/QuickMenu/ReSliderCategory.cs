using ReMod.Core.VRChat;
using System;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace ReMod.Core.UI.QuickMenu
{
    public class ReMenuSliderContainer : UiElement
    {
        private static GameObject _ContainerPrefab;
        internal static GameObject ContainerPrefab
        {                       
            get
            {
                if (_ContainerPrefab == null)
                {
                    _ContainerPrefab = MenuEx.QMInstance.transform
                        .Find("CanvasGroup/Container/Window/QMParent/Menu_AudioSettings/Panel_QM_ScrollRect/Viewport/VerticalLayoutGroup").gameObject;
                }
                return _ContainerPrefab;
            }
        }
        
        public ReMenuSliderContainer(string name, Transform parent = null) : base(ContainerPrefab, parent == null ? ContainerPrefab.transform.parent : parent, $"Sliders_{name}")
        {
            foreach (var obj in RectTransform)
            {
                var control = obj.Cast<Transform>();
                if (control == null)
                {
                    continue;
                }
                Object.Destroy(control.gameObject);
            }
            
            var vlg = GameObject.GetComponent<VerticalLayoutGroup>();
            vlg.m_Padding = new RectOffset(64, 64, 0, 0);
        }

        public ReMenuSliderContainer(Transform transform) : base(transform)
        {
        }
    }

    public class ReMenuSliderCategory
    {
        public readonly ReMenuHeader Header;
        private readonly ReMenuSliderContainer _sliderContainer;

        public string Title
        {
            get => Header.Title;
            set => Header.Title = value;
        }

        public bool Active
        {
            get => _sliderContainer.Active;
            set
            {
                Header.Active = value;
                _sliderContainer.Active = value;
            }
        }

        public ReMenuSliderCategory(string title, Transform parent = null, bool collapsible = true, string color = "#ffffff")
        {
            if (collapsible)
            {
                var header = new ReMenuHeaderCollapsible("<color=" + color + ">" + title + "</color>", parent);
                header.OnToggle += b => _sliderContainer!.GameObject.SetActive(b);
                Header = header;
                
            }
            else
            {
                var header = new ReMenuHeader("<color=" + color + ">" + title + "</color>", parent);
                Header = header;
            }
            
            _sliderContainer = new ReMenuSliderContainer("<color=" + color + ">" + title + "</color>", parent);
        }

        public ReMenuSliderCategory(ReMenuHeader headerElement, ReMenuSliderContainer container)
        {
            Header = headerElement;
            _sliderContainer = container;
        }

        public ReMenuSlider AddSlider(string text, string tooltip, Action<float> onSlide, bool reset = false, float defaultValue = 0, float minValue = 0, float maxValue = 10, string color = "#ffffff")
        {
            var slider = new ReMenuSlider(text, tooltip, onSlide, _sliderContainer.RectTransform, reset, defaultValue, minValue, maxValue, color);
            return slider;
        }

        public ReMenuSlider AddSlider(string text, string tooltip, ConfigValue<float> configValue, bool reset = false, float defaultValue = 0, float minValue = 0, float maxValue = 10, string color = "#ffffff")
        {
            var slider = new ReMenuSlider(text, tooltip, configValue.SetValue, _sliderContainer.RectTransform, reset, configValue, minValue, maxValue, color);
            return slider;
        }
       
    }
}
