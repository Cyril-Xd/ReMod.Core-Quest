using ReMod.Core.Unity;
using ReMod.Core.VRChat;
using System;
using System.Linq;
using System.Reflection;
using ReMod.Core.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VRC.UI.Elements;
using VRC.UI.Elements.Menus;
using Object = UnityEngine.Object;

namespace ReMod.Core.UI.QuickMenu
{
    public class ReMenuPage : UiElement, IButtonPage
    {
        private static GameObject _menuPrefab;

        private static GameObject MenuPrefab
        {
            get
            {
                if (_menuPrefab == null)
                {
                    _menuPrefab = MenuEx.QMInstance.transform.Find("CanvasGroup/Container/Window/QMParent/Menu_DevTools").gameObject;
                }
                
                return _menuPrefab;
            }
        }

        private static int SiblingIndex => MenuEx.QMInstance.transform.Find("CanvasGroup/Container/Window/QMParent/Modal_AddMessage").GetSiblingIndex();

        public event Action OnOpen;
        public event Action OnClose;
        private readonly bool _isRoot;

        private readonly Transform _container;
        private readonly Transform menuContents;

        public UIPage UiPage { get; }

        public ReMenuPage(string text, bool isRoot = false, string color = "#ffffff") : base(MenuPrefab, MenuEx.QMenuParent, $"Menu_{text}", false)
        {
            Object.DestroyImmediate(GameObject.GetComponent<DevMenu>());

            RectTransform.SetSiblingIndex(SiblingIndex);

            var menuName = GetCleanName(text);
            _isRoot = isRoot;
            var headerTransform = RectTransform.GetChild(0);
            var titleText = headerTransform.GetComponentInChildren<TextMeshProUGUI>();
            titleText.text = "<color=" + color + ">" + text + "</color>";
            titleText.richText = true;

            if (!_isRoot)
            {
                var backButton = headerTransform.GetComponentInChildren<Button>(true);
                backButton.gameObject.SetActive(true);
            }

            headerTransform.name = $"Header_{menuName}";

            var buttonContainer = RectTransform.Find("Scrollrect/Viewport/VerticalLayoutGroup/Buttons");
            foreach (var obj in buttonContainer)
            {
                var control = obj.Cast<Transform>();
                if (control == null)
                {
                    continue;
                }
                Object.Destroy(control.gameObject);
            }

            // Set up UIPage
            UiPage = GameObject.AddComponent<UIPage>();
            UiPage.field_Public_String_0 = $"QuickMenuReMod{menuName}";
            UiPage.field_Protected_MenuStateController_0 = MenuEx.QMenuStateCtrl;
            UiPage.field_Private_List_1_UIPage_0 = new Il2CppSystem.Collections.Generic.List<UIPage>();
            UiPage.field_Private_List_1_UIPage_0.Add(UiPage);
            UiPage.GetComponent<Canvas>().enabled = true; // Fix for Late Menu Creation
            UiPage.GetComponent<CanvasGroup>().enabled = true; // Fix for Late Menu Creation
            UiPage.GetComponent<UIPage>().enabled = true; // Fix for Late Menu Creation
            UiPage.GetComponent<GraphicRaycaster>().enabled = true; // Fix for Late Menu Creation
            UiPage.gameObject.active = false;

            // Get scroll stuff
            menuContents = GameObject.transform.Find("ScrollRect/Viewport/VerticalLayoutGroup");
            var scrollRect = RectTransform.Find("Scrollrect").GetComponent<ScrollRect>();
            _container = scrollRect.content;

            // copy properties of old grid layout
            var gridLayoutGroup = _container.Find("Buttons").GetComponent<GridLayoutGroup>();

            Object.DestroyImmediate(_container.GetComponent<VerticalLayoutGroup>());
            var glp = _container.gameObject.AddComponent<GridLayoutGroup>();
            glp.spacing = gridLayoutGroup.spacing;
            glp.cellSize = gridLayoutGroup.cellSize;
            glp.constraint = gridLayoutGroup.constraint;
            glp.constraintCount = gridLayoutGroup.constraintCount;
            glp.startAxis = gridLayoutGroup.startAxis;
            glp.startCorner = gridLayoutGroup.startCorner;
            glp.childAlignment = TextAnchor.UpperLeft;
            glp.padding = gridLayoutGroup.padding;
            glp.padding.top = 8;
            glp.padding.left = 64;

            // delete components we're not using
            Object.DestroyImmediate(_container.Find("Buttons").gameObject);
            Object.DestroyImmediate(_container.Find("Spacer_8pt").gameObject);

            // Fix scrolling
            var scrollbar = scrollRect.transform.Find("Scrollbar");
            scrollbar.gameObject.SetActive(true);

            scrollRect.enabled = true;
            scrollRect.verticalScrollbar = scrollbar.GetComponent<Scrollbar>();
            scrollRect.verticalScrollbarVisibility = ScrollRect.ScrollbarVisibility.Permanent;
            scrollRect.viewport.GetComponent<RectMask2D>().enabled = true;

            MenuEx.QMenuStateCtrl.field_Private_Dictionary_2_String_UIPage_0.Add(UiPage.field_Public_String_0, UiPage);

            if (isRoot)
            {
                var rootPages = MenuEx.QMenuStateCtrl.field_Public_ArrayOf_UIPage_0.ToList();
                rootPages.Add(UiPage);
                MenuEx.QMenuStateCtrl.field_Public_ArrayOf_UIPage_0 = rootPages.ToArray();
            }

            var listener = GameObject.AddComponent<EnableDisableListener>();
            listener.OnEnableEvent += () => OnOpen?.Invoke();
            listener.OnDisableEvent += () => OnClose?.Invoke();
        }

