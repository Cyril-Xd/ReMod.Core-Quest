﻿using ReMod.Core.VRChat;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace ReMod.Core.UI.QuickMenu
{
    public class ReMenuHeader : UiElement
    {
        private static GameObject _headerPrefab;
        private static GameObject HeaderPrefab
        {
            get
            {
                if (_headerPrefab == null)
                {
                    _headerPrefab = MenuEx.QMInstance.transform
                        .Find("CanvasGroup/Container/Window/QMParent/Menu_Dashboard/ScrollRect").GetComponent<ScrollRect>().content
                        .Find("Header_QuickActions").gameObject;
                }
                return _headerPrefab;
            }
        }

        protected TextMeshProUGUI TextComponent;
        public string Title
        {
            get => TextComponent.text;
            set => TextComponent.text = value;
        }

        public ReMenuHeader(string title, Transform parent) : base(HeaderPrefab, (parent == null ? HeaderPrefab.transform.parent : parent), $"Header_{title}")
        {
            TextComponent = GameObject.GetComponentInChildren<TextMeshProUGUI>();
            TextComponent.text = title;
            TextComponent.richText = true;

            TextComponent.transform.parent.GetComponent<HorizontalLayoutGroup>().childControlWidth = true;
        }

        public ReMenuHeader(Transform transform) : base(transform)
        {
            TextComponent = GameObject.GetComponentInChildren<TextMeshProUGUI>();
        }

        protected ReMenuHeader(GameObject original, Transform parent, Vector3 pos, string name, bool defaultState = true) : base(original, parent, pos, name, defaultState) { }
        protected ReMenuHeader(GameObject original, Transform parent, string name, bool defaultState = true) : base(original, parent, name, defaultState) { }
    }

    public class ReMenuHeaderCollapsible : ReMenuHeader
    {
        private static GameObject _headerPrefab;
        private static GameObject HeaderPrefab
        {
            get
            {
                if (_headerPrefab == null)
                {
                    _headerPrefab = MenuEx.QMInstance.transform
                        .Find("CanvasGroup/Container/Window/QMParent/Menu_QM_GeneralSettings/Panel_QM_ScrollRect").GetComponent<ScrollRect>().content
                        .Find("UIElements/QM_Foldout").gameObject;
                }
                return _headerPrefab;
            }
        }

        public Action<bool> OnToggle;

        public ReMenuHeaderCollapsible(string title, Transform parent) : base(HeaderPrefab, (parent == null ? HeaderPrefab.transform.parent : parent), $"Header_{title}")
        {
            TextComponent = GameObject.GetComponentInChildren<TextMeshProUGUI>();
            TextComponent.text = title;
            TextComponent.richText = true;

            var foldout = GameObject.GetComponent<FoldoutToggle>();
            foldout.Method_Public_Void_String_Boolean_0($"UI.ReMod.{GetCleanName(title)}");
            foldout.Method_Public_Void_UnityAction_1_Boolean_0(new Action<bool>(b => OnToggle?.Invoke(b)));
        }

        public ReMenuHeaderCollapsible(Transform transform) : base(transform)
        {
            TextComponent = GameObject.GetComponentInChildren<TextMeshProUGUI>();
        }
    }

    public class ReMenuButtonContainer : UiElement
    {
        private static GameObject _containerPrefab;
        private static GameObject ContainerPrefab
        {
            get
            {
                if (_containerPrefab == null)
                {
                    _containerPrefab = MenuEx.QMInstance.transform
                        .Find("CanvasGroup/Container/Window/QMParent/Menu_Dashboard/ScrollRect").GetComponent<ScrollRect>().content
                        .Find("Buttons_QuickActions").gameObject;
                }
                return _containerPrefab;
            }
        }

        public ReMenuButtonContainer(string name, Transform parent = null) : base(ContainerPrefab, parent == null ? ContainerPrefab.transform.parent : parent, $"Buttons_{name}")
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

            var gridLayout = GameObject.GetComponent<GridLayoutGroup>();

            gridLayout.childAlignment = TextAnchor.UpperLeft;
            gridLayout.padding.top = 8;
            gridLayout.padding.left = 64;
        }

        public ReMenuButtonContainer(Transform transform) : base(transform)
        {
        }
    }

    public class ReMenuCategory : IButtonPage
    {
        public readonly ReMenuHeader Header;
        private readonly ReMenuButtonContainer _buttonContainer;

        public string Title
        {
            get => Header.Title;
            set => Header.Title = value;
        }

        public bool Active
        {
            get => _buttonContainer.GameObject.activeInHierarchy;
            set
            {
                Header.Active = value;
                _buttonContainer.Active = value;
            }
        }

        public ReMenuCategory(string title, Transform parent = null, bool collapsible = true, string color = "#ffffff")
        {
            if (collapsible)
            {
                var header = new ReMenuHeaderCollapsible("<color=" + color + ">" + title + "</color>", parent);
                header.OnToggle += b => _buttonContainer.GameObject.SetActive(b);
                Header = header;
            }

            else
            {
                var header = new ReMenuHeader("<color=" + color + ">" + title + "</color>", parent);
                Header = header;
            }
            _buttonContainer = new ReMenuButtonContainer("<color=" + color + ">" + title + "</color>", parent);
        }

        public ReMenuCategory(ReMenuHeader headerElement, ReMenuButtonContainer container)
        {
            Header = headerElement;
            _buttonContainer = container;
        }

        public ReMenuButton AddButton(string text, string tooltip, Action onClick, Sprite sprite = null, string color = "#ffffff")
        {
            var button = new ReMenuButton(text, tooltip, onClick, _buttonContainer.RectTransform, sprite, color:color);
            return button;
        }
        
        public ReMenuButton AddSpacer(Sprite sprite = null) {
            var spacer = AddButton(string.Empty, string.Empty, null, sprite);
            spacer.GameObject.name = "Button_Spacer";
            spacer.Background.gameObject.SetActive(false);
            return spacer;
        }
        
        public ReMenuToggle AddToggle(string text, string tooltip, Action<bool> onToggle, string color = "#ffffff") 
            => AddToggle(text, tooltip, onToggle, false, null, null, color);
        public ReMenuToggle AddToggle(string text, string tooltip, Action<bool> onToggle, bool defaultValue = false, string color = "#ffffff") 
            => AddToggle(text, tooltip, onToggle, defaultValue, null, null, color);
        public ReMenuToggle AddToggle(string text, string tooltip, Action<bool> onToggle, bool defaultValue = false) 
            => AddToggle(text, tooltip, onToggle, defaultValue, null, null);
        public ReMenuToggle AddToggle(string text, string tooltip, Action<bool> onToggle) 
            => AddToggle(text, tooltip, onToggle, false, null, null);
        public ReMenuToggle AddToggle(string text, string tooltip, ConfigValue<bool> configValue, string color = "#ffffff")
            => AddToggle(text, tooltip, configValue, null, null, color);
        public ReMenuToggle AddToggle(string text, string tooltip, ConfigValue<bool> configValue)
            => AddToggle(text, tooltip, configValue, null, null);
        public ReMenuToggle AddToggle(string text, string tooltip, Action<bool> onToggle, bool defaultValue, Sprite iconOn, Sprite iconOff, string color = "#ffffff")
        {
            var toggle = new ReMenuToggle(text, tooltip, onToggle, _buttonContainer.RectTransform, defaultValue, iconOn, iconOff, color);
            return toggle;
        }
        public ReMenuToggle AddToggle(string text, string tooltip, Action<bool> onToggle, bool defaultValue, Sprite iconOn, Sprite iconOff)
        {
            var toggle = new ReMenuToggle(text, tooltip, onToggle, _buttonContainer.RectTransform, defaultValue, iconOn, iconOff);
            return toggle;
        }
        public ReMenuToggle AddToggle(string text, string tooltip, ConfigValue<bool> configValue, Sprite iconOn, Sprite iconOff, string color = "#ffffff")
        {
            var toggle = new ReMenuToggle(text, tooltip, configValue.SetValue, _buttonContainer.RectTransform, configValue, iconOn, iconOff, color);
            return toggle;
        }
        public ReMenuToggle AddToggle(string text, string tooltip, ConfigValue<bool> configValue, Sprite iconOn, Sprite iconOff)
        {
            var toggle = new ReMenuToggle(text, tooltip, configValue.SetValue, _buttonContainer.RectTransform, configValue, iconOn, iconOff);
            return toggle;
        }

        public ReMenuPage AddMenuPage(string text, string tooltip = "", Sprite sprite = null, string color = "#ffffff")
        {
            var existingPage = GetMenuPage(text);
            if (existingPage != null)
            {
                return existingPage;
            }

            var menu = new ReMenuPage(text, color: color);
            AddButton(text, string.IsNullOrEmpty(tooltip) ? $"Open the {text} menu" : tooltip, menu.Open, sprite, color);
            return menu;
        }
        public ReMenuPage ToMenuPage(string name, string tooltip = "", Sprite sprite = null)
        {
            var menu = GetMenuPage(name);
            AddButton(name, string.IsNullOrEmpty(tooltip) ? $"Open the {name} menu" : tooltip, menu.Open, sprite);
            return menu;
        }
        public ReCategoryPage AddCategoryPage(string text, string tooltip = "", Sprite sprite = null, string color = "#ffffff")
        {
            var existingPage = GetCategoryPage(text);
            if (existingPage != null)
            {
                return existingPage;
            }

            var menu = new ReCategoryPage(text, color:color);
            AddButton(text, string.IsNullOrEmpty(tooltip) ? $"Open the {text} menu" : tooltip, menu.Open, sprite, color);
            return menu;
        }

        public void AddMenuPage(string text, string tooltip, Action<ReMenuPage> onPageBuilt, Sprite sprite = null, string color = "#ffffff")
        {
            onPageBuilt(AddMenuPage(text, tooltip, sprite, color));
        }

        public void AddCategoryPage(string text, string tooltip, Action<ReCategoryPage> onPageBuilt, Sprite sprite = null, string color = "#ffffff")
        {
            onPageBuilt(AddCategoryPage(text, tooltip, sprite, color));
        }

        public RectTransform RectTransform => _buttonContainer.RectTransform;

        public ReMenuPage GetMenuPage(string name)
        {
            var transform = MenuEx.QMenuParent.Find(UiElement.GetCleanName($"Menu_{name}"));
            return transform == null ? null : new ReMenuPage(transform);
        }

        public ReCategoryPage GetCategoryPage(string name)
        {
            var transform = MenuEx.QMenuParent.Find(UiElement.GetCleanName($"Menu_{name}"));
            return transform == null ? null : new ReCategoryPage(transform);
        }

    }
}
