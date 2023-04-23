using ReMod.Core.Unity;
using ReMod.Core.VRChat;
using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VRC.UI.Elements;
using VRC.UI.Elements.Menus;

namespace ReMod.Core.UI.MainMenu
{
    public class ReMMenuPage : UiElement, IButtonSystem
    {
        
        private static GameObject _mmmenuPagePrefab;
        private static GameObject MMMenuPagePrefab
        {
            get
            {
                if (_mmmenuPagePrefab == null)
                {
                    _mmmenuPagePrefab = MenuEx.MMenuParent.transform.Find("Menu_Settings").gameObject;
                }
               
                return _mmmenuPagePrefab;
            }
        }
        public UIPage UiPage { get; }
        public event Action OnOpen;
        public event Action OnClose;
        private readonly bool _isRoot;
        public GameObject MenuObject;


        private readonly Transform _container;
        public ReMMenuPage(string text, Sprite icon, bool isRoot = false, string color = "#ffffff") : base(MMMenuPagePrefab, MenuEx.QMenuParent, $"Menu_{text}", false)
        {
            MenuObject = UnityEngine.Object.Instantiate(MMMenuPagePrefab, MMMenuPagePrefab.transform.parent);
            UnityEngine.Object.DestroyImmediate(MenuObject.GetComponent<SettingsPage>());

            MenuObject.transform.SetSiblingIndex(19);

            var menuName = GetCleanName(text);
            MenuObject.name = menuName;
            UiPage = MenuObject.AddComponent<UIPage>();
            UiPage.field_Public_String_0 = $"MainMenuReMod{menuName}";
            UiPage.field_Protected_MenuStateController_0 = MenuEx.MMenuStateCtrl;
            UiPage.field_Private_List_1_UIPage_0 = new Il2CppSystem.Collections.Generic.List<UIPage>();
            UiPage.field_Private_List_1_UIPage_0.Add(UiPage);
            UiPage.GetComponent<Canvas>().enabled = true; // Fix for Late Menu Creation
            UiPage.GetComponent<CanvasGroup>().enabled = true; // Fix for Late Menu Creation
            UiPage.GetComponent<UIPage>().enabled = true; // Fix for Late Menu Creation
            UiPage.GetComponent<GraphicRaycaster>().enabled = true; // Fix for Late Menu Creation
            UiPage.gameObject.active = false;

            MenuEx.MMenuStateCtrl.field_Private_Dictionary_2_String_UIPage_0.Add(UiPage.field_Public_String_0, UiPage);

            var list = MenuEx.MMenuStateCtrl.field_Public_ArrayOf_UIPage_0.ToList();
            list.Add(UiPage);
            MenuEx.MMenuStateCtrl.field_Public_ArrayOf_UIPage_0 = list.ToArray();

            var listener = GameObject.AddComponent<EnableDisableListener>();
            listener.OnEnableEvent += () => OnOpen?.Invoke();
            listener.OnDisableEvent += () => OnClose?.Invoke();

            //This section and below is for creating the new menu
            Transform VLC = MenuObject.transform.Find("Menu_MM_DynamicSidePanel/Panel_SectionList/ScrollRect_Navigation/ScrollRect_Content/Viewport/VerticalLayoutGroup");
            for (int i = 0; i < VLC.childCount; i++)
            {
                Transform child = VLC.GetChild(i);
                if (child == null || child.name == "DynamicSidePanel_Header")
                    continue;
                UnityEngine.Object.Destroy(child.gameObject);
            }
            Transform categoryContainer = MenuObject.transform.Find("Menu_MM_DynamicSidePanel/Panel_SectionList/ScrollRect_Navigation/Viewport/VerticalLayoutGroup");
            for (int i = 0; i < categoryContainer.childCount; i++)
            {
                Transform child = categoryContainer.GetChild(i);
                if (child == null || child.name == "DynamicSidePanel_Header")
                    continue;
                UnityEngine.Object.Destroy(child.gameObject);
            }
            var MenuTitleText = MenuObject.transform.Find("Menu_MM_DynamicSidePanel/Panel_SectionList/ScrollRect_Navigation/Viewport/VerticalLayoutGroup/DynamicSidePanel_Header/Text_Name").GetComponent<TextMeshProUGUI>();
            MenuTitleText.text = text;

            UnityEngine.Object.Destroy(MenuTitleText.transform.parent.Find("Button_Logout").gameObject);
            UnityEngine.Object.Destroy(MenuTitleText.transform.parent.Find("Button_Exit").gameObject);
            MenuTitleText.transform.parent.Find("Separator").GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -100);
            MenuTitleText.transform.parent.GetComponent<LayoutElement>().minHeight = 100;
            /*var rt = MenuTitleText.transform.parent.Find("Text_Name").GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2(0, -50);
            rt.sizeDelta = new Vector2(250, 48);*/
            MenuTitleText.transform.parent.Find("Icon").GetComponent<Image>().sprite = icon;
            MenuObject.transform.Find("Menu_MM_DynamicSidePanel/Panel_SectionList/ScrollRect_Navigation/ScrollRect_Content/Header_MM_H2/LeftItemContainer/Text_Title").gameObject.SetActive(false);
           

        }
        public ReMMenuPage(Transform transform) : base(transform)
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

            OnOpen?.Invoke();
        }
        public Transform GetCategoryButtonContainer()
        {
            return MenuObject.transform.Find("Menu_MM_DynamicSidePanel/Panel_SectionList/ScrollRect_Navigation/Viewport/VerticalLayoutGroup");
        }

        public Transform GetCategoryChildContainer()
        {
            return MenuObject.transform.Find("Menu_MM_DynamicSidePanel/Panel_SectionList/ScrollRect_Navigation/ScrollRect_Content/Viewport/VerticalLayoutGroup");
        }
        public TextMeshProUGUI GetCategoryTitle()
        {
            return MenuObject.transform.Find("Menu_MM_DynamicSidePanel/Panel_SectionList/ScrollRect_Navigation/ScrollRect_Content/Header_MM_H2/LeftItemContainer/Text_Title").GetComponent<TextMeshProUGUI>();
        }

        public ReMMenuPage GetMenuPage(string name)
        {
            var transform = MenuEx.MMenuParent.Find(GetCleanName($"Menu_{name}"));
            return transform == null ? null : new ReMMenuPage(transform);
        }
        public static ReMMenuPage Create(string text, Sprite icon, bool isRoot)
        {
            return new ReMMenuPage(text, icon, isRoot);
        }
    }
}