        public ReMenuPage(Transform transform) : base(transform)
        {
            UiPage = GameObject.GetComponent<UIPage>();
            _isRoot = MenuEx.QMenuStateCtrl.field_Public_ArrayOf_UIPage_0.Contains(UiPage);
            var scrollRect = RectTransform.Find("Scrollrect").GetComponent<ScrollRect>();
            _container = scrollRect.content;
        }

        public void Open()
        {
            UiPage.gameObject.active = true;
            MenuEx.QMenuStateCtrl.Method_Public_Void_String_ObjectPublicStBoAc1ObObUnique_Boolean_EnumNPublicSealedvaNoLeRiBoIn6vUnique_0(UiPage.field_Public_String_0);
        }

        public ReMenuButton AddButton(string text, string tooltip, Action onClick, Sprite sprite = null, string color = "#ffffff")
        {
            return new ReMenuButton(text, tooltip, onClick, _container, sprite, color:color);
        }

        public ReMenuButton AddSpacer(Sprite sprite = null)
        {
            var spacer = AddButton(string.Empty, string.Empty, null, sprite);
            spacer.GameObject.name = "Button_Spacer";
            spacer.Background.gameObject.SetActive(false);
            return spacer;
        }

        public ReMenuToggle AddToggle(string text, string tooltip, Action<bool> onToggle, bool defaultValue = false, string color = "#ffffff")
            => AddToggle(text, tooltip, onToggle, defaultValue, null, null, color);
        public ReMenuToggle AddToggle(string text, string tooltip, Action<bool> onToggle, string color = "#ffffff")
            => AddToggle(text, tooltip, onToggle, false, null, null, color);
        public ReMenuToggle AddToggle(string text, string tooltip, ConfigValue<bool> configValue)
            => AddToggle(text, tooltip, configValue, null, null);
        public ReMenuToggle AddToggle(string text, string tooltip, Action<bool> onToggle, bool defaultValue = false, Sprite iconOn = null, Sprite iconOff = null, string color = "#ffffff")
        {
            return new ReMenuToggle(text, tooltip, onToggle, _container, defaultValue, iconOn, iconOff, color);
        }
        public ReMenuToggle AddToggle(string text, string tooltip, Action<bool> onToggle, bool defaultValue = false)
        {
            return new ReMenuToggle(text, tooltip, onToggle, _container, defaultValue);
        }
        public ReMenuToggle AddToggle(string text, string tooltip, Action<bool> onToggle)
        {
            return new ReMenuToggle(text, tooltip, onToggle, _container, false);
        }
        public ReMenuToggle AddToggle(string text, string tooltip, ConfigValue<bool> configValue, Sprite iconOn = null, Sprite iconOff = null, string color = "#ffffff")
        {
            return new ReMenuToggle(text, tooltip, configValue.SetValue, _container, configValue, iconOn, iconOff, color);
        }
        public ReMenuToggle AddToggle(string text, string tooltip, ConfigValue<bool> configValue, string color = "#ffffff")
        {
            return new ReMenuToggle(text, tooltip, configValue.SetValue, _container, configValue, color: color);
        }

        public ReMenuPage AddMenuPage(string text, string tooltip = "", Sprite sprite = null, string color = "#ffffff")
        {
            var existingPage = GetMenuPage(text);
            if (existingPage != null)
            {
                return existingPage;
            }

            var menu = new ReMenuPage(text, color:color);
            AddButton(text, string.IsNullOrEmpty(tooltip) ? $"Open the {text} menu" : tooltip, menu.Open, sprite, color);
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

        public ReMenuPage GetMenuPage(string name)
        {
            var transform = MenuEx.QMenuParent.Find(GetCleanName($"Menu_{name}"));
            return transform == null ? null : new ReMenuPage(transform);
        }

        public ReCategoryPage GetCategoryPage(string name)
        {
            var transform = MenuEx.QMenuParent.Find(GetCleanName($"Menu_{name}"));
            return transform == null ? null : new ReCategoryPage(transform);
        }
        public ReMenuPage ToMenuPage(string name, string tooltip = "", Sprite sprite = null)
        {
            var menu = GetMenuPage(name);
            AddButton(name, string.IsNullOrEmpty(tooltip) ? $"Open the {name} menu" : tooltip, menu.Open, sprite);
            return menu;
        }
        public static ReMenuPage Create(string text, bool isRoot, string color = "#ffffff")
        {
            return new ReMenuPage(text, isRoot, color);
        }

        public static implicit operator ReMenuPage(Transform v)
        {
            throw new NotImplementedException();
        }
    }
}